using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SortingArray))]
public class WorldGeneration : MonoBehaviour
{
    GameManager gameManager;
    SortingArray sortingArray;
    [SerializeField] GameObject[] hexPrefabs;
    [SerializeField] LayerMask layerMask;

    private GameObject chunk;
    private bool chunsIsSpawnd = false;

    private int rotation;
    private Transform[] spawnBlocks;

    public static Vector3[] path;
    private void Start()
    {
        sortingArray = FindObjectOfType<SortingArray>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            gameManager.StartSpawningEnemys();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameManager.StartSpawningEnemys();
        }

        //Debug.Log(GameManager.isExtendable);
        if (Input.GetKeyDown(KeyCode.B) && !chunsIsSpawnd)
        {
            SpawnChunk();
           
        }

        if (chunsIsSpawnd)
        {
            chunk.transform.position = GetMousePosition();
            if (Input.GetMouseButtonDown(0) && GameManager.isExtendable && HexBuildTriggerCheck.isTuching)
            {
                HexBuildTriggerCheck.isTuching = false;
                GameManager.isExtendable = false;
                chunsIsSpawnd = false;
                while (HexBuildTriggerCheck.isTuching == true)
                {
                    Destroy(HexBuildTriggerCheck.thisObject);
                }
                //Debug.Log(chunk.transform.root.position - HexBuildTriggerCheck.conectingRoad.transform.position);
                //HexBuildTriggerCheck.conectingRoad.transform.position = HexBuildTriggerCheck.placmentPosition;
                chunk.transform.position = HexBuildTriggerCheck.spawnPositionLocation;
                Destroy(HexBuildTriggerCheck.thisGameObject);

                spawnBlocks = chunk.GetComponentsInChildren<Transform>();
                spawnBlocks = spawnBlocks.Where(child => child.tag == "buildHex").ToArray();
                Debug.Log(spawnBlocks);
                foreach (Transform item in spawnBlocks)
                {
                    Debug.Log(item.name);
                    item.gameObject.GetComponent<BoxCollider>().enabled = true;
                }
                
               
                
                //sortingArray.SpawnPreview();
                path = sortingArray.GenerateNewPath().ToArray();
                
                Debug.Log(HexBuildTriggerCheck.isTuching);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                rotation -= 60;
                UpdateRotation(rotation);
                
                //Debug.Log("Rotate: " + rotation);
                
            }
        }
    }

    private void SpawnChunk()
    {
        int randomPrefab = Random.Range(0, hexPrefabs.Length);
        chunk = Instantiate(hexPrefabs[randomPrefab], new Vector3(GetMousePosition().x, 0f, GetMousePosition().z), hexPrefabs[randomPrefab].transform.rotation);
        
        chunsIsSpawnd = true;
    }

    private void UpdateRotation(int angle)
    {
        chunk.transform.rotation = Quaternion.Euler(90, 0, chunk.transform.rotation.z + angle);
    }

    private Transform GetRoadExpandHex()
    {
        foreach (Transform hex in chunk.transform)
        {
            if (hex.gameObject.tag == "RoadConnect")
            {
                return hex;
            }
            else
            {
                return null;
            }
        }
        return null;
    }


    //Gives me the ray hit position in Vector3 on the hexBuild layer
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
