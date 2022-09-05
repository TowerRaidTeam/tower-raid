using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    bool canBuild = false;

    [SerializeField] private GameObject building;

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
