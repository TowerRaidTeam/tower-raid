using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBuildTriggerCheck : MonoBehaviour
{
    //public static Vector3 placmentPosition;
    //public static Vector3 hexAdjustmentAmount;
    public static GameObject conectingRoad;

    //public static GameObject thisObject;

    //private List<Transform> spawnPositionsList;
    [SerializeField] Transform[] spawnPositions;
    public static Vector3 spawnPositionLocation;
    //public Vector3 spawnPositionLocation;
    //public static bool isTuching = false;
    public static bool isTuching = false;
    public bool isTuchingLocal = false;
    [SerializeField] bool check = false;
    //public bool isTuchingForDeleat = false;
    //public static GameObject thisGameObject;
   
    MeshCollider mc;
    WorldGeneration wg;

    private void Start()
    {
        wg = FindObjectOfType<WorldGeneration>();
        mc = GetComponent<MeshCollider>();
    }

    public void HasBeenPlaces()
    {
        check = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoadConnect")
        {
            
            
            isTuching = true;
            
            //isTuchingForDeleat = true;
            GameManager.isExtendable = true;
            spawnPositionLocation = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
            Debug.Log("Touching " + isTuching);
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        //if (check)
        //{
        //    if (other.gameObject.tag == "Road")
        //    {
        //        Debug.Log("Needs to Be Deleated");
        //        isPlacedAndTouchingRoadOrBuildable = true;

        //    }
        //    if (other.gameObject.tag == "buildable")
        //    {
        //        Debug.Log("Needs to Be Deleated");
        //        isPlacedAndTouchingRoadOrBuildable = true;
        //    }
        //}

        //if (isPlacedAndTouchingRoadOrBuildable)
        //{
        //    spawnPositionLocation = Vector3.zero;
        //    Destroy(this.gameObject);
        //}
        //if (this.gameObject != null)
        //{
        //    check = false;
        //}
        if (other.gameObject.tag == "RoadConnect")
        {

            isTuchingLocal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RoadConnect")
        {
            
            
            isTuching = false;
            isTuchingLocal = false;
            GameManager.isExtendable = false;
            //isTuchingForDeleat = true;
            Debug.Log("Left " + isTuching);


        }
    }

    public void DestroySpawnPositions()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Destroy(spawnPositions[i]);
        }
    }

    public bool ReturnBool()
    {
        return isTuching;
    }
}
