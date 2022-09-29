using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SortingArray sortingArray;
    [SerializeField] private GameObject enemyWizard;
    [SerializeField] private Vector3 enemySpawnPosition;

    public static bool isExtendable = false;
    public static bool spawnEnemies = false;

    private void Start()
    {
        //sortingArray = FindObjectOfType<SortingArray>();
        //Vector3[] path = sortingArray.GenerateNewPath().ToArray();
        //enemySpawnPosition = path[path.Length];
        //StartCoroutine(SpawnEnemy());
    }

    public static bool  GetTurretHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static GameObject GetTurretHitGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void StartSpawningEnemys()
    {
        enemySpawnPosition = WorldGeneration.path[WorldGeneration.path.Length - 1] + Vector3.up;
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawningEnemys()
    {
        StopCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyWizard, enemySpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        
    }

    
}
