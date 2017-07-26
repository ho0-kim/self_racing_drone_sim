using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CheckPoint03 : MonoBehaviour {

    public static Stopwatch cp03_sw = new Stopwatch();

    public GameObject Player_collider;
    public GameObject finishPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player_collider)
        {
            this.gameObject.SetActive(false);
            finishPoint.SetActive(true);

            GameOver.checkpoint = 3;

            cp03_sw.Stop();
        }
    }
}
