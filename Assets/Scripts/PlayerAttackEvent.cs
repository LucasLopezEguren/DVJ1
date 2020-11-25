﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayers;

    public int damage = 5;

    public PlayerController playerController;

    public float knockbackStrength;

    public Transform shootPoint;
    public GameObject bulletPrefab;

    public GameObject playerrb;


    public void PlayerAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemyHitted in hitColliders)
        {
            try
            {
                if (!playerController.HasBeenHitted().Contains(enemyHitted.GetInstanceID()))
                {
                    enemyHitted.GetComponent<DamageController>().TakeDamage(damage);
                    playerController.AddHitted(enemyHitted.GetInstanceID());
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        Vector3 temp = new Vector3(25, 0, 0);
        playerrb.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
    }

    public void ResetHitted()
    {
        playerController.ResetHitted();
    }

    public void ThirdAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemyHitted in hitColliders)
        {
            try
            {
                if (!playerController.HasBeenHitted().Contains(enemyHitted.GetInstanceID()))
                {
                    enemyHitted.GetComponent<DamageController>().TakeDamage(damage);
                    playerController.AddHitted(enemyHitted.GetInstanceID());
                    KnockBack(enemyHitted);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        Vector3 temp = new Vector3(30, 0, 0);
        playerrb.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
    }

    void KnockBack (Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 direction = collider.transform.position - attackPoint.position;
            direction.y = 0;
            direction.z = 0;
            rb.AddForce(direction.normalized * knockbackStrength, ForceMode.Impulse);
        }
    }

    public void PlayerShoot ()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Vector3 temp = new Vector3(-15, 0, 0);
        playerrb.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
    } 
}
