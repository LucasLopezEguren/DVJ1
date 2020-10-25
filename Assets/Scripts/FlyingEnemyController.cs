using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    public int health;

    public int maxHealth = 100;

    private Rigidbody rb;

    public GameObject healthBarUI;

    public Slider slider;

    public GameObject bullet;

    public GameObject bloodSplash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-movementSpeed, 0, 0, ForceMode.Impulse);
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CalculateHealth();
        InvokeRepeating(nameof(ShootBullet), 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();
    }

    public void ShootBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = transform.position;

    }

    float CalculateHealth()
    {
        return health;
    }
}
