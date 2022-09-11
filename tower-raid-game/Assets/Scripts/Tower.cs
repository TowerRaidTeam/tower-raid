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
    private GameObject crystalSpawnArea;
    private ParticleSystem particles;
    

    [SerializeField] private bool hasCrystal = false;
   

    [SerializeField] GameObject[] crystalPrefabs;

    private void Awake()
    {
        //projectileShootFromPosition = transform.Find("Crystal").position;
        lookAtEnemyForParticles = transform.Find("LookAtEnemy").gameObject;
        crystalSpawnArea = transform.Find("CrystalSpawnArea").gameObject;
        shootTimerMax = projectileSO.projectileAttackSpeed;
        shootTimer = projectileSO.projectileAttackSpeed;
    }

    private void Start()
    {
        Debug.Log(projectileShootFromPosition);
        //SpawnParticles();
        //particles.Stop();
    }

    private void Update()
    {
        if (hasCrystal)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0f)
            {
                shootTimer = shootTimerMax;

                Enemy enemy = GetClosestEnemy();
                //Debug.Log(enemy.transform.name);
                if (enemy != null)
                {
                    //Debug.Log(enemy.transform.position);
                    Projectile.Create(projectileShootFromPosition, enemy, projectileSO.projectileDmg, projectileSO.projectilePrefab);
                    lookAtEnemyForParticles.transform.LookAt(enemy.GetPosition());

                    particles.Play();
                    particles.transform.forward = lookAtEnemyForParticles.transform.forward;
                }
                else
                {
                    //Debug.Log("Null bitch");
                    particles.Stop();
                }
            }
           
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            hasCrystal = true;
        }

        //This chech if there is a crystai in your hand and if youa a pointa at a tower, and if you are it spawns a crystal and start shooting
        if (!hasCrystal)
        {
            //DragDrop.crystalInHand != null && 
            if (DragDrop.crystalInHand != null &&  GameManager.GetTurretHitInfo())
            {
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
        return Enemy.GetClosestEnemy(transform.position, projectileSO.projectileRange);
    }

    private void SpawnParticles()
    {
       particles = Instantiate(projectileSO.projectileParticles, lookAtEnemyForParticles.transform.position, Quaternion.identity);
    }

    public void SpawnCrystal()
    {
        foreach (GameObject crystal in crystalPrefabs)
        {
            if (DragDrop.crystalInHand.tag == crystal.tag)
            {
                Instantiate(crystal, crystalSpawnArea.transform.position, Quaternion.identity);
                SpawnParticles();
                Destroy(DragDrop.crystalInHand);
                projectileShootFromPosition = crystalSpawnArea.transform.position;
                hasCrystal = true;
                break;
            }
        }
    }
    
}
