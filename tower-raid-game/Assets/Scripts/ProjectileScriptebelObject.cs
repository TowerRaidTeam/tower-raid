using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Projectiles")]
public class ProjectileScriptebelObject : ScriptableObject
{
    public float projectileDmg;
    public float projectileRange;
    public float projectileAttackSpeed;
    public GameObject projectilePrefab;
    public ParticleSystem projectileParticles;
    
}
