using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accel_Gyro_measure : MonoBehaviour {
    
    public GameObject abc;
    private Rigidbody rig;
    private Vector3 acceleration = Physics.gravity;
    private Vector3 old_velocity = new Vector3(0, 0, 0);

    private Vector3 old_z_vector;
    private Vector3 old_y_vector;
    private Vector3 old_x_vector;

    public static float accel_z;
    public static float accel_y;
    public static float accel_x;

    public static float vel_pitch;
    public static float vel_rol;
    public static float vel_yaw;


    // Use this for initialization
    void Start () {
        rig = abc.GetComponent<Rigidbody>();

        old_z_vector = abc.transform.forward;
        old_y_vector = abc.transform.up;
        old_x_vector = abc.transform.right;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        acceleration = (rig.velocity - old_velocity) / Time.deltaTime + Physics.gravity;
        old_velocity = rig.velocity;
        
        accel_z = Vector3.Dot(acceleration, rig.transform.forward);
        accel_y = Vector3.Dot(acceleration, rig.transform.up);
        accel_x = Vector3.Dot(acceleration, rig.transform.right);
        
        vel_pitch = (Mathf.Asin(Vector3.Dot(Vector3.Cross(abc.transform.forward, old_z_vector), abc.transform.right)) / Time.deltaTime) * 360 / Mathf.PI;
        vel_rol = (Mathf.Asin(Vector3.Dot(Vector3.Cross(abc.transform.right, old_x_vector), abc.transform.forward)) / Time.deltaTime) * 360 / Mathf.PI;
        vel_yaw = (Mathf.Asin(Vector3.Dot(Vector3.Cross(abc.transform.forward, old_z_vector), abc.transform.up)) / Time.deltaTime) * 360 / Mathf.PI;
        old_z_vector = abc.transform.forward;
        old_x_vector = abc.transform.right;
        old_y_vector = abc.transform.up;
    }
}
