using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CheckPoint02 : MonoBehaviour {

    public static Stopwatch cp02_sw = new Stopwatch();

    public GameObject Player_collider;
    public GameObject checkPoint03;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player_collider)
        {
            this.gameObject.SetActive(false);
            checkPoint03.SetActive(true);

            GameOver.checkpoint = 2;

            cp02_sw.Stop();
            CheckPoint03.cp03_sw.Start();
        }
    }
}
