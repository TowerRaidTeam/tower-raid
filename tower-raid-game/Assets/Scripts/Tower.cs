using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] GameObject[] crystalPrefabs;
    [SerializeField] ProjectileScriptebelObject[] projectileSOs;
    private int projectileIndex;

    private Vector3 projectileShootFromPosition; //Projectile spawn area
    
    [SerializeField] private float shootTimerMax;
    private float shootTimer;

    private GameObject lookAtEnemyForParticles;
    private GameObject crystalSpawnArea;
    private ParticleSystem particles;
    

    public bool hasCrystal = false;
    [SerializeField] private string[] allUpgradesTags;

    [SerializeField] float dmgUpgrade = 0;
    [SerializeField] float rangeUpgrade = 0;

    string crystalType;
    HexTypeRecogniser hexTypeRecogniser;

    [SerializeField] GameObject hexParticles;

    public GameObject buildCrystalButton;
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
        gm = FindObjectOfType<GameManager>();
        hexTypeRecogniser = GetComponent<HexTypeRecogniser>();
        Debug.Log(projectileShootFromPosition);
        //SpawnParticles();
        //particles.Stop();
        projectileShootFromPosition = crystalSpawnArea.transform.position;
    }

    private void Update()
    {
        //if (DragDrop.itemInHandUpgrade != null && GameManager.GetTurretHitInfo() && hasCrystal == true)
        //{
        //    switch (DragDrop.itemInHandUpgrade.tag)
        //    {
        //        case "DmgUpgrade": //Upgrade Tower Damage
        //            GetTurretYouAreHitting().GetComponent<Tower>().dmgUpgrade += 5;
        //            //dmgUpgrade += 5;
        //            gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
        //            gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
        //            gm.UpdateShopInventory();
        //            Destroy(DragDrop.itemInHandUpgrade);
        //            break;
        //        case "AttackSpeedUpgrade": //Upgrade Tower Attack Speed
        //            GetTurretYouAreHitting().GetComponent<Tower>().shootTimerMax -= shootTimerMax * 0.05f;
        //            //shootTimerMax -= shootTimerMax * 0.05f;
        //            gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
        //            gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
        //            gm.UpdateShopInventory();
        //            Destroy(DragDrop.itemInHandUpgrade);
        //            break;
        //        case "RangeUpgrade": //Upgrade Tower range
        //            GetTurretYouAreHitting().GetComponent<Tower>().rangeUpgrade += 1f;
        //            //rangeUpgrade += 10f;
        //            Debug.Log(projectileSOs[projectileIndex].projectileRange + rangeUpgrade);
        //            gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
        //            gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
        //            gm.UpdateShopInventory();
        //            Destroy(DragDrop.itemInHandUpgrade);
        //            break;
        //        default:
        //            Debug.Log("NOTHING TO UPGRADE");
        //            break;
        //    }
        //}
        //??Change this ti IEnumerator later
        if (hasCrystal)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0f)
            {
                shootTimer = shootTimerMax;

                Enemy enemy = GetClosestEnemy(rangeUpgrade);

                if (enemy == null)
                {
                    particles.Stop();
                }
                else 
                {
                    Debug.Log(shootTimerMax);
                    Projectile.Create(projectileShootFromPosition, enemy, projectileSOs[projectileIndex].projectileDmg + dmgUpgrade, projectileSOs[projectileIndex].projectilePrefab);
                    lookAtEnemyForParticles.transform.LookAt(enemy.GetPosition());

                    particles.Play();
                    particles.transform.forward = lookAtEnemyForParticles.transform.forward;
                    
                }
                
            }
           
        }

        //This checks if there is a crystai in your hand and if you are pointing at a tower, and if you are it spawns a crystal and start shooting
        //if (!hasCrystal)
        //{
        //    //DragDrop.crystalInHand != null && 
        //    if (DragDrop.crystalInHand != null && GameManager.GetTurretHitInfo())
        //    {
        //        #region garbage
        //        //Instantiate(crystalPrefabs[1], crystalSpawnArea.transform.position, Quaternion.identity);
        //        //Destroy(DragDrop.crystalInHand);
        //        //projectileShootFromPosition = crystalSpawnArea.transform.position;
        //        //hasCrystal = true;

        //        //foreach (GameObject crystal in crystalPrefabs)
        //        //{
        //        //    if (DragDrop.crystalInHand.tag == crystal.tag)
        //        //    {
        //        //        Instantiate(crystal, crystalSpawnArea.transform.position, Quaternion.identity);
        //        //        Destroy(DragDrop.crystalInHand);
        //        //        projectileShootFromPosition = crystalSpawnArea.transform.position;
        //        //        hasCrystal = true;
        //        //        break;
        //        //    }
        //        //}
        //        #endregion
        //        GameObject thisObject = GameManager.GetTurretHitGameObject();
        //        thisObject.GetComponent<Tower>().SpawnCrystal();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
    }

    private Enemy GetClosestEnemy(float rangeUpgrade)
    {
        return Enemy.GetClosestEnemy(transform.position, projectileSOs[projectileIndex].projectileRange + rangeUpgrade);
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
                Debug.Log(DragDrop.crystalInHand.transform.name);
                //GameManager.crystalIndexDeleated = int.Parse(DragDrop.crystalInHand.transform.name);
                gm.RefreshShopSlots(int.Parse(DragDrop.crystalInHand.transform.name));
                gm.spawnedCrystals.Remove(DragDrop.crystalInHand);
                gm.UpdateShopInventory();
                crystalType = DragDrop.crystalInHand.tag;
                if (crystalType == hexTypeRecogniser.hexType)
                {
                    dmgUpgrade += 10;
                    hexParticles.SetActive(true);
                    ParticleSystem ps = hexParticles.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.useUnscaledTime = true;
                    Debug.Log("DMG WAS UPGRADED BECAUSE OF THE HEX");
                }
                gm.TurnOnAllUpgradeButtons(false);
                gm.TurnOnMainCanvas(true);
                Destroy(DragDrop.crystalInHand); //Destory the crystal sprite after spawning the crystal object
                
                projectileShootFromPosition = crystalSpawnArea.transform.position;
                hasCrystal = true;
                
                break;
            }
        }
    }

    public GameObject GetTurretYouAreHitting()
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
                return hit.transform.gameObject;
            }
        }
        else
        {
            return hit.transform.gameObject;
        }
    }

    public void TurnOnUpgradeButton(bool isOn)
    {
        buildCrystalButton.SetActive(isOn);
    }

    public void DebugButtonTest()
    {
        Debug.Log("Button Working");
    }

    public void AddUpgradeToTowerWithButton()
    {
        if (DragDrop.itemInHandUpgrade != null && hasCrystal == true)
        {
            switch (DragDrop.itemInHandUpgrade.tag)
            {
                case "DmgUpgrade": //Upgrade Tower Damage
                    //GetTurretYouAreHitting().GetComponent<Tower>().dmgUpgrade += 5;
                    dmgUpgrade += 5;
                    gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
                    gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
                    gm.UpdateShopInventory();
                    gm.TurnOnAllUpgradeButtons(false);
                    gm.TurnOnMainCanvas(true);
                    Destroy(DragDrop.itemInHandUpgrade);
                    break;
                case "AttackSpeedUpgrade": //Upgrade Tower Attack Speed
                    //GetTurretYouAreHitting().GetComponent<Tower>().shootTimerMax -= shootTimerMax * 0.05f;
                    shootTimerMax -= shootTimerMax * 0.05f;
                    gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
                    gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
                    gm.UpdateShopInventory();
                    gm.TurnOnAllUpgradeButtons(false);
                    gm.TurnOnMainCanvas(true);
                    Destroy(DragDrop.itemInHandUpgrade);
                    break;
                case "RangeUpgrade": //Upgrade Tower range
                    //GetTurretYouAreHitting().GetComponent<Tower>().rangeUpgrade += 1f;
                    rangeUpgrade += 1f;
                    Debug.Log(projectileSOs[projectileIndex].projectileRange + rangeUpgrade);
                    gm.RefreshShopSlots(int.Parse(DragDrop.itemInHandUpgrade.transform.name));
                    gm.spawnedCrystals.Remove(DragDrop.itemInHandUpgrade);
                    gm.UpdateShopInventory();
                    gm.TurnOnAllUpgradeButtons(false);
                    gm.TurnOnMainCanvas(true);
                    Destroy(DragDrop.itemInHandUpgrade);
                    break;
                default:
                    Debug.Log("NOTHING TO UPGRADE");
                    break;
            }
        }
    }
}
