using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody rb;

    public int damage = 12;

    public float TimeLeft = 15;

    public GameObject grenadeParticles;

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
            GameObject newGrenadeParticles = Instantiate(grenadeParticles, transform.position, Quaternion.identity);
            newGrenadeParticles.GetComponent<GrenadeParticles>().collisionEnemy = hitEnemy.GetComponent<GameObject>();
            Destroy(gameObject);
        }
    }

}
