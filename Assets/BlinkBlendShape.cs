using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlinkBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int blinkBlendShapeIndex = 0;

    public float blinkDuration = 0.1f;
    public float minBlinkInterval = 2f;
    public float maxBlinkInterval = 6f;

    public List<int> expressionBlendShapeIndices = new List<int>(); // Diðer yüz ifadeleri için

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
        StartCoroutine(ExpressionRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(AnimateBlendShape(blinkBlendShapeIndex, 0f, 100f, blinkDuration));
            yield return StartCoroutine(AnimateBlendShape(blinkBlendShapeIndex, 100f, 0f, blinkDuration));
        }
    }

    IEnumerator ExpressionRoutine()
    {
        while (true)
        {
            if (expressionBlendShapeIndices.Count > 0)
            {
                int index = expressionBlendShapeIndices[Random.Range(0, expressionBlendShapeIndices.Count)];
                float targetWeight = Random.Range(30f, 80f);
                float duration = Random.Range(0.5f, 1.5f);

                yield return StartCoroutine(AnimateBlendShape(index, 0f, targetWeight, duration));
                yield return new WaitForSeconds(Random.Range(1f, 3f));
                yield return StartCoroutine(AnimateBlendShape(index, targetWeight, 0f, duration));
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator AnimateBlendShape(int index, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float value = Mathf.Lerp(start, end, elapsed / duration);
            skinnedMeshRenderer.SetBlendShapeWeight(index, value);
            elapsed += Time.deltaTime;
            yield return null;
        }
        skinnedMeshRenderer.SetBlendShapeWeight(index, end);
    }
}
