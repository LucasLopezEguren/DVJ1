using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeParticles : MonoBehaviour
{
    public int damage = 4;

    [HideInInspector]
    public GameObject collisionEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy" && other != collisionEnemy)
        {
            other.GetComponent<DamageController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
