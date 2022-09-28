using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SortingArray : MonoBehaviour
{
    [SerializeField] GameObject startingSpot;
    GameObject[] hexess;

    private void Start()
    {

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnRoadMarker();
        }
    }
    public void SpawnRoadMarker()
    {
        Vector3[] temp = SortByDistanceToLast(GetAllHexesPositions());
        //foreach (var item in temp)
        //{
        //    Debug.Log(item);
        //}
        StartCoroutine(SpawnBlocks(temp));
    }

    Vector3[] SortPoitionArray(Vector3[] sorte)
    {
        Vector3[] returnValue = new Vector3[sorte.Length];
        List<Vector3> returnValuesList = sorte.ToList();
        List<Vector3> sortList = sorte.ToList();

        //int index = sorte.Length;
        //Debug.Log(index);


        int indexRemove = 0;
        Debug.Log("before for");

        Vector3 startingPosition = startingSpot.transform.position;
        Vector3 startingPositionHolder = Vector3.zero;
        //Vector3[] sort = new Vector3[sorte.Length];
        for (int i = 0; i < sorte.Length; i++)
        {
            Debug.Log("first for");
            for (int j = 0; j > sorte.Length; j++)
            {
                Debug.Log("second for");
                //Debug.Log(j);
                if (Vector3.Distance(startingPosition, sortList[i]) > Vector3.Distance(startingPosition, returnValuesList[j]))
                {
                    Debug.Log("in if");
                    //Vector3 temp = sorte[i];
                    //sorte[i] = sorte[j];
                    //sorte[j] = temp;
                    //i = -1;
                    //startingPosition = returnValuesList[j];
                    Debug.Log(startingPosition);
                    indexRemove = j;
                    startingPositionHolder = returnValuesList[j];

                }

            }
            //index--; 
            startingPosition = startingPositionHolder;
            returnValue[i] = startingPositionHolder;

            returnValuesList.RemoveAt(indexRemove);
            sortList.RemoveAt(i);

        }
        Debug.Log("Lenght " + returnValue.Length);
        return sortList.ToArray();
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
        Vector3 startPos = startingSpot.transform.position;
        Vector3 startPosHolder = Vector3.zero;

        List<Vector3> sortList = sort.ToList();
        List<Vector3> sortListTow = sort.ToList();
        List<Vector3> sortedList = new List<Vector3>();

        for (int i = 0; i < sort.Length; i++)
        {
            for (int j = 0; j < sort.Length; j++)
            {
                if (MathF.Abs(Vector3.Distance(startPos, sortList[i])) > MathF.Abs(Vector3.Distance(startPos, sortListTow[j])))
                {
                    if (sortedList.Contains(sortListTow[j]))
                    {
                        continue;
                    }
                    else
                    {
                        startPosHolder = sort[j];
                        sortedList.Add(sort[j]);
                        Debug.Log(sortedList[0]);
                    }

                    //removeIndex = j;
                }
            }
            startPos = startPosHolder;
            //sortListTow.RemoveAt(removeIndex);
            //returnValue.Add(startPos);
        }
        return sortedList.ToArray();
    }
    IEnumerator SpawnBlocks(Vector3[] temp)
    {
        foreach (Vector3 item in temp)
        {
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), item, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}
