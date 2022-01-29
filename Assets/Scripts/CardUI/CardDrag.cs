using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    private Vector3 _offset;
    private Vector3 _startPosition;
    private int _startIndex;
    private Transform _startParent;
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
        Debug.Log(transform.name + " was clicked.");
        _dragging = true;
        _offset = Input.mousePosition - transform.position;
        _startPosition = transform.position;
        _startIndex = transform.GetSiblingIndex();
        _startParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
        transform.position = _startPosition;
        transform.SetParent(_startParent);
        transform.SetSiblingIndex(_startIndex);
    }
}
