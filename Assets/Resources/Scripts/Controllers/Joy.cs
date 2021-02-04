using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joy : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public float maxDistance;
    public Vector3 stickValue;
    public RectTransform stick;

    void Start()
    {
        stick.transform.localPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;
        stick.localPosition = Vector3.ClampMagnitude(stick.localPosition, maxDistance);
        stickValue = stick.localPosition / maxDistance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stick.localPosition = Vector3.zero;
        stickValue = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.position = eventData.position;

        stick.localPosition = Vector3.ClampMagnitude(stick.localPosition, maxDistance);
        stickValue = stick.localPosition / maxDistance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector3.zero;
        stickValue = Vector3.zero;
    }
}
