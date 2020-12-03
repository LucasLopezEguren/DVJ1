using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForMenu : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("health", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
