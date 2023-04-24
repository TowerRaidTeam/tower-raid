using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedTurret : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "foliage")
        {
            other.gameObject.SetActive(false);
        }
    }
}
