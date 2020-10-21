using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEShooting : MonoBehaviour
{
    public float bulletSpeed = 5f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-bulletSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
    }
}
