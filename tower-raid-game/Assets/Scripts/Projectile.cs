using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] static GameObject prefab;

    private Enemy enemy;
    private float damageAmount;

    //Stativ methode udes for instantiating projectiles
    public static void Create(Vector3 spawnPosition, Enemy enemy, float damageAmount, GameObject projectilePrefab)
    {
        Transform projectileTransform = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity).transform;

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.Setup(enemy, damageAmount);
    }

   

    private void Setup(Enemy enemy, float damageAmount)
    {
        this.enemy = enemy;
        this.damageAmount = damageAmount;
    }

    private void Update()
    {
        //if there is ne enemy destory projectile
        if (enemy == null || enemy.IsDead())
        {
            // Enemy already dead
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = enemy.GetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float moveSpeed = 70f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float angle = GetAngleFromVectorFloat(moveDir);
        transform.eulerAngles = new Vector3(0, 0, angle);

        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
            enemy.Damage(damageAmount);
            Destroy(gameObject);
        }
    }


    //convrts vectors to float angles
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
