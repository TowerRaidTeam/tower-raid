using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public ProjectileScriptebelObject projectileSO;
    [SerializeField] GameObject cubeLol;

    private Vector3 projectileShootFromPosition;
    private float shootTimerMax;
    private float shootTimer;

    private void Awake()
    {
        projectileShootFromPosition = transform.Find("Crystal").position;
        shootTimerMax = projectileSO.projectileAttackSpeed;
        shootTimer = projectileSO.projectileAttackSpeed;
    }

    private void Start()
    {
        Debug.Log(projectileShootFromPosition);
        
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            Enemy enemy = GetClosestEnemy();
            //Debug.Log(enemy.transform.name);
            if (enemy != null)
            {
                Debug.Log(enemy.transform.position);
                Projectile.Create(projectileShootFromPosition, enemy, projectileSO.projectileDmg, projectileSO.projectilePrefab);
          
            }
            else
            {
                Debug.Log("Null bitch");
            }
        }
    }

    private Enemy GetClosestEnemy()
    {
        return Enemy.GetClosestEnemy(transform.position, 30f);
    }

    

}
