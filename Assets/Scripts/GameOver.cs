using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static long Score;
    public static int checkpoint = 0;

    public static void ScoreCalc() // checkpoint : 0(o) 1(checkpoint01) 2(checkpoint02) 3(checkpoint03) 4(finishpoint)
    {
        if(checkpoint == 0)
            Score = -10000;
        else if(checkpoint == 1)
            Score = ((long)1000000) / (CheckPoint01.cp01_sw.ElapsedMilliseconds) - 10000;
        else if(checkpoint == 2)
            Score = ((long)1000000) / (CheckPoint01.cp01_sw.ElapsedMilliseconds) + ((long)1000000) / (CheckPoint02.cp02_sw.ElapsedMilliseconds) - 10000;
        else if(checkpoint == 3)
            Score = ((long)1000000) / (CheckPoint01.cp01_sw.ElapsedMilliseconds) + ((long)1000000) / (CheckPoint02.cp02_sw.ElapsedMilliseconds) + ((long)1000000) / (CheckPoint03.cp03_sw.ElapsedMilliseconds) - 10000;
        else
            Score = ((long)1000000) / (CheckPoint01.cp01_sw.ElapsedMilliseconds) + ((long)1000000) / (CheckPoint02.cp02_sw.ElapsedMilliseconds) + ((long)1000000) / (CheckPoint03.cp03_sw.ElapsedMilliseconds) + ((long)1000000) / (LapTimeManager.lapTime.ElapsedMilliseconds);
    }

    public static void Restart()
    {
        SceneManager.LoadScene("Scene");
    }
}
