using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1SwordHittingPlayer : MonoBehaviour
{
    private int damageToPlayer;
    private bool isAttacking = false;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        damageToPlayer = enemyController.damageToPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking = enemyController.IsAttacking();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isAttacking)
        {
            other.GetComponent<PlayerController>().TakeDamage(damageToPlayer);
        }
    }
}
