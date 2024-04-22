using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUIAtBottomRight : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    [SerializeField] private float widthOffset = 0;
    [SerializeField] private float heightOffset = 0;

    private float width;
    private float height;

    private void Start()
    {
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
    }

    private void Update()
    {
        rectTransform.anchoredPosition = new Vector2(-((width + widthOffset) / 2), (height + heightOffset) / 2);
    }
}
