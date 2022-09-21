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
    MeshCollider mc;

    private void Start()
    {
       
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
            spawnPositionLocation = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
            GameManager.isExtendable = true;
        }
        
    }
}
