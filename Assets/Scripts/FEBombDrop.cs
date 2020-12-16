using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEBombDrop : MonoBehaviour
{
    public float bombSpeed = 5f;

    public float explosionRadius = 1f;

    public int damage = 10;

    public bool hasExploded = false;

    private Rigidbody rb;

    public GameObject explosionEffect;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        if(transform.position.x < targetPlayer.position.x)
        {
            rb.velocity = new Vector3(bombSpeed, bombSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector3(-bombSpeed, bombSpeed, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision collided)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if(nearbyObject.gameObject.tag == "Player")
            {
                nearbyObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
