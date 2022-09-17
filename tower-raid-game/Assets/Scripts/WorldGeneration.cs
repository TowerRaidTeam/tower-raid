using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] hexPrefabs;
    [SerializeField] LayerMask layerMask;

    private GameObject chunk;
    private bool chunsIsSpawnd = false;

    private int rotation;
    private float rotationLenght;
    private float elapsedTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnChunk();
            
        }

        if (chunsIsSpawnd)
        {
            chunk.transform.position = GetMousePosition();
            if (Input.GetMouseButtonDown(0))
            {
                chunsIsSpawnd = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                rotation -= 60;
                UpdateRotation(rotation);
                
                Debug.Log("Rotate: " + rotation);
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
        chunk.transform.rotation = Quaternion.Euler(90, 0, chunk.transform.rotation.z - angle);
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
