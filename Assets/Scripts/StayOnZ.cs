using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnZ : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.position.Set(transform.position.x, transform.position.y , 0f);
    }
}
