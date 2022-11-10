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
    [SerializeField] LayerMask buildRotaionLayer;

    private GameObject chunk;
    
    private bool chunsIsSpawnd = false;

    private int rotation;
    private Transform[] spawnBlocks;


    public static Vector3[] path;

    [SerializeField] AudioSource hexBuildSound;
    private void Start()
    {
        sortingArray = FindObjectOfType<SortingArray>();
        gameManager = FindObjectOfType<GameManager>();
    }

    bool isTuchingPrivate;
    bool isAlignedCorrectlyToPlace = false;
    [SerializeField] int rotationIndex = 0;
    [SerializeField] private GameObject shopCoverPanel;

    private int hexPrice = 50;

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
            //if (Input.GetMouseButtonDown(1))
            //{
            //    gameManager.cash += hexPrice;
            //    gameManager.UpdateCash();
            //    HexBuildTriggerCheck.isTuching = false;
            //    Destroy(chunk);
            //    shopCoverPanel.SetActive(false);
            //    chunsIsSpawnd = false;
            //}
            //moves the cgunk to the mouse position
            chunk.transform.position = GetMousePosition();
            
            if ( GameManager.isExtendable && HexBuildTriggerCheck.spawnPositionLocation != Vector3.zero)
            {
                
                spawnBlocks = chunk.GetComponentsInChildren<Transform>();
                spawnBlocks = spawnBlocks.Where(child => child.tag == "buildHex").ToArray();

                if (Input.GetMouseButtonDown(0))
                {
                    foreach (Transform item in chunk.transform)
                    {
                        if (item.GetComponentsInChildren<Collider>() != null)
                        {
                            Collider[] itemsBaby = item.GetComponentsInChildren<Collider>();
                            foreach (var cols in itemsBaby)
                            {
                                cols.enabled = false;
                            }
                        }
                        else
                        {
                            item.gameObject.GetComponent<Collider>().enabled = false;
                        }
                        
                    }
                    Vector3[] pos = ShootRayToCheck(chunk, HexBuildTriggerCheck.spawnPositionLocation);
                    foreach (var item in pos)
                    {
                        Debug.Log(item);
                    }

                    //Vector3[] pos = ShootRayToCheck(chunk, HexBuildTriggerCheck.spawnPositionLocation);
                    foreach (Vector3 item in pos)
                    {
                        if (item == chunk.transform.eulerAngles)
                        {
                            Debug.Log("CAN PLACE CORRECT");
                            foreach (Transform trans in spawnBlocks)
                            {
                                trans.gameObject.GetComponent<Collider>().enabled = true;
                            }
                            //Debug.Log("HEREEEEE");
                            if (HexBuildTriggerCheck.isTuching)
                            {
                                //MAYBE DELEAT THEM
                                foreach (Transform itemTwo in chunk.transform)
                                {
                                    if (itemTwo.GetComponentsInChildren<Collider>() != null)
                                    {
                                        Collider[] itemsBaby = itemTwo.GetComponentsInChildren<Collider>();
                                        foreach (var cols in itemsBaby)
                                        {
                                            cols.enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        itemTwo.gameObject.GetComponent<Collider>().enabled = true;
                                    }

                                }

                                foreach (RotationChecker rc in chunk.GetComponentsInChildren<RotationChecker>())
                                {
                                    Destroy(rc.gameObject);
                                }
                                // Debug.Log("AAAAAAAAAAAAAAAA");
                                shopCoverPanel.SetActive(false);
                                Vector3 spawnPosition = HexBuildTriggerCheck.spawnPositionLocation;
                                chunk.transform.position = spawnPosition;
                                GameManager.isExtendable = false;
                                //path = sortingArray.GenerateNewPath().ToArray();
                                //sortingArray.SpawnPreview();
                                hexBuildSound.volume = PlayerPrefs.GetFloat("volume");
                                hexBuildSound.Play();
                                chunsIsSpawnd = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Cant playce wrong rotation");
                        }
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

            //FIX LATER HAS LITTLE STOP AT ROTATION
            if (Input.GetKeyDown(KeyCode.R))
            {
                float[] correctRotation = {  -60, -120, -180, -240, -300, -360 };
                rotationIndex++;
                if (rotationIndex > 5)
                {
                    rotationIndex = 0;
                    chunk.transform.rotation = Quaternion.Euler(90, 0, correctRotation[rotationIndex]);
                }
                else
                {
                    chunk.transform.rotation = Quaternion.Euler(90, 0, correctRotation[rotationIndex]);
                }
                //rotation -= 60;
                //UpdateRotation(rotation);

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
    public void SpawnChunk()
    {
        if (gameManager.cash >= hexPrice)
        {
            gameManager.cash -= hexPrice;
            gameManager.UpdateCash();
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
            shopCoverPanel.SetActive(true);

            chunsIsSpawnd = true;
        }
        else
        {
            Debug.Log("DONT HAVE ENAUGH MONEY");
        }
        
    }

    public void SpawnChunkWithItem(int index)
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
        
        chunk = Instantiate(hexPrefabs[index], new Vector3(GetMousePosition().x, 0f, GetMousePosition().z), hexPrefabs[index].transform.rotation);
        shopCoverPanel.SetActive(true);

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

    private Vector3[] ShootRayToCheck(GameObject hex, Vector3 spawnPosition)
    {
        float[] correctRotation = {-60,-120,-180,-240,-300, -360};
        List<Vector3> allowedRotations = new List<Vector3>();
        Debug.Log(allowedRotations.Count + " SIZE AT THE START BABAY");
        GameObject hexCopy = Instantiate(hex, spawnPosition, hex.transform.rotation);
        RotationChecker[] rotationChacker = hexCopy.GetComponentsInChildren<RotationChecker>();
        Debug.DrawRay(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), Color.red);
        
        RaycastHit hit;
        #region garbage
        //RaycastHit hitMe;
        //if (Physics.Raycast(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), out hitMe, 2f, buildRotaionLayer))
        //{
        //    Debug.Log(hitMe.transform.name);
        //    //if (hit.transform.tag == "RoadConnect")
        //    //{
        //    //    Debug.Log("Touching");
        //    //}
        //}
        //WORKS
        #endregion
        for (int i = 0; i < 7; i++)
        {
            
            for (int j = 0; j < rotationChacker.Length; j++)
            {
                Ray ray = new Ray(rotationChacker[j].transform.position, transform.TransformDirection(rotationChacker[j].transform.forward));
                if (Physics.Raycast(ray, out hit, 1f, buildRotaionLayer))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.tag == "RoadConnect")
                    {
                        allowedRotations.Add(hexCopy.transform.eulerAngles);
                        Debug.Log("DISTANCE BABY " + hit.distance);
                        Debug.Log("NAME OF THE CULPRIT: " + hit.transform.name + " NAME OF THE PARENT" + hit.transform.gameObject.GetComponentInParent<Transform>().name);
                    }
                }
                else
                {
                    Debug.Log("not");
                }
            }
            hexCopy.transform.eulerAngles = new Vector3(hexCopy.transform.eulerAngles.x, hexCopy.transform.eulerAngles.y, correctRotation[0]);
        }
        Destroy(hexCopy);

        return allowedRotations.ToArray();
    }
    
    //IEnumerator ShootRayToCheck(GameObject hex, Vector3 spawnPosition)
    //{
    //    List<Vector3> allowedRotations = new List<Vector3>();
    //    GameObject hexCopy = Instantiate(hex, spawnPosition, hex.transform.rotation);
    //    RotationChecker[] rotationChacker = hexCopy.GetComponentsInChildren<RotationChecker>();
    //    Debug.DrawRay(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), Color.red);

    //    #region garbage
    //    //RaycastHit hitMe;
    //    //if (Physics.Raycast(rotationChacker[0].transform.position, transform.TransformDirection(rotationChacker[0].transform.forward), out hitMe, 2f, buildRotaionLayer))
    //    //{
    //    //    Debug.Log(hitMe.transform.name);
    //    //    //if (hit.transform.tag == "RoadConnect")
    //    //    //{
    //    //    //    Debug.Log("Touching");
    //    //    //}
    //    //}
    //    //WORKS
    //    #endregion
    //    for (int i = 0; i < 7; i++)
    //    {
    //        for (int j = 0; j < rotationChacker.Length; j++)
    //        {
    //            RaycastHit hit;
    //            if (Physics.Raycast(rotationChacker[j].transform.position, transform.TransformDirection(rotationChacker[j].transform.forward), out hit, 1f, buildRotaionLayer))
    //            {
    //                Debug.Log(hit.transform.name);
    //                if (hit.transform.tag == "RoadConnect")
    //                {
    //                    allowedRotations.Add(hexCopy.transform.eulerAngles);
    //                    Debug.Log(hexCopy.transform.eulerAngles);
    //                    Debug.Log("IN HERE BABAY GIRL");
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("not");
    //            }
    //        }
    //        yield return new WaitForSeconds(1);
    //        Debug.Log("ROTATION: " + i);
    //        hexCopy.transform.eulerAngles += new Vector3(0f, 0f, -60f);
            
    //    }
    //    Destroy(hexCopy);
    //    //return allowedRotations.ToArray();
    //}
}
