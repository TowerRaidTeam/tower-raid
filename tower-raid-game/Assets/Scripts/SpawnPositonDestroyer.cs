using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositonDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "buildable" || other.gameObject.tag == "Road")
        {
            Destroy(this.gameObject);
        }
    }
}
