using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    public enum TypeOfShoot
    {
        normal,
        grenade,
        laser
    }

    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayers;

    public int damage = 5;

    public PlayerController playerController;

    public float knockbackStrength;

    public Transform shootPoint;

    public GameObject bulletPrefab;

    public GameObject grenadePrefab;

    public GameObject laserPrefab;

    public GameObject player;

    [HideInInspector]
    public TypeOfShoot typeOfShoot;

    private bool right = true;

    private AudioManager audioManager;

    public void PlayerAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemyHitted in hitColliders)
        {
            try
            {
                if (!playerController.HasBeenHitted().Contains(enemyHitted.GetInstanceID()))
                {
                    if (!playerController.skillTree.skills.rageActive) enemyHitted.GetComponent<DamageController>().TakeDamage(damage);
                    else enemyHitted.GetComponent<DamageController>().TakeDamage(damage * playerController.skillTree.skills.rageMultiplier);
                    playerController.AddHitted(enemyHitted.GetInstanceID());
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        Vector3 temp = new Vector3(25, 0, 0);
        player.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
        audioManager = (AudioManager)GameObject.Find("AudioManager").GetComponent("AudioManager");
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
                    if (!playerController.skillTree.skills.rageActive) enemyHitted.GetComponent<DamageController>().TakeDamage(damage);
                    else enemyHitted.GetComponent<DamageController>().TakeDamage(damage * playerController.skillTree.skills.rageMultiplier);
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
        player.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
    }

    void KnockBack(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = collider.transform.position - attackPoint.position;
            direction.y = 0;
            direction.z = 0;
            rb.AddForce(direction.normalized * knockbackStrength, ForceMode.Impulse);
        }
    }

    public void PlayerShoot()
    {
        if (typeOfShoot == TypeOfShoot.normal)
        {
            right = player.GetComponent<PlayerController>().isFacingRight;
            if (right)
            {
                Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 180, 0));
            }
            Vector3 temp = new Vector3(-15, 0, 0);
            player.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
        }
        if(typeOfShoot == TypeOfShoot.grenade)
        {
            right = player.GetComponent<PlayerController>().isFacingRight;
            if (right)
            {
                Instantiate(grenadePrefab, shootPoint.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(grenadePrefab, shootPoint.position, Quaternion.Euler(0, 180, 0));
            }
            Vector3 temp = new Vector3(-15, 0, 0);
            player.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
        }
        if (typeOfShoot == TypeOfShoot.laser)
        {
            right = player.GetComponent<PlayerController>().isFacingRight;
            if (right)
            {
                Instantiate(laserPrefab, shootPoint.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(laserPrefab, shootPoint.position, Quaternion.Euler(0, 180, 0));
            }
            Vector3 temp = new Vector3(-15, 0, 0);
            player.GetComponent<Rigidbody>().AddRelativeForce(temp, ForceMode.Impulse);
        }
    }

    public void StartDashing()
    {
        playerController.isDashing = true;
    }

    public void StopDashing()
    {
        playerController.isDashing = false;
    }

    public void PlayFirstAttackSound()
    {
        audioManager.Play("Player_Attack1");
    }

    public void PlaySecondAttackSound()
    {
        audioManager.Play("Player_Attack2");
    }

    public void PlayThirdAttackSound()
    {
        audioManager.Play("Player_Attack3");
    }

    public void PlayFall()
    {
        audioManager.Play("Player_Fall");
    }

    public void PlayDash()
    {
        audioManager.Play("Player_Dash");
    }

    public void PlayShoot()
    {
        audioManager.Play("Player_Shoot");
    }

    public void PlayHit1()
    {
        audioManager.Play("Player_Hit1");
    }

    public void PlayHit2()
    {
        audioManager.Play("Player_Hit2");
    }

    public void PlayHit3()
    {
        audioManager.Play("Player_Hit3");
    }

    public void PlayStep1()
    {
        audioManager.Play("Player_Step1");
    }

    public void PlayStep2()
    {
        audioManager.Play("Player_Step2");
    }

}
