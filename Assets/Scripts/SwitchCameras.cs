using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    public GameObject cameraFront;

    public GameObject cameraBack;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isFacingRight)
        {
            cameraFront.SetActive(true);
            cameraBack.SetActive(false);
        }
        else
        {
            cameraFront.SetActive(false);
            cameraBack.SetActive(true);
        }
    }
}
