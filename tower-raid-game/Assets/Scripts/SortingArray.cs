
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SortingArray : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] GameObject prefab;

    

    //Vector3[] SortAllPositionFromFarToClose()
    //{
    //    Vector3 startPos = startPosition.position;
    //    Vector3[] positions = GetAllHexesPositions();
    //    List<Vector3> positionCopy = GetAllHexesPositions().ToList();
    //    Vector3[] sortedArray = new Vector3[positions.Length];


    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        for (int j = 0; j < positionCopy.Count; j++)
    //        {
    //            if (MathF.Abs(Vector3.Distance(startPos, positions[i])) > MathF.Abs(Vector3.Distance(startPos, positionCopy[j])))
    //            {
    //                Vector3 temp = positions[i];

    //                positions[i] = positionCopy[j];

    //                positions[j] = temp;

    //                //positions[i] = positionCopy[j];
    //                j = 0;
    //            }
    //            else if(MathF.Abs(Vector3.Distance(startPos, positions[i])) <= MathF.Abs(Vector3.Distance(startPos, positionCopy[j])))
    //            {
    //                continue;
    //            }
    //        }
    //        sortedArray[i] = positions[i];
    //        //positionCopy.Remove(sortedArray[i]);
    //        int index = positionCopy.IndexOf(sortedArray[i]);
    //        positionCopy[index] = Vector3.positiveInfinity;

    //        startPos = sortedArray[i];
    //        Debug.Log(i);
    //    }

    //    return sortedArray;
    //}
    public void SpawnPreview()
    {
        GameObject[] old = GameObject.FindGameObjectsWithTag("test");
        foreach (GameObject item in old)
        {
            Destroy(item);
        }

        StartCoroutine(SpawnPoints(prefab, SortAllPositionFromFarToClose()));
    }
    public List<Vector3> GenerateNewPath()
    {
        return SortAllPositionFromFarToClose().ToList();
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

    IEnumerator SpawnPoints(GameObject prefab, Vector3[] positions)
    {
        foreach (Vector3 item in positions)
        {
            Instantiate(prefab, item, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    Vector3[] SortAllPositionFromFarToClose()
    {
        Vector3 startPos = startPosition.position;
        Vector3[] positions = GetAllHexesPositions();
        //List<Vector3> positionCopy = GetAllHexesPositions().ToList();
        Vector3[] sortedArray = new Vector3[positions.Length];


        for (int i = 0; i < positions.Length; i++)
        {
            for (int j = 0; j < positions.Length; j++)
            {
                if (MathF.Abs(Vector3.Distance(startPos, positions[i])) > MathF.Abs(Vector3.Distance(startPos, positions[j])))
                {
                    if (sortedArray.Contains(positions[j]))
                    {
                        continue;
                    }
                    Vector3 temp = positions[i];
                    positions[i] = positions[j];
                    positions[j] = temp;
                    j = 0;
                }
                else if (MathF.Abs(Vector3.Distance(startPos, positions[i])) <= MathF.Abs(Vector3.Distance(startPos, positions[j])))
                {
                    continue;
                }
            }
            sortedArray[i] = positions[i];
            //int index = positionCopy.IndexOf(sortedArray[i]);
            //positionCopy[index] = Vector3.positiveInfinity;

            startPos = sortedArray[i];
            Debug.Log(i);
        }

        return sortedArray;
    }
}