using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndAlert : MonoBehaviour
{
    public BossController bossController;
    public HitPlayer hitPlayer;
    public void AlertObservers(string message)
    {
        bossController.AlertObservers(message);
        if (hitPlayer != null) hitPlayer.SetReadyToAttack();
    }

    public void PlaySound(string message)
    {
        bossController.PlaySound(message);
    }
}
