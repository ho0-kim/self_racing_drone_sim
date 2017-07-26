using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordSave : MonoBehaviour {

    public const string CSVFileName = "driving_log.csv";
    public const string DirFrames = "IMG";

    public Camera CenterCamera;

    private string m_saveLocation = Directory.GetCurrentDirectory();

    private Queue<DroneSample> droneSamples;
        private int TotalSamples;
        private bool isSaving;
        //private Vector3 saved_position;
        //private Quaternion saved_rotation;

    private bool m_isRecording = false;
    public bool IsRecording
    {
        get
        {
            return m_isRecording;
        }

        set
        {
            m_isRecording = value;
            if (value == true)
            {
                Debug.Log("Starting to record");
                droneSamples = new Queue<DroneSample>();
                StartCoroutine(Sample());
            }
            else
            {
                Debug.Log("Stopping record");
                StopCoroutine(Sample());
                Debug.Log("Writing to disk");
                //save the cars coordinate parameters so we can reset it to this properly after capturing data
                //saved_position = transform.position;
                //saved_rotation = transform.rotation;
                //see how many samples we captured use this to show save percentage in UISystem script
                TotalSamples = droneSamples.Count;
                isSaving = true;
                StartCoroutine(WriteSamplesToDisk());

            };
        }
    }

    // Use this for initialization
    void Start () {
        string targetDir = Path.Combine(m_saveLocation, DirFrames);
        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (IsRecording)
        {
            //Dump();
        }
    }

    public IEnumerator WriteSamplesToDisk()
    {
        yield return new WaitForSeconds(0.000f); //retrieve as fast as we can but still allow communication of main thread to screen and UISystem
        if (droneSamples.Count > 0)
        {
            //pull off a sample from the que
            DroneSample sample = droneSamples.Dequeue();

            //pysically moving the car to get the right camera position
            //transform.position = sample.position;
            //transform.rotation = sample.rotation;

            // Capture and Persist Image
            string centerPath = WriteImage(CenterCamera, "center", sample.timeStamp);

            string row = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}\n", centerPath, sample.accel_x, sample.accel_y, sample.accel_z, sample.vel_roll, sample.vel_pitch, sample.vel_yaw, sample.throttle, sample.roll, sample.pitch, sample.yaw);
            File.AppendAllText(Path.Combine(m_saveLocation, CSVFileName), row);
        }
        if (droneSamples.Count > 0)
        {
            //request if there are more samples to pull
            StartCoroutine(WriteSamplesToDisk());
        }
        else
        {
            //all samples have been pulled
            StopCoroutine(WriteSamplesToDisk());
            isSaving = false;

            //need to reset the car back to its position before ending recording, otherwise sometimes the car ended up in strange areas
            //transform.position = saved_position;
            //transform.rotation = saved_rotation;
            //m_Rigidbody.velocity = new Vector3(0f, -10f, 0f);
            //Move(0f, 0f, 0f, 0f);

        }
    }

    public float getSavePercent()
    {
        return (float)(TotalSamples - droneSamples.Count) / TotalSamples;
    }

    public bool getSaveStatus()
    {
        return isSaving;
    }



    private string WriteImage(Camera camera, string prepend, string timestamp)
    {
        //needed to force camera update 
        camera.Render();
        RenderTexture targetTexture = camera.targetTexture;
        RenderTexture.active = targetTexture;
        Texture2D texture2D = new Texture2D(targetTexture.width, targetTexture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
        texture2D.Apply();
        byte[] image = texture2D.EncodeToJPG();
        UnityEngine.Object.DestroyImmediate(texture2D);
        string directory = Path.Combine(m_saveLocation, DirFrames);
        string path = Path.Combine(directory, prepend + "_" + timestamp + ".jpg");
        File.WriteAllBytes(path, image);
        image = null;
        return path;
    }



    public IEnumerator Sample()
    {
        // Start the Coroutine to Capture Data Every Second.
        // Persist that Information to a CSV and Perist the Camera Frame
        yield return new WaitForSeconds(0.0666666666666667f);

        if (m_saveLocation != "")
        {
            DroneSample sample = new DroneSample();

            sample.timeStamp = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            sample.accel_x = Accel_Gyro_measure.accel_x;
            sample.accel_y = Accel_Gyro_measure.accel_y;
            sample.accel_z = Accel_Gyro_measure.accel_z;
            sample.vel_pitch = Accel_Gyro_measure.vel_pitch;
            sample.vel_roll = Accel_Gyro_measure.vel_rol;
            sample.vel_yaw = Accel_Gyro_measure.vel_yaw;
            //sample.throttle = ; input;
            //sample.roll = ;
            //sample.pitch = ;
            //sample.yaw = ;

            droneSamples.Enqueue(sample);

            sample = null;
            //may or may not be needed
        }

        // Only reschedule if the button hasn't toggled
        if (IsRecording)
        {
            StartCoroutine(Sample());
        }

    }

    internal class DroneSample
    {
        //public Quaternion rotation;
        public float vel_roll, vel_pitch, vel_yaw;
        public float accel_x, accel_y, accel_z;
        public float throttle;
        public float roll;
        public float yaw;
        public float pitch;
        public string timeStamp;
    }
}
