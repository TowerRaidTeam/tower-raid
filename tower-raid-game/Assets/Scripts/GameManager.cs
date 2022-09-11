using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool  GetTurretHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static GameObject GetTurretHitGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
