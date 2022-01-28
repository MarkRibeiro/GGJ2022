using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    private Vector3 _offset;
    private void Start() {
        _dragging = false;
    }
    private void LateUpdate() {
        if (_dragging) {
            transform.position = Input.mousePosition - _offset;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
        _offset = Input.mousePosition - transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
    }
}
