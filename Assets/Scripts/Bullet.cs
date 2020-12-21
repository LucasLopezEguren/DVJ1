using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody rb;

    public int damage = 5;

    public float TimeLeft = 5;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = (PlayerController)GameObject.Find("Player").GetComponent("PlayerController");
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider hitEnemy)
    {
        if(hitEnemy != null )
        {
            if (playerController.skillTree.skills.superBullets) hitEnemy.GetComponent<DamageController>().TakeDamage(damage * playerController.skillTree.skills.superBulletsMultiplier);
            else hitEnemy.GetComponent<DamageController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
