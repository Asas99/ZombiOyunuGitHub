using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class EmreAnimatorManager
{
    // Tum animator degiskenlerini false yapar.
    public static void SetAllAnimatorBools(Animator animator)
    {
        SetAllAnimatorBools(animator, null);
    }

    // Belirtilen animator degiskenleri disinda tum degiskenleri false yapar.
    // excludeBools: True yapilacak degiskenler.
    public static void SetAllAnimatorBools(Animator animator, params string[] excludeBools)
    {
        var excludeSet = excludeBools?.ToHashSet() ?? new HashSet<string>();

        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                bool targetValue = excludeSet.Contains(param.name);
                if (animator.GetBool(param.name) != targetValue) // Sadece gerektiginde degistir
                {
                    animator.SetBool(param.name, targetValue);
                }
            }
        }
    }
}
