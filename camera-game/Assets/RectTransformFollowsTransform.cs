using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RectTransformFollowsTransform : MonoBehaviour
{
    public Transform worldTransform;
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = worldTransform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        Vector2 bottomLeft = new Vector2(canvasRectTransform.rect.xMin, canvasRectTransform.rect.yMin);
        Vector2 canvasPosition = bottomLeft + screenPosition;
        rectTransform.anchoredPosition = canvasPosition;
    }
}
