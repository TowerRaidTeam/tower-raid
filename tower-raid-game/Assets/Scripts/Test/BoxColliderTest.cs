using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderTest : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "box")
        {
            
        }

        Debug.Log(collision.contactCount);
    }
}
