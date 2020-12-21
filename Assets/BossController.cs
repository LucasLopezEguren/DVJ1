using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject idle;
    public GameObject closeAttacks;
    public GameObject moveHigh;
    public GameObject moveLow;
    public GameObject introSummon;
    public GameObject death;
    public Animator animator;
    public Transform _transform;
    public Collider rightCollider;
    public Collider leftCollider;
    public DamageController damageController;
    public GameObject portal;
    public List<string> possibleActions;
    public string preferedAction;

    public void SetPreferedAction (string action) {
        preferedAction = action;
        possibleActions.Add(action);
    }

    public void RemoveAction (string action) {
        possibleActions.RemoveAll(trigger => trigger == action);
    }

    void Start() {
        _transform = GetComponent<Transform>();    
    }

    private bool dead = false;
    private float actionCurrentTime = 0f;
    private float triggerActionTime = 3f;
    private float randomize = 0;
    private bool isActing = false;
    void Update()
    {
        if (dead) return;
        actionCurrentTime = actionCurrentTime + Time.deltaTime;
        if (actionCurrentTime >= (triggerActionTime - randomize) && !isActing) {
            TriggerAction();
        }
        if (damageController.CalculateHealth() <= 0 && !dead)
        {
            Die();
            dead = true;
            portal.SetActive(true);
        }
    }

    private void TriggerAction() {
        string action;
        isActing = true;
        try {
            List<string> actions = possibleActions;
            if (actions.Count == 0) {
                actions.Add(preferedAction);
            } else if (actions[actions.Count-1] != preferedAction) {
                actions.Add(preferedAction);
            }
            action = actions[Mathf.FloorToInt(UnityEngine.Random.Range(0f, (actions.Count-1)))];
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
            action = preferedAction;
        }
        if (action == "AttackMidHigh")
        {
            AttackMidHigh();
        }
        if (action == "AttackMidLow")
        {
            AttackMidLow();
        }
        if (action == "AttackClose")
        {
            AttackClose();
        }
        if (action == "MoveHigh")
        {
            MoveHigh();
        }
        if (action == "MoveLow")
        {
            MoveLow();
        }
        if (action == "Summon")
        {
            Summon();
        }
    }

    private void turnOff() 
    {
        idle.SetActive(false);
        closeAttacks.SetActive(false);
        moveHigh.SetActive(false);
        moveLow.SetActive(false);
        introSummon.SetActive(false);
        death.SetActive(false);
    }

    private void trunOffColliders() {
        leftCollider.enabled = false;
        rightCollider.enabled = false;
    }

    private void enableColliders() {
        Vector3 destination = idle.GetComponent<Transform>().position;
        if (destination.x < 0) {
            leftCollider.enabled = true;
            rightCollider.enabled = false;
        } else {
            rightCollider.enabled = true;
            leftCollider.enabled = false;
        }
    }

    private void AttackMidHigh() 
    {
        turnOff();
        closeAttacks.SetActive(true);
        Animator anim = closeAttacks.GetComponent<Animator>();
        anim.Play("BossHighAttack");
    }
    private void AttackMidLow() 
    {
        turnOff();
        closeAttacks.SetActive(true);
        Animator anim = closeAttacks.GetComponent<Animator>();
        anim.Play("BossLowAttack");
    }
    private void AttackClose() 
    {
        turnOff();
        closeAttacks.SetActive(true);
        Animator anim = closeAttacks.GetComponent<Animator>();
        anim.Play("CloseAttack");
    }
    private void MoveHigh() 
    {
        turnOff();
        trunOffColliders();
        moveHigh.SetActive(true);
        Vector3 destination = idle.GetComponent<Transform>().position;
        Animator anim = moveHigh.GetComponent<Animator>();
        if (destination.x < 0) {
            anim.Play("MoveHighLR");
        } else {
            anim.Play("MoveHighRL");
        }
    }
    private void MoveLow() 
    {
        turnOff();
        trunOffColliders();
        moveLow.SetActive(true);            
        Vector3 destination = idle.GetComponent<Transform>().position;
        Animator anim = moveLow.GetComponent<Animator>();
        anim.Play("MoveLow");
        if (destination.x < 0) {
            anim.Play("MoveLowLR");
        } else {
            anim.Play("MoveLowRL");
        }
    }
    private void Summon() 
    {
        turnOff();
        introSummon.SetActive(true);
        Animator anim = introSummon.GetComponent<Animator>();
        anim.Play("Intro");
    }
    private void Die() 
    {
        turnOff();
        death.SetActive(true);
        Animator anim = death.GetComponent<Animator>();
        anim.Play("Death");
    }

     public void AlertObservers(string message)
    {
        if (message.Equals("AnimationEnded"))
        {
            turnOff();
            idle.SetActive(true);
            Animator anim = idle.GetComponent<Animator>();
            anim.Play("Idle");
            actionCurrentTime = 0;
            randomize = UnityEngine.Random.Range(0f, 2f);
            isActing = false;
        }
        if (message.Equals("MoveEnded"))
        {
            float distance = 0;
            isActing = false;
            Vector3 destination = idle.GetComponent<Transform>().position;
            if (destination.x < 0) {
                distance = 8.3f;
            } else {
                distance = -8.3f;
            }
            actionCurrentTime = 0;
            randomize = UnityEngine.Random.Range(0f, 2f);
            
            idle.GetComponent<Transform>().position = idle.GetComponent<Transform>().position + _transform.forward * distance;
            idle.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            turnOff();
            idle.SetActive(true);
            Animator anim = idle.GetComponent<Animator>();
            anim.Play("Idle");
            
            closeAttacks.GetComponent<Transform>().position = closeAttacks.GetComponent<Transform>().position + _transform.forward * distance;
            closeAttacks.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            
            moveHigh.GetComponent<Transform>().position = moveHigh.GetComponent<Transform>().position + _transform.forward * distance;
            moveHigh.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            
            moveLow.GetComponent<Transform>().position = moveLow.GetComponent<Transform>().position + _transform.forward * distance;
            moveLow.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            
            introSummon.GetComponent<Transform>().position = introSummon.GetComponent<Transform>().position + _transform.forward * distance;
            introSummon.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            
            death.GetComponent<Transform>().position = death.GetComponent<Transform>().position + _transform.forward * distance;
            death.GetComponent<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            enableColliders();
        }
    }
}
