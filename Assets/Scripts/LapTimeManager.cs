using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class LapTimeManager : MonoBehaviour {
    
    public static Stopwatch lapTime = new Stopwatch();

    public static int MinuteCount;
    public static int Secondcount;
    public static float Millicount;
    public static string MilliDisplay;

    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MilliBox;
    public GameObject RawLapTimeBox;

    private void Start()
    {
        GameOver.Score = 0;
        GameOver.checkpoint = 0;
        MinuteCount = 0;
        Secondcount = 0;
        Millicount = 0;
        CheckPoint01.cp01_sw.Stop();
        CheckPoint01.cp01_sw.Reset();
        CheckPoint02.cp02_sw.Stop();
        CheckPoint02.cp02_sw.Reset();
        CheckPoint03.cp03_sw.Stop();
        CheckPoint03.cp03_sw.Reset();
        lapTime.Stop();
        lapTime.Reset();

        CheckPoint01.cp01_sw.Start();
        lapTime.Start();
    }

    // Update is called once per frame
    void Update()
    {
        RawLapTimeBox.GetComponent<Text>().text = lapTime.ElapsedMilliseconds.ToString("N0") + "ms";

        Millicount += Time.deltaTime * 10;
        MilliDisplay = Millicount.ToString("F0");
        if (Millicount >= 10)
        {
            Millicount = 0;
            Secondcount += 1;
        }
        MilliBox.GetComponent<Text>().text = "" + MilliDisplay;

        if (Secondcount >= 60)
        {
            Secondcount = 0;
            MinuteCount += 1;
        }
        if (Secondcount <= 9)
            SecondBox.GetComponent<Text>().text = "0" + Secondcount + ".";
        else
            SecondBox.GetComponent<Text>().text = "" + Secondcount + ".";

        if (MinuteCount <= 9)
            MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";
        else
            MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";
    }
}
