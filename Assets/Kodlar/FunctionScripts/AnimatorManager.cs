using UnityEngine;
using UnityEngine.UI;

public class AnimatorManager : MonoBehaviour
{
    [Space(10)]
    [Header("Animat�r")]
    public Animator AlexAnimator;

    /// <summary>
    /// T�m animat�r de�i�kenlerini false yapar.
    /// </summary>
    public void SetAllAnimatorBools(Animator animator)
    {
        foreach (AnimatorControllerParameter param in AlexAnimator.parameters)
        {
            animator.SetBool(param.name, false);
        }
    }

    /// <summary>
    /// Belirtilen animator de�i�keni d���nda t�m de�i�kenleri false yapar.
    /// </summary>
    /// <param name="excludeBool"> True yap�lacak de�i�ken.</param>
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

