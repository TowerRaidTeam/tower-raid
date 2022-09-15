using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    bool canBuild = false;

    [SerializeField] private GameObject building;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Checks if the tile zou are tring to build on is buildable
        //??Add chechker to see if there is building all ready placed there
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.tag == "buildable")
            {
                canBuild = true;
            }
            else
            {
                canBuild = false;
            }

            if (canBuild && Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject build = Instantiate(building, hit.transform.position + new Vector3(0f, hit.transform.GetComponent<Renderer>().bounds.extents.y, 0f), Quaternion.Euler(0f,Random.Range(0,360),0f));
                build.transform.position = new Vector3(build.transform.position.x, build.transform.position.y + (build.transform.localScale.y / 2), build.transform.position.z);
            }


        }
        //Debug.Log(hit.transform.GetComponent<Renderer>().bounds.max.y);
        //Debug.Log(canBuild);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }
}
