using UnityEngine;
using UnityEngine.UI;

public class AnimatorManager : MonoBehaviour
{
    [Space(10)]
    [Header("Animatör")]
    public Animator AlexAnimator;

    /// <summary>
    /// Tüm animatör deðiþkenlerini false yapar.
    /// </summary>
    public void SetAllAnimatorBools(Animator animator)
    {
        foreach (AnimatorControllerParameter param in AlexAnimator.parameters)
        {
            animator.SetBool(param.name, false);
        }
    }

    /// <summary>
    /// Belirtilen animator deðiþkeni dýþýnda tüm deðiþkenleri false yapar.
    /// </summary>
    /// <param name="excludeBool"> True yapýlacak deðiþken.</param>
    public void SetAllAnimatorBools(Animator animator,string excludeBool = null)
    {
        foreach (AnimatorControllerParameter param in AlexAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool && param.name != excludeBool)
            {
                animator.SetBool(param.name, false);
            }
        }
        animator.SetBool(excludeBool, true);
    }
}

