using UnityEngine;
using UnityEngine.UI;

public class ZombieAnimator : AnimatorManager
{
    public void PlayIdle()
    {
        SetAllAnimatorBools(AlexAnimator);
    }
    public void PlayWalk()
    {
        SetAllAnimatorBools(AlexAnimator, "IsWalking");
    }
    public void PlayAttack()
    {
        SetAllAnimatorBools(AlexAnimator, "IsAttacking");
    }
    public void PlayDie()
    {
        SetAllAnimatorBools(AlexAnimator, "IsDying");
    }
    public void PlayNeckBite()
    {
        SetAllAnimatorBools(AlexAnimator, "IsBitingNeck");
    }
}
