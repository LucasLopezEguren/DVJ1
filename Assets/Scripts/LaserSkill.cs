using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkill : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody rb;

    public int damage = 7;

    public float TimeLeft = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider hitEnemy)
    {
        if (hitEnemy != null)
        {
            hitEnemy.GetComponent<DamageController>().TakeDamage(damage);
        }
    }

}
