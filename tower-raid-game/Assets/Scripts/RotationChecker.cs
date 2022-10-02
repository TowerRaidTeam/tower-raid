using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    public bool correctAligmentOfRoads = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoadConnect")
        {
            
            correctAligmentOfRoads = true;
            //Debug.Log("Touchin road connect: " + correctAligmentOfRoads);
        }
    }
}
