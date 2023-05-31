using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform imageRectTransform;
    private RectTransform parentRectTransform;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalImageLocalPosition;
    private Vector2 minDragPosition;
    private Vector2 maxDragPosition;

    private void Awake()
    {
        imageRectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        CalculateDragBounds();
    }

    private void CalculateDragBounds()
    {
        if (imageRectTransform == null || parentRectTransform == null)
            return;

        // Calculate the allowed range of drag positions within the parent RectTransform
        Vector2 parentSize = parentRectTransform.rect.size;
        Vector2 imageSize = imageRectTransform.rect.size;

        minDragPosition = (parentSize - imageSize) * 0.5f;
        maxDragPosition = parentSize - minDragPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalImageLocalPosition = imageRectTransform.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (imageRectTransform == null || parentRectTransform == null)
            return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);
        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;

        // Calculate the new position within the allowed range
        Vector3 newPosition = originalImageLocalPosition + offsetToOriginal;
        newPosition.x = Mathf.Clamp(newPosition.x, minDragPosition.x, maxDragPosition.x - Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, minDragPosition.y, maxDragPosition.y - Screen.height);

        imageRectTransform.localPosition = newPosition;
    }
}
