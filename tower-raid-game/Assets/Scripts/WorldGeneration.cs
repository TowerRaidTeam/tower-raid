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

    bool isTuchingPrivate;
    bool isAlignedCorrectlyToPlace = false;

    //private void Update()
    //{
    //   // Debug.Log(HexBuildTriggerCheck.spawnPositionLocation);

    //    //SPAWNING ENEMIES FOR TESTING
    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        gameManager.StartSpawningEnemys();
    //    }
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        gameManager.StartSpawningEnemys();
    //    }

    //    //SPAWN NEW CHUNK, GONNA CHANGE WHEN I ADD BUTTON
    //    if (Input.GetKeyDown(KeyCode.B) && !chunsIsSpawnd)
    //    {
    //        SpawnChunk();

    //    }

    //    //CHECKS IF A NEW CHUNK HAS BEN SPAWNED AND LETSE ME PLACE IT
    //    if (chunsIsSpawnd)
    //    {
    //        //moves the cgunk to the mouse position
    //        chunk.transform.position = GetMousePosition();

    //        if (Input.GetMouseButtonDown(0) && GameManager.isExtendable && HexBuildTriggerCheck.isTuching && HexBuildTriggerCheck.spawnPositionLocation != Vector3.zero)
    //        {
    //            chunk.transform.position = HexBuildTriggerCheck.spawnPositionLocation;

    //            GameManager.isExtendable = false;



    //            spawnBlocks = chunk.GetComponentsInChildren<Transform>();
    //            spawnBlocks = spawnBlocks.Where(child => child.tag == "buildHex").ToArray();

    //            foreach (Transform item in spawnBlocks)
    //            {
    //                item.GetComponent<Collider>().enabled = true;
    //                item.GetComponent<HexBuildTriggerCheck>().HasBeenPlaces();
    //                //if (item.GetComponent<HexBuildTriggerCheck>().isPlacedAndTouchingRoadOrBuildable)
    //                //{
    //                //    Destroy(item.gameObject);
    //                //}

    //            }

    //            path = sortingArray.GenerateNewPath().ToArray();
    //            chunsIsSpawnd = false;

    //            //if (HexBuildTriggerCheck.spawnPositionLocation != Vector3.zero)
    //            //{
    //            //    HexBuildTriggerCheck[] temp = chunk.GetComponentsInChildren<HexBuildTriggerCheck>();
    //            //    foreach (var item in temp)
    //            //    {
    //            //        if (item.isTuchingForDeleat)
    //            //        {
    //            //            Destroy(item.gameObject);
    //            //        }
    //            //    }
    //            //    HexBuildTriggerCheck.spawnPositionLocation = Vector3.zero;
    //            //}
    //            //HexBuildTriggerCheck.spawnPositionLocation = Vector3.zero;
    //        }

    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            rotation -= 60;
    //            UpdateRotation(rotation);

    //            //Debug.Log("Rotate: " + rotation);

    //        }
    //    }
    //}

    private void Update()
    {
        // Debug.Log(HexBuildTriggerCheck.spawnPositionLocation);

        //SPAWNING ENEMIES FOR TESTING
        if (Input.GetKeyDown(KeyCode.N))
        {
            //gameManager.StartSpawningEnemys();
            sortingArray.SpawnPreview();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameManager.StopAllCoroutines();
        }

        //SPAWN NEW CHUNK, GONNA CHANGE WHEN I ADD BUTTON
        if (Input.GetKeyDown(KeyCode.B) && !chunsIsSpawnd)
        {
            SpawnChunk();

        }
        
        //CHECKS IF A NEW CHUNK HAS BEN SPAWNED AND LETSE ME PLACE IT
        if (chunsIsSpawnd)
        {
            //moves the cgunk to the mouse position
            chunk.transform.position = GetMousePosition();

            ShootRayToCheck(chunk, HexBuildTriggerCheck.spawnPositionLocation);
            if ( GameManager.isExtendable && HexBuildTriggerCheck.spawnPositionLocation != Vector3.zero)
            {
                
                spawnBlocks = chunk.GetComponentsInChildren<Transform>();
                spawnBlocks = spawnBlocks.Where(child => child.tag == "buildHex").ToArray();

                


                if (Input.GetMouseButtonDown(0))
                {
                    foreach (Transform item in spawnBlocks)
                    {
                        item.gameObject.GetComponent<Collider>().enabled = true;
                    }
                    Debug.Log("HEREEEEE");
                    if (HexBuildTriggerCheck.isTuching)
                    {
                        Debug.Log("AAAAAAAAAAAAAAAA");
                        Vector3 spawnPosition = HexBuildTriggerCheck.spawnPositionLocation;
                        chunk.transform.position = spawnPosition;
                        GameManager.isExtendable = false;
                        //path = sortingArray.GenerateNewPath().ToArray();
                        //sortingArray.SpawnPreview();
                        chunsIsSpawnd = false;
                    }
                }
                #region garbage

                //foreach (Transform item in spawnBlocks)
                //{
                //    item.GetComponent<Collider>().enabled = true;
                //    Debug.Log("ENABLED COLLIDERS");

                //    //item.GetComponent<HexBuildTriggerCheck>().HasBeenPlaces();
                //    //if (item.GetComponent<HexBuildTriggerCheck>().isPlacedAndTouchingRoadOrBuildable)
                //    //{
                //    //    Destroy(item.gameObject);
                //    //}
                //    if (item.gameObject.GetComponent<HexBuildTriggerCheck>().isTuching)
                //    {
                //        Debug.Log("AAAAAAAAAAAAAAAA");
                //        Vector3 spawnPosition = item.gameObject.GetComponent<HexBuildTriggerCheck>().spawnPositionLocation;
                //        chunk.transform.position = spawnPosition;
                //        GameManager.isExtendable = false;
                //        path = sortingArray.GenerateNewPath().ToArray();
                //        Destroy(item.gameObject);
                //        chunsIsSpawnd = false;
                //    }
                //}


                //for (int i = 0; i < spawnBlocks.Length; i++)
                //{
                //    if (spawnBlocks[i].GetComponent<HexBuildTriggerCheck>().isTuching)
                //    {
                //        Debug.Log("AAAAAAAAAAAAAAAA");
                //        Vector3 spawnPosition = spawnBlocks[i].GetComponent<HexBuildTriggerCheck>().spawnPositionLocation;
                //        chunk.transform.position = spawnPosition;
                //        GameManager.isExtendable = false;
                //        path = sortingArray.GenerateNewPath().ToArray();
                //        Destroy(spawnBlocks[i].gameObject);
                //        chunsIsSpawnd = false;
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}
                //foreach (var item in spawnBlocks)
                //{

                //}
                //chunk.transform.position = HexBuildTriggerCheck.spawnPositionLocation;

                //GameManager.isExtendable = false;



                //spawnBlocks = chunk.GetComponentsInChildren<Transform>();
                //spawnBlocks = spawnBlocks.Where(child => child.tag == "buildHex").ToArray();

                //foreach (Transform item in spawnBlocks)
                //{
                //    item.GetComponent<Collider>().enabled = true;
                //    item.GetComponent<HexBuildTriggerCheck>().HasBeenPlaces();
                //    //if (item.GetComponent<HexBuildTriggerCheck>().isPlacedAndTouchingRoadOrBuildable)
                //    //{
                //    //    Destroy(item.gameObject);
                //    //}

                //}

                //path = sortingArray.GenerateNewPath().ToArray();
                //chunsIsSpawnd = false;

                //if (HexBuildTriggerCheck.spawnPositionLocation != Vector3.zero)
                //{
                //    HexBuildTriggerCheck[] temp = chunk.GetComponentsInChildren<HexBuildTriggerCheck>();
                //    foreach (var item in temp)
                //    {
                //        if (item.isTuchingForDeleat)
                //        {
                //            Destroy(item.gameObject);
                //        }
                //    }
                //    HexBuildTriggerCheck.spawnPositionLocation = Vector3.zero;
                //}
                //HexBuildTriggerCheck.spawnPositionLocation = Vector3.zero;
                #endregion
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                rotation -= 60;
                UpdateRotation(rotation);

                //Debug.Log("Rotate: " + rotation);

            }
        }
    }

    private bool ReturnPrviewResoult(GameObject prefab)
    {
        bool returnBool = false;
        GameObject clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        RotationChecker[] rc = clone.GetComponentsInChildren<RotationChecker>();

        foreach (RotationChecker item in rc)
        {
            item.gameObject.GetComponent<Collider>().enabled = true;
        }
        clone.transform.position = HexBuildTriggerCheck.spawnPositionLocation;
        foreach (RotationChecker item in rc)
        {
            Debug.Log(item.correctAligmentOfRoads);
        }



        //Destroy(clone);
        return returnBool;
    }
    private void SpawnChunk()
    {
        GameObject[] pads = GameObject.FindGameObjectsWithTag("buildHex");
        foreach (GameObject item in pads)
        {
            if (item.GetComponent<HexBuildTriggerCheck>().isTuchingLocal)
            {
                Destroy(item.gameObject);
            }
        }
        HexBuildTriggerCheck.spawnPositionLocation = Vector3.zero;
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

    private void ShootRayToCheck(GameObject hex, Vector3 spawnPosition)
    {
        //GameObject hexCopy = Instantiate(hex, spawnPosition, hex.transform.rotation);
        RotationChecker[] rotationChacker = hex.GetComponentsInChildren<RotationChecker>();
        Debug.DrawRay(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), Color.red);
        RaycastHit hit;
        if (Physics.Raycast(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), out hit, 2f))
        {
            Debug.Log(hit.transform.name);
            //if (hit.transform.tag == "RoadConnect")
            //{
            //    Debug.Log("Touching");
            //}
        }
        //for (int i = 0; i < 6; i++)
        //{
        //    for (int j = 0; j < rotationChacker.Length; j++)
        //    {
        //        RaycastHit hit;
        //        if (Physics.Raycast(rotationChacker[j].transform.position, transform.TransformDirection(rotationChacker[j].transform.forward), out hit, 1f))
        //        {
        //            if (hit.transform.tag == "RoadConnect")
        //            {
        //                Debug.Log("Touching");
        //            }
        //        }

        //    }
        //}
    }
}
