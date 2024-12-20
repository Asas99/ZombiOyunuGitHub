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
    public void SetAllAnimatorBools()
    {
        foreach (AnimatorControllerParameter param in AlexAnimator.parameters)
        {
            AlexAnimator.SetBool(param.name, false);
        }
    }

    /// <summary>
    /// Belirtilen animator deðiþkeni dýþýnda tüm deðiþkenleri false yapar.
    /// </summary>
    /// <param name="excludeBool"> True yapýlacak deðiþken.</param>
    public void SetAllAnimatorBools(string excludeBool = null)
    {
        foreach (AnimatorControllerParameter param in AlexAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool && param.name != excludeBool)
            {
                AlexAnimator.SetBool(param.name, false);
            }
        }
        AlexAnimator.SetBool(excludeBool, true);
    }
}

