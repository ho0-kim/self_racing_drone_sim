using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CheckPoint01 : MonoBehaviour
{
    public static Stopwatch cp01_sw = new Stopwatch();

    public GameObject Player_collider;
    public GameObject checkPoint02;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player_collider)
        {
            this.gameObject.SetActive(false);
            checkPoint02.SetActive(true);

            GameOver.checkpoint = 1;
            
            cp01_sw.Stop();
            CheckPoint02.cp02_sw.Start();
        }
    }
}