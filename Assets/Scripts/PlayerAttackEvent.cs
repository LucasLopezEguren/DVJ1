using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayers;

    public int damage = 5;

    public PlayerController playerController;

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
    }

    public void ResetHitted()
    {
        playerController.ResetHitted();
    }
}
