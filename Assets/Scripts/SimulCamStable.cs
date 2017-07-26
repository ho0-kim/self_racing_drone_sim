using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulCamStable : MonoBehaviour {

    private GameObject theDrone;
    private float droneX;
    private float droneY;
    private float droneZ;

    private void Start()
    {
        theDrone = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        droneX = theDrone.transform.eulerAngles.x;
        droneY = theDrone.transform.eulerAngles.y;
        droneZ = theDrone.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(droneX - droneX, droneY, droneZ - droneZ);
    }
}
