using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject[] crystalPrefabs;
    [SerializeField] ProjectileScriptebelObject[] projectileSOs;
    private int projectileIndex;

    private Vector3 projectileShootFromPosition; //Projectile spawn area
    
    private float shootTimerMax;
    private float shootTimer;

    private GameObject lookAtEnemyForParticles;
    private GameObject crystalSpawnArea;
    private ParticleSystem particles;
    

    [SerializeField] private bool hasCrystal = false;
   

    enum CrystalsEnum
    {
        FireCrystal = 0,
        WaterCrystal = 1
    }

    private void Awake()
    {
        //projectileShootFromPosition = transform.Find("Crystal").position;
        lookAtEnemyForParticles = transform.Find("LookAtEnemy").gameObject;
        crystalSpawnArea = transform.Find("CrystalSpawnArea").gameObject;

        //Prepear the timer and timer reset amounts
        //shootTimerMax = projectileSO.projectileAttackSpeed;
        //shootTimer = shootTimerMax;
    }
    private void Start()
    {
        Debug.Log(projectileShootFromPosition);
        //SpawnParticles();
        //particles.Stop();
        projectileShootFromPosition = crystalSpawnArea.transform.position;
    }

    private void Update()
    {
        //??Change this ti IEnumerator later
        if (hasCrystal)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0f)
            {
                shootTimer = shootTimerMax;

                Enemy enemy = GetClosestEnemy();

                if (enemy == null)
                {
                    particles.Stop();
                }
                else 
                {
                    Projectile.Create(projectileShootFromPosition, enemy, projectileSOs[projectileIndex].projectileDmg, projectileSOs[projectileIndex].projectilePrefab);
                    lookAtEnemyForParticles.transform.LookAt(enemy.GetPosition());

                    particles.Play();
                    particles.transform.forward = lookAtEnemyForParticles.transform.forward;
                    
                }
                
            }
           
        }

        //This checks if there is a crystai in your hand and if you are pointing at a tower, and if you are it spawns a crystal and start shooting
        if (!hasCrystal)
        {
            //DragDrop.crystalInHand != null && 
            if (DragDrop.crystalInHand != null && GameManager.GetTurretHitInfo())
            {
                #region garbage
                //Instantiate(crystalPrefabs[1], crystalSpawnArea.transform.position, Quaternion.identity);
                //Destroy(DragDrop.crystalInHand);
                //projectileShootFromPosition = crystalSpawnArea.transform.position;
                //hasCrystal = true;

                //foreach (GameObject crystal in crystalPrefabs)
                //{
                //    if (DragDrop.crystalInHand.tag == crystal.tag)
                //    {
                //        Instantiate(crystal, crystalSpawnArea.transform.position, Quaternion.identity);
                //        Destroy(DragDrop.crystalInHand);
                //        projectileShootFromPosition = crystalSpawnArea.transform.position;
                //        hasCrystal = true;
                //        break;
                //    }
                //}
                #endregion
                GameObject thisObject = GameManager.GetTurretHitGameObject();
                thisObject.GetComponent<Tower>().SpawnCrystal();
            }
            else
            {
                return;
            }
        }
    }

    private Enemy GetClosestEnemy()
    {
        return Enemy.GetClosestEnemy(transform.position, projectileSOs[projectileIndex].projectileRange);
    }

    private void SpawnParticles()
    {
       particles = Instantiate(projectileSOs[projectileIndex].projectileParticles, lookAtEnemyForParticles.transform.position, Quaternion.identity);
    }

    //Function for spawning the crystal above the tower
    public void SpawnCrystal()
    {
        
        foreach (GameObject crystal in crystalPrefabs)
        {
            if (DragDrop.crystalInHand.tag == crystal.tag && !hasCrystal)
            {
                //Gets the current position in the array and uses that to get the criptable object it needs to
                int index = Array.IndexOf(crystalPrefabs, crystal);
                projectileIndex = index;
                shootTimerMax = projectileSOs[projectileIndex].projectileAttackSpeed;
                shootTimer = shootTimerMax;

                SpawnParticles();
                Instantiate(crystal, crystalSpawnArea.transform.position, Quaternion.identity);
                particles.Stop();
                Destroy(DragDrop.crystalInHand); //Destory the crystal sprite after spawning the crystal object
                projectileShootFromPosition = crystalSpawnArea.transform.position;
                hasCrystal = true;
                break;
            }
            
           
        }
    }

    
}
