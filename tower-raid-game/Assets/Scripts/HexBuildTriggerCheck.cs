using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBuildTriggerCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "buildHex")
        {
            Debug.Log("Can build");
        }
       
    }
}
