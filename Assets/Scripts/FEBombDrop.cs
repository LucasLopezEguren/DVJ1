using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEBombDrop : MonoBehaviour
{
    public float bombSpeed = 5f;

    public float explosionRadius = 1f;

    public bool hasExploded = false;

    private Rigidbody rb;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-bombSpeed, 0, 0);
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
                nearbyObject.GetComponent<PlayerController>().TakeDamage(10);
            }
        }
        Destroy(gameObject);
    }
}
