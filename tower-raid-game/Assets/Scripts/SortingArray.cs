using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SortingArray : MonoBehaviour
{
    [SerializeField] GameObject startingSpot;
    [SerializeField] GameObject spawnObject;
    GameObject[] hexess;

    private void Start()
    {
        //Debug.Log("Box to centure " + Vector3.Distance(new Vector3(0,0, 2.97f), new Vector3(-0.866000175f, -9.53674316e-07f, 7.50000048f)));
        //Debug.Log("Box to OTHER " + Vector3.Distance(new Vector3(0,0, 2.97f), new Vector3(0.00919103622f, 0, 8.9946146f)));
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject[] priprObjects = GameObject.FindGameObjectsWithTag("pathGeneratorTest");
            foreach (GameObject item in priprObjects)
            {
                Destroy(item.gameObject);
            }
            SpawnRoadMarker();
        }
    }
    public void SpawnRoadMarker()
    {
        Vector3[] temp = SortByDistanceToLast(GetAllHexesPositions());
        StartCoroutine(SpawnBlocks(temp, spawnObject));
    }

    Vector3[] SortPoitionArray(Vector3[] sorte)
    {
     
        List<Vector3> returnValuesList = sorte.ToList();
        List<Vector3> sortList = sorte.ToList();

        
        List<Vector3> sortedArray = new List<Vector3>();

       

        Vector3 startingPosition = startingSpot.transform.position;
        Vector3 startingPositionHolder = Vector3.zero;
        //Vector3[] sort = new Vector3[sorte.Length];
        for (int i = 0; i < sorte.Length; i++)
        {
         
            for (int j = 0; j > sorte.Length; j++)
            {
                
                //Debug.Log(j);
                if (Vector3.Distance(startingPosition, sortList[i]) > Vector3.Distance(startingPosition, returnValuesList[j]))
                {
                    if (sortedArray.Contains(returnValuesList[j]))
                    {
                        continue;
                    }
                    
                    startingPositionHolder = returnValuesList[j];
                    sortedArray.Add(returnValuesList[j]);

                }

            }
            startingPosition = startingPositionHolder;
            

        }
       
        return sortedArray.ToArray();
    }

    Vector3[] GetAllHexesPositions()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Road");
        Vector3[] positions = new Vector3[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            positions[i] = temp[i].transform.position;
        }
        return positions;
    }

    Vector3[] SortByDistanceToLast(Vector3[] sort)
    {
        sort = GetAllHexesPositions();
        Vector3[] sortTwo = GetAllHexesPositions();
        Debug.Log("SORT LENGHT: " + sort.Length);
        Vector3 startPos = startingSpot.transform.position;
        Vector3 startPosHolder = Vector3.zero;

        List<Vector3> sortList = sort.ToList();
        List<Vector3> sortListTow = sort.ToList();
        List<Vector3> sortedList = new List<Vector3>();

        Vector3[] returnValue = new Vector3[sort.Length];
        Vector3 holder = Vector3.zero;
        for (int i = 0; i < sort.Length; i++)
        {
            for (int j = 0; j < sort.Length; j++)
            {
                if (MathF.Abs(Vector3.Distance(startPos, sort[i])) < MathF.Abs(Vector3.Distance(startPos, sortTwo[j])))
                {
                    //if (returnValue.Contains(sortList[j]))
                    //{
                    //    //continue;
                    //    Debug.Log("already has it");
                    //}
                    //else
                    //{
                    //    startPosHolder = sortList[j];
                    //    sortedList.Add(sort[j]);
                    //    holder = startPosHolder;
                    //}
                    //continue;
                    //Debug.Log(j);
                    //if (sort.Contains(sortTwo[j]))
                    //{
                    //    //continue;
                    //    Debug.Log("already has it");
                    //}
                    //else
                    //{
                    //    startPosHolder = sort[j];
                    //    //sortedList.Add(sort[j]);
                    //    holder = startPosHolder;
                    //}

                    // Debug.Log(sortedList[0]);
                    //Debug.Log(holder);


                    //removeIndex = j;
                    //Debug.Log(j);
                }
                else if (MathF.Abs(Vector3.Distance(startPos, sort[i])) == MathF.Abs(Vector3.Distance(startPos, sortTwo[j])))
                {
                    continue;
                }
                else
                {
                    sort[i] = sortTwo[j];
                    j = 0;
                    
                }
            }
            //Debug.Log(i);
            startPos = startPosHolder;
            Debug.Log(holder);
            returnValue[i] = holder;
            //Debug.Log(startingSpot);
            //sortedList.Add(startPosHolder);
            //sortListTow.RemoveAt(removeIndex);
            //returnValue.Add(startPos);
        }
        //return sortedList.ToArray();
        return sort;
    }
    IEnumerator SpawnBlocks(Vector3[] temp, GameObject prefab)
    {
        foreach (Vector3 item in temp)
        {
            Instantiate(prefab, item, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}
