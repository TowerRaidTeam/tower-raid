using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    bool canBuild = false;
    public static bool isBought = false;

    [SerializeField] private GameObject building;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject shopCoverPanel;
    GameObject turret;

    GameManager gm;
    private int turretPrice = 50;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isBought)
        {

            turret.transform.position = GetMousePosition();
            if (Input.GetMouseButtonDown(0))
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

                    if (canBuild)
                    {
                        Destroy(turret);
                        
                        GameObject buildSpawn = Instantiate(building, hit.transform.position + new Vector3(0f, hit.transform.GetComponent<Renderer>().bounds.extents.y, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                        buildSpawn.transform.position = new Vector3(buildSpawn.transform.position.x, buildSpawn.transform.position.y + (buildSpawn.transform.localScale.y / 2), buildSpawn.transform.position.z);
                        shopCoverPanel.SetActive(false);
                        isBought = false;
                    }

                }
            }
            //SELL TOWERA POSLJE ZAMJENI
            //else if (Input.GetMouseButtonDown(1))
            //{
            //    gm.cash += turretPrice;
            //    gm.UpdateCash();
            //    Destroy(turret);
            //    shopCoverPanel.SetActive(false);
            //    isBought = false;
            //}

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

    //public void BoughtATower()
    //{
    //    if (gm.cash >= turretPrice)
    //    {
    //        gm.cash -= turretPrice;
    //        gm.UpdateCash();
    //        shopCoverPanel.SetActive(true);
    //        isBought = true;
    //        turret = Instantiate(building, GetMousePosition(), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
    //        turret.GetComponent<Collider>().enabled = false;
    //    }
    //    else
    //    {
    //        Debug.Log("NOT ENAUGH MONEY");
    //    }


    //}

    public void BoughtATower()
    {
        shopCoverPanel.SetActive(true);
        turret = Instantiate(building, GetMousePosition(), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
        turret.GetComponent<Collider>().enabled = false;
        Invoke(nameof(ChangeIsBought), 0.05f);
    }

    void ChangeIsBought()
    {
        isBought = true;
        Debug.Log(isBought + "This is it, the bought check");
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
