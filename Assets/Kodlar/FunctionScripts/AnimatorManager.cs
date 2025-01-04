using System.Linq;
using UnityEngine;

public static class AnimatorManager
{
    /// <summary>
    /// Tüm animatör deðiþkenlerini false yapar.
    /// </summary>
    public static void SetAllAnimatorBools(Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(param.name, false);
            }
        }
    }

    /// <summary>
    /// Belirtilen animator deðiþkeni dýþýnda tüm deðiþkenleri false yapar.
    /// </summary>
    /// <param name="excludeBool">True yapýlacak deðiþken.</param>
    public static void SetAllAnimatorBools(Animator animator, string excludeBool = null)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool && param.name != excludeBool)
            {
                animator.SetBool(param.name, false);
            }
        }

        if (!string.IsNullOrEmpty(excludeBool))
        {
            animator.SetBool(excludeBool, true);
        }
    }

    /// <summary>
    /// Belirtilen animator deðiþkenleri dýþýnda tüm deðiþkenleri false yapar.
    /// </summary>
    /// <param name="excludeBools">True yapýlacak deðiþkenler.</param>
    public static void SetAllAnimatorBools(Animator animator, params string[] excludeBools)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool &&
                (excludeBools == null || !excludeBools.Contains(param.name)))
            {
                animator.SetBool(param.name, false);
            }
        }

        if (excludeBools != null)
        {
            foreach (string excludeBool in excludeBools)
            {
                if (!string.IsNullOrEmpty(excludeBool))
                {
                    animator.SetBool(excludeBool, true);
                }
            }
        }
    }
}
