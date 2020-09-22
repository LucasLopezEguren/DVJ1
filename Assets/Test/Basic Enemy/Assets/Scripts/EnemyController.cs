using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform targetPlayer;

    public float movementSpeed = 8.0f;

    public float distance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPlayer.position) >= distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, movementSpeed * Time.deltaTime);
        }      
        if (transform.position.x < targetPlayer.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
