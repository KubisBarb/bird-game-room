using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform imageRectTransform;
    private RectTransform parentRectTransform;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalImageLocalPosition;
    private Vector2 minDragPosition;
    private Vector2 maxDragPosition;

    bool focusingOnLocation = true;
    public RectTransform focusPointLocation;

    private void Awake()
    {
        imageRectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        CalculateDragBounds();
    }

    Vector3 RecalculateFocusPosition()
    {
        // Get the local position of the RectTransform
        Vector2 localPosition = focusPointLocation.localPosition;

        // Convert the local position to world position
        Vector3 worldPosition = focusPointLocation.TransformPoint(localPosition);

        return worldPosition;
    }

    Vector3 RecalculateIconPosition()
    {
        // Puts pointer click position into worlds space
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPosition;
    }

    /*private void OnDrawGizmos()
    {
        // Draw gizmos for the focus position
        Gizmos.color = Color.red;
        Vector3 focusPosition = RecalculateFocusPosition();
        Gizmos.DrawSphere(focusPosition, 0.1f);

        // Draw gizmos for the icon position
        Gizmos.color = Color.green;
        Vector3 iconPosition = RecalculateIconPosition();
        Gizmos.DrawSphere(iconPosition, 0.1f);

        // Draw a line between focus and icon positions
        Gizmos.color = Color.white;
        Gizmos.DrawLine(focusPosition, iconPosition);
    }*/

    private void Update()
    {
        /*if (focusingOnLocation)
        {
            // Calculate the vector from the original location to the desired location, wanted - original
            Vector3 offset = RecalculateFocusPosition() - RecalculateIconPosition();

            // Move the object instantly to the desired location
            imageRectTransform.position += offset;

            Debug.Log(offset);

            focusingOnLocation = false;
        }*/
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

    public void FocusOnLocation()
    {
        //focusingOnLocation = true;
    }

}