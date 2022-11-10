using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [SerializeField]EnemyScriptableObject enemyScriptableObject;
    //WorldGeneration worldGeneration;
    //SortingArray sortingArray;
    GameManager gameManager;
    private float enemyHp;
    private float enemySpeed;
    [SerializeField] private List<Vector3> pathVectorList = new List<Vector3>();
    private GameObject[] movePoints;
    private int curretnPathIndex;

    [SerializeField] Slider healthBar;
    [SerializeField] ParticleSystem poofParticles;


    public static List<Enemy> enemyList = new List<Enemy>();

    [SerializeField] GameObject enemyDeathSound;

    private void Awake()
    {
        enemyList.Add(this);
        Debug.Log(enemyList);
    }

    private void Start()
    {
        enemyHp = enemyScriptableObject.enemyHp;
        
        

        enemySpeed = enemyScriptableObject.enemySpeed;
        //worldGeneration = FindObjectOfType<WorldGeneration>();
        //sortingArray = FindObjectOfType<SortingArray>();
        gameManager = FindObjectOfType<GameManager>();

        enemyHp = gameManager.EnemyHpIncrees(enemyHp);
        healthBar.maxValue = enemyHp;
        healthBar.value = enemyHp;
        Debug.Log(enemyHp);
        //movePoints = GameObject.FindGameObjectsWithTag("Point");
        ////Debug.Log(movePoints);

        //foreach (GameObject point in movePoints)
        //{
        //    pathVectorList.Add(point.transform.position);
        //}
        pathVectorList = WorldGeneration.path.ToList();
        
        curretnPathIndex = WorldGeneration.path.Length - 1;
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
            if (enemy.IsDead()) { continue; } 
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
        Debug.Log(damageAmount);
        enemyHp -= damageAmount;
        UpdateHealthBar();

        if (enemyHp <= 0)
        {
            
            gameObject.GetComponent<Collider>().enabled = false;
            Instantiate(poofParticles, transform.position, Quaternion.identity);
            GameObject poof = Instantiate(enemyDeathSound, transform.position, Quaternion.identity);
            poof.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");

            gameManager.AddCash(15 + gameManager.harvest);
            gameManager.UpdateCash();
            
            
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
        gameManager.UpdateEnemyCounter(true);
        Debug.Log(gameManager.deadEnemys);
    }

    private void HandleMovment()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[curretnPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.25f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //Play animation here
                transform.position = transform.position + moveDir * enemySpeed * Time.deltaTime;

                transform.LookAt(targetPosition);
            }
            else
            {
                curretnPathIndex--;
                if (curretnPathIndex == 0)
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

    private void UpdateHealthBar()
    {
        healthBar.value = enemyHp;
    }
}
