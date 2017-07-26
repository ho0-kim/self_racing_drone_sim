using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollide : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player_collider"))
        { 
            this.gameObject.SetActive(true);
            GameOver.ScoreCalc();
            GameOver.Restart();
        }
    }
}