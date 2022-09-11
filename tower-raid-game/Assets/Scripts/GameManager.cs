using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyWizard;
    [SerializeField] private Transform enemySpawnPosition;


    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyWizard, enemySpawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        
    }
}
