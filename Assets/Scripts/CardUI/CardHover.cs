using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _anim;
    private Canvas _canvas;

    public int defaultSortingOrder;
    public int hoverSortingOrder;

    public void OnPointerExit(PointerEventData eventData)
    {
        _anim.SetBool("Hover", false);
        _canvas.sortingOrder = defaultSortingOrder;
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _canvas = GetComponentInParent<Canvas>();
        Assert.IsNotNull(_anim);
        Assert.IsNotNull(_canvas);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _anim.SetBool("Hover", true);
        _canvas.sortingOrder = hoverSortingOrder;
    }


}
