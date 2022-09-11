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

    private GameObject lookAtEnemyForParticles;
    private ParticleSystem particles;
    private bool isShooting;

    private void Awake()
    {
        projectileShootFromPosition = transform.Find("Crystal").position;
        lookAtEnemyForParticles = transform.Find("LookAtEnemy").gameObject;
        shootTimerMax = projectileSO.projectileAttackSpeed;
        shootTimer = projectileSO.projectileAttackSpeed;
    }

    private void Start()
    {
        Debug.Log(projectileShootFromPosition);
        SpawnParticles();
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
                lookAtEnemyForParticles.transform.LookAt(enemy.GetPosition());

                particles.Play();
                particles.transform.forward = lookAtEnemyForParticles.transform.forward;
            }
            else
            {
                Debug.Log("Null bitch");
                particles.Stop();
            }


        }
    }

    private Enemy GetClosestEnemy()
    {
        return Enemy.GetClosestEnemy(transform.position, projectileSO.projectileRange);
    }

    private void SpawnParticles()
    {
       particles = Instantiate(projectileSO.projectileParticles, lookAtEnemyForParticles.transform.position, Quaternion.identity);
    }

    

}
