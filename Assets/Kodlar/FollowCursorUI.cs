using UnityEngine;
using UnityEngine.UI;

public class FollowCursorUI : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            Input.mousePosition,
            null,
            out mousePosition);

        rectTransform.anchoredPosition = mousePosition;
    }
}

