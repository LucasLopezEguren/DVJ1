using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyHit : MonoBehaviour
{
    private HeavyEnemyController heavyEnemyController;

    private int damageToPlayer;

    private bool canHit = false;

    private bool hasHitted = false;

    // Start is called before the first frame update
    void Start()
    {
        heavyEnemyController = GetComponentInParent<HeavyEnemyController>();
        damageToPlayer = heavyEnemyController.damageToPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        canHit = heavyEnemyController.CanHit();
        if(hasHitted && !canHit)
        {
            hasHitted = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canHit && !hasHitted)
        {
            hasHitted = true;
            other.GetComponent<PlayerController>().TakeDamage(damageToPlayer);            
        }
    }

}
