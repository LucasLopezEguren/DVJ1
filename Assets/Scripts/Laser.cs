using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public float speed = 10f;

    public Rigidbody rb;

    public int damage = 5;

    public bool hasExploded = false;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        if (transform.position.x < targetPlayer.position.x)
        {
            rb.velocity = new Vector3(speed, -speed, 0);
            transform.Rotate(0, 0, -45);
        }
        else
        {
            rb.velocity = new Vector3(-speed, -speed, 0);
            transform.Rotate(0, 0, 45);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.CompareTag("Player"))
            {
                nearbyObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
