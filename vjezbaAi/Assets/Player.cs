using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider slider;
    float hp = 100;
    

    private void Start()
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BigEnemy")
        {
            TakeDamage(25);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "MidEnemy")
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SmallEnemy")
        {
            TakeDamage(15);
            Destroy(collision.gameObject);
        }
    }


    void TakeDamage(int enemyDmg)
    {
        hp -= enemyDmg;
        slider.value = hp;
    }
}
