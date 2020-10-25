using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int damage = 5;

    public void PlayerAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider hitEnemy in hitColliders) {
            try {
                hitEnemy.GetComponent<DamageController>().TakeDamage(damage);
            } catch (System.Exception e) {
                Debug.Log(e.Message);
            }
        }
    }
}
