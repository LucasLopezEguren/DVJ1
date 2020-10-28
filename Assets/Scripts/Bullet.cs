using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter (Collider hitEnemy)
    {
        hitEnemy.GetComponent<DamageController>().TakeDamage(damage);
        Destroy(gameObject);
    }

}
