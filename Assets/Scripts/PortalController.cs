using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    
    private DamageController damageController;

    public GameObject[] summoneablesEnemies;
    public GameObject forceFieldBack;
    public GameObject forceFieldFront;
    public float timeToSpawnEnemy;
    public float currentTime;
    
    void Start()
    {
        damageController = this.GetComponent<DamageController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CalculateHealth() <= 0) {

            gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
            forceFieldBack.SetActive(false);
            forceFieldFront.SetActive(false);
            Destroy(gameObject, 5f);
        } else {
            if (timeToSpawnEnemy < currentTime) {
                GameObject toInitiatie = summoneablesEnemies[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(summoneablesEnemies.Length)))];
                Vector3 spawnPosition = transform.position;
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
