using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills 
{

    public bool shield = false;

    public bool rage = false;

    public bool hittingDash = false;

    public bool doubleJump = false;

    public bool superBullets = false;

    public bool grenade = false;

    public bool laser = false;

    [Header("Shield")]
    public float timeOfShield = 10f;

    public bool shieldActive = false;

    [HideInInspector]
    public float timerShield = 0;

    [Header("Rage")]
    public float timeOfRage = 10f;

    public bool rageActive = false;

    public int rageMultiplier = 2;

    [HideInInspector]
    public float timerRage = 0;

    public int SkilsActivated()
    {
        int toReturn = 0;
        if (shield) toReturn++;
        if (rage) toReturn++;
        if (hittingDash) toReturn++;
        if (doubleJump) toReturn++;
        if (superBullets) toReturn++;
        if (grenade) toReturn++;
        if (laser) toReturn++;
        return toReturn;
    }

    public int SkillsActivatedThatNeedToBeSelected()
    {
        int toReturn = 0;
        if (shield) toReturn++;
        if (rage) toReturn++;
        if (grenade) toReturn++;
        if (laser) toReturn++;
        return toReturn;
    }
}
