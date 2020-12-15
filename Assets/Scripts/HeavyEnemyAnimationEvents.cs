using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAnimationEvents : MonoBehaviour
{
    public HeavyEnemyController heavyEnemyController;

    public void StartHit()
    {
        heavyEnemyController.StartHit();
    }

    public void StopHit()
    {
        heavyEnemyController.StopHit();
    }

    public void CanFlip()
    {
        heavyEnemyController.CanFlip();
    }

    public void CantFlip()
    {
        heavyEnemyController.CantFlip();
    }

}
