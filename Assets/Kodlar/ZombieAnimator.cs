using UnityEngine;
using UnityEngine.UI;

public class ZombieAnimator : MonoBehaviour
{
    public void PlayIdle(Animator animator)
    {
        AnimatorManager.SetAllAnimatorBools(animator);
    }
    public void PlayWalk(Animator animator)
    {
        AnimatorManager.SetAllAnimatorBools(animator, "IsWalking");
    }
    public void PlayAttack(Animator animator)
    {
        AnimatorManager.SetAllAnimatorBools(animator, "IsAttacking");
    }
    public void PlayDie(Animator animator)
    {
        AnimatorManager.SetAllAnimatorBools(animator, "IsDying");
    }
    public void PlayNeckBite(Animator animator)
    {
        AnimatorManager.SetAllAnimatorBools(animator, "IsBitingNeck");
    }
}
