using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int maxAmmo;
    int currentAmmo;

    public float fireRate;
    float fireRateRestart;
    public float accuracy;

    public Camera mainCamera;
    public Camera scopeCamera;
}
