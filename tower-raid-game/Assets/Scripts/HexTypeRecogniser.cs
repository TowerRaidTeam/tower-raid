using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypeRecogniser : MonoBehaviour
{
    public string hexType;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Wind":
                Debug.Log(other.gameObject.tag);
                hexType = "AirCrystal";
                break;
            case "Earth":
                Debug.Log(other.gameObject.tag);
                hexType = "EarthCrystal";
                break;
            case "Fire":
                Debug.Log(other.gameObject.tag);
                hexType = "FireCrystal";
                break;
            case "Water":
                Debug.Log(other.gameObject.tag);
                hexType = "WaterCrystal";
                break;
            default:
                Debug.Log("Not touching other hex types");
                break;
        }
    }
}
