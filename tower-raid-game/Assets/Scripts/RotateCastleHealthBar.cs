using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCastleHealthBar : MonoBehaviour
{
    [SerializeField] Camera cam;
    Quaternion originaRotation;

    private void Start()
    {
        originaRotation = Camera.main.transform.rotation;
       // transform.rotation = originaRotation;
       
    }

    private void Update()
    {
        
        transform.rotation = cam.transform.rotation;
        //transform.Rotate(new Vector3(transform.rotation.x, cam.transform.rotation.y, transform.rotation.z));
    }
}

