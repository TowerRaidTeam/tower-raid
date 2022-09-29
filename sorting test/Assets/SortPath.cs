using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SortPath : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] GameObject prefab;

    private void Start()
    {
        Debug.Log(GetAllHexesPositions().Length);
        Vector3[] loler = SortAllPositionFromFarToClose();
        StartCoroutine(SpawnPoints(prefab, loler));
        foreach (var item in SortAllPositionFromFarToClose())
        {
            Debug.Log("SORTED ARRAY TEXT: " + item);
        }
    }

    Vector3[] SortAllPositionFromFarToClose()
    {
        Vector3 startPos = startPosition.position;
        Vector3[] positions = GetAllHexesPositions();
        List<Vector3> positionCopy = GetAllHexesPositions().ToList();
        Vector3[] sortedArray = new Vector3[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            for (int j = 0; j < positionCopy.Count; j++)
            {
                if (MathF.Abs(Vector3.Distance(startPos, positions[i])) > MathF.Abs(Vector3.Distance(startPos, positionCopy[j])))
                {
                    positions[i] = positionCopy[j];
                    j = 0;
                }
                else
                {
                    continue;
                }
            }
            //Debug.Log("POSITION I : " + positions[i] + "COPY HAS VALUE: " + positionCopy.Contains(positions[i]));
            sortedArray[i] = positions[i];
            startPos = sortedArray[i];
            positionCopy.Remove(positions[i]);
            //foreach (var item in positionCopy)
            //{
            //    Debug.Log("PositionCopyValue: " + item);
            //}
            //Debug.Log("Count" + positionCopy.Count);
            
        }

        return sortedArray;
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
            yield return new WaitForSeconds(1);
        }
    }
}
