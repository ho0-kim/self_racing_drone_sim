using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeComplete : MonoBehaviour {

    public GameObject finishPoint;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    private void OnTriggerEnter(Collider other)
    {
        LapTimeManager.lapTime.Stop();

        if (LapTimeManager.Secondcount <= 9)
            SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.Secondcount + ".";
        else
            SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.Secondcount + ".";

        if (LapTimeManager.MinuteCount <= 9)
            MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";
        else
            MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";

        MilliDisplay.GetComponent<Text>().text = "" + LapTimeManager.Millicount;

        LapTimeManager.MinuteCount = 0;
        LapTimeManager.Secondcount = 0;
        LapTimeManager.Millicount = 0;
        
        finishPoint.SetActive(false);

        GameOver.checkpoint = 4;
        GameOver.ScoreCalc();
        GameOver.Restart();
    }
}
