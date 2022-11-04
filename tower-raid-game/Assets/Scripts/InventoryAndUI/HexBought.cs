using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBought : MonoBehaviour
{
    [HideInInspector] public int hexIndex;
    private void Start()
    {
        hexIndex = Random.Range(0, 16);
        //hexIndex = 0;
    }
}
