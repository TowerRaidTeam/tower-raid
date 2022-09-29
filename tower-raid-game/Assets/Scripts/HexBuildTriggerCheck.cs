using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBuildTriggerCheck : MonoBehaviour
{
    public static Vector3 placmentPosition;
    public static Vector3 hexAdjustmentAmount;
    public static GameObject conectingRoad;

    public static GameObject thisObject;

    //private List<Transform> spawnPositionsList;
    [SerializeField] Transform[] spawnPositions;
    public static Vector3 spawnPositionLocation;
    public static bool isTuching = false;
    public bool isTuchingForDeleat = false;
    public static GameObject thisGameObject;
   
    MeshCollider mc;
    WorldGeneration wg;

    private void Start()
    {
        wg = FindObjectOfType<WorldGeneration>();
        mc = GetComponent<MeshCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "buildHex")
        //{
        //    Debug.Log("Can build");
        //}


        //if (other.gameObject.tag == "RoadConnect")
        //{
        //    Debug.Log(mc.bounds.max);
        //    Debug.Log("Position: " + transform.position +" isExoandable: " + isExpandable);
        //    isExpandable = true;

        //    conectingRoad = other.gameObject;
        //    placmentPosition = transform.position;
        //    hexAdjustmentAmount = placmentPosition - transform.root.position  /*- new Vector3(mc.bounds.extents.x, 0f, mc.bounds.extents.z)*/ /*- new Vector3(mc.bounds.extents.x, 0f, mc.bounds.extents.z)*/;

        //    //Debug.Log("Placment position: " + placmentPosition);
        //    //Debug.Log(Vector3.Distance(transform.position, other.gameObject.transform.position));
        //}

        if (other.gameObject.tag == "RoadConnect")
        {
            thisGameObject = this.gameObject;
            //tuchingHex = other.gameObject;
            Debug.Log("Touching");
            thisObject = this.gameObject;
            //foreach (Transform part in transform.root)
            //{
            //    if (part.gameObject.tag == "SpawnPosition")
            //    {
            //        spawnPositionsList.Add(part);
            //        Debug.Log(spawnPositionsList);
            //    }
            //}
            isTuching = true;
            isTuchingForDeleat = true;
            spawnPositionLocation = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
            GameManager.isExtendable = true;
        }
         
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "RoadConnect")
    //    {
    //        isTuching = true;
    //    }

    //    Debug.Log(isTuching);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "RoadConnect")
    //    {
    //        thisGameObject = null;
    //        isTuching = false;
    //        isTuchingForDeleat = false;
    //        GameManager.isExtendable = false;
    //    }
        
    //}

    public void DestroySpawnPositions()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Destroy(spawnPositions[i]);
        }
    }
}
