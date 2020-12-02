using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    
    private DamageController damageController;

    public GameObject[] summoneablesEnemies;
    public GameObject forceFieldBack;
    public GameObject forceFieldFront;
    public GameObject portalUI;
    public float timeToSpawnEnemy;
    public float currentTime;
    public float secondsToShowAttackUI = 10;
    private float timer;
    private float previousHealth;

    void Start()
    {
        damageController = this.GetComponent<DamageController>();
        timer = secondsToShowAttackUI;
        previousHealth = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(previousHealth == CalculateHealth())
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                portalUI.SetActive(true);
            }
        }
        else
        {
            previousHealth = CalculateHealth();
            timer = secondsToShowAttackUI;
            portalUI.SetActive(false);
        }
        if (CalculateHealth() <= 0) {
            gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
            forceFieldBack.SetActive(false);
            forceFieldFront.SetActive(false);
            Destroy(gameObject, 5f);
        } else {
            if (timeToSpawnEnemy < currentTime) {
                GameObject toInitiatie = summoneablesEnemies[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(summoneablesEnemies.Length)))];
                Vector3 spawnPosition = transform.position;
                if (!toInitiatie.name.Contains("fly")) {
                    spawnPosition.y = spawnPosition.y + 3f;
                }
                Instantiate(toInitiatie, spawnPosition, Quaternion.identity);
                currentTime = 0f;
            }
            currentTime = currentTime + Time.deltaTime;
        }
    }

    private float CalculateHealth()
    {
        return damageController.health;
    }
}
