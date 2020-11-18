using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float rangeForChasing = 5f;

    public float attackRange = 1f;

    private Rigidbody rb;

    public GameObject healthBarUI;

    public Slider slider;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        damageController = this.GetComponent<DamageController>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
