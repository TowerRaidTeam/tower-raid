using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHp = 100f;
    [SerializeField] private float enemySpeed = 30f;
    [SerializeField] private List<Vector3> pathVectorList = new List<Vector3>();
    private GameObject[] movePoints;
    private int curretnPathIndex;
    


    public static List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        enemyList.Add(this);
        //Debug.Log(enemyList);
    }

    private void Start()
    {
        movePoints = GameObject.FindGameObjectsWithTag("Point");
        //Debug.Log(movePoints);

        foreach (GameObject point in movePoints)
        {
            pathVectorList.Add(point.transform.position);
        }
    }

    private void Update()
    {
        HandleMovment();
    }

    //Returnes the coloses enemy to the tower
    public static Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        Enemy closest = null;
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.IsDead()) continue;
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                if (closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = enemy;
                    }
                }
            }
        }
        return closest;
    }

    public bool IsDead()
    {
        if (enemyHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage(float damageAmount)
    {
        enemyHp -= damageAmount;

        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void HandleMovment()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[curretnPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //Play animation here
                transform.position = transform.position + moveDir * enemySpeed * Time.deltaTime;

                transform.LookAt(targetPosition);
            }
            else
            {
                curretnPathIndex++;
                if (curretnPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
        else
        {
            //Debug.Log("NO TARGET");
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }
}
