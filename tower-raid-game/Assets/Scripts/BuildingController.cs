using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    bool canBuild = false;
    bool isBought = false;

    [SerializeField] private GameObject building;
    [SerializeField] private LayerMask layerMask;

    GameObject turret;

    private void Update()
    {
        if (isBought)
        {

            turret.transform.position = GetMousePosition();
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log("HERE BABY");
                //Checks if the tile zou are tring to build on is buildable
                //??Add chechker to see if there is building all ready placed there
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("HERE BABY 2");
                    if (hit.transform.tag == "buildable")
                    {
                        canBuild = true;
                    }
                    else
                    {
                        canBuild = false;
                    }

                    if (canBuild)
                    {
                        Destroy(turret);
                        GameObject buildSpawn = Instantiate(building, hit.transform.position + new Vector3(0f, hit.transform.GetComponent<Renderer>().bounds.extents.y, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                        buildSpawn.transform.position = new Vector3(buildSpawn.transform.position.x, buildSpawn.transform.position.y + (buildSpawn.transform.localScale.y / 2), buildSpawn.transform.position.z);
                        isBought = false;
                    }

                }
            }
        }

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        ////Checks if the tile zou are tring to build on is buildable
        ////??Add chechker to see if there is building all ready placed there
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        //{
        //    if (hit.transform.tag == "buildable")
        //    {
        //        canBuild = true;
        //    }
        //    else
        //    {
        //        canBuild = false;
        //    }

        //    if (canBuild && Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        GameObject build = Instantiate(building, hit.transform.position + new Vector3(0f, hit.transform.GetComponent<Renderer>().bounds.extents.y, 0f), Quaternion.Euler(0f,Random.Range(0,360),0f));
        //        build.transform.position = new Vector3(build.transform.position.x, build.transform.position.y + (build.transform.localScale.y / 2), build.transform.position.z);
        //    }


        //}

        
        //Debug.Log(hit.transform.GetComponent<Renderer>().bounds.max.y);
        //Debug.Log(canBuild);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }

    public void BoughtATower()
    {
        isBought = true;
        turret = Instantiate(building, GetMousePosition(), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
        turret.GetComponent<Collider>().enabled = false;
        
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
