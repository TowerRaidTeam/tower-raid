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
                Debug.Log(other.gameObject.tag + "PLACED AIR");
                hexType = "AirCrystal";
                this.enabled = false;
                break;
            case "Earth":
                Debug.Log(other.gameObject.tag + "PLACED EARTH");
                hexType = "EarthCrystal";
                this.enabled = false;
                break;
            case "Fire":
                Debug.Log(other.gameObject.tag + "PLACED FIRE");
                hexType = "FireCrystal";
                this.enabled = false;
                break;
            case "Water":
                Debug.Log(other.gameObject.tag + "PLACED WATER");
                hexType = "WaterCrystal";
                this.enabled = false;
                break;
            default:
                Debug.Log("Not touching other hex types");
                break;
        }
    }
}
