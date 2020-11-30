using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrapFight : MonoBehaviour
{

    public List<SpawnEnemiesForTrapFight> spotsForSpawnEnemies;

    public float distanceForActivate = 5f;

    public GameObject cameraForFighting;

    private GameObject player;

    public GameObject UITrap;

    public Animator UITrapAnimator;

    private bool hasStarted = false;

    public List<GameObject> restrictionsForPlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) CheckToStart();
        else CheckSpawnEnemiesActives();
        if (spotsForSpawnEnemies.Count <= 0) FinishTrapFight();
        CheckPlayerGetRestrictedArea();
    }


    private void CheckToStart()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= distanceForActivate)
        {
            foreach (var spotForSpawnEnemies in spotsForSpawnEnemies)
            {
                spotForSpawnEnemies.gameObject.SetActive(true);
            }
            foreach (var restrictionToMovement in restrictionsForPlayerMovement)
            {
                restrictionToMovement.SetActive(true);
            }
            cameraForFighting.SetActive(true);
            UITrap.SetActive(true);
            hasStarted = true;
        }
    }

    private void CheckSpawnEnemiesActives()
    {
        if (spotsForSpawnEnemies.Count > 0)
        {
            if (spotsForSpawnEnemies[0] == null)
            {
                spotsForSpawnEnemies.RemoveAt(0);
            }
        }
    }

    private void FinishTrapFight()
    {
        cameraForFighting.SetActive(false);
        UITrapAnimator.SetTrigger("EndTrapFight");
        foreach (var restrictionToMovement in restrictionsForPlayerMovement)
        {
            restrictionToMovement.SetActive(false);
        }
    }

    private void CheckPlayerGetRestrictedArea()
    {
        foreach (var restrictedArea in restrictionsForPlayerMovement)
        {
            if (restrictedArea.activeSelf)
            {
                if (Vector3.Distance(restrictedArea.gameObject.GetComponent<Transform>().position, player.transform.position) <= 3)
                {
                    Vector3 direction = transform.position - player.transform.position;
                    direction.y = 0;
                    direction.z = 0;
                    player.GetComponent<Rigidbody>().AddForce(direction.normalized * 20, ForceMode.Impulse);
                }
            }

        }

    }

}
