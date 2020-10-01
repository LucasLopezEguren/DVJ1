using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;

    private GameObject enemy;
    private bool canAttack = false;

    public void Attack()
    {
        if(canAttack)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            canAttack = true;
            enemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            canAttack = false;
            enemy = other.gameObject;
        }
    }
}
