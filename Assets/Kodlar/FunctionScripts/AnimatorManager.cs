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
}
