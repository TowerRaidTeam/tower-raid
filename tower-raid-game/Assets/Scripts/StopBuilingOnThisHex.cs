using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBuilingOnThisHex : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "buildable")
        {
            other.gameObject.tag = "Untagged";
            Destroy(this);
        }
    }
}
