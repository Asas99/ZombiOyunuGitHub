using UnityEngine;

public static class AnimatorManager
{
    /// <summary>
    /// T�m animat�r de�i�kenlerini false yapar.
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
    /// Belirtilen animator de�i�keni d���nda t�m de�i�kenleri false yapar.
    /// </summary>
    /// <param name="excludeBool">True yap�lacak de�i�ken.</param>
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
