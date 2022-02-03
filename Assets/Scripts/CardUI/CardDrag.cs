using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    RectTransform playArea;
    [SerializeField]
    UnityEvent onPlay;
    private bool _dragging;
    private Vector3 _offset;
    private Vector3 _startPosition;
    private int _startIndex;
    private Transform _startParent;
    private CardAnimation _anim;
    private RectTransform rectTransform;
    private void Start()
    {
        _dragging = false;
        _anim = GetComponent<CardAnimation>();
        playArea = GameObject.FindGameObjectWithTag("PlayArea").GetComponent<RectTransform>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        Assert.IsNotNull(playArea);
        Assert.IsNotNull(rectTransform);
    }
    private void LateUpdate()
    {
        if (_dragging)
        {
            transform.position = Input.mousePosition - _offset;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
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
        var mousePosition = Input.mousePosition;
        var normalizedMousePosition = new Vector2(rectTransform.position.x / Screen.width, (rectTransform.position.y + (rectTransform.rect.height / 2.0f)) / Screen.height);
        Debug.Log(normalizedMousePosition.x);
        Debug.Log(normalizedMousePosition.y);
        Debug.Log(playArea.anchorMin.x);
        Debug.Log(playArea.anchorMax.x);
        Debug.Log(playArea.anchorMin.y);
        Debug.Log(playArea.anchorMax.y);


        if (normalizedMousePosition.x > playArea.anchorMin.x &&
            normalizedMousePosition.x < playArea.anchorMax.x &&
            normalizedMousePosition.y > playArea.anchorMin.y &&
            normalizedMousePosition.y < playArea.anchorMax.y)
        {
            onPlay.Invoke();
        }
        else
        {
            if (_anim != null)
            {
                _anim.GoToPosition(_startPosition);
            }
            else
            {
                transform.position = _startPosition;
            }
            transform.SetParent(_startParent);
            transform.SetSiblingIndex(_startIndex);
        }
    }
}
