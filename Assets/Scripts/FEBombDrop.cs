using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEBombDrop : MonoBehaviour
{
    public float bombSpeed = 5f;

    private Rigidbody rb;

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
}
