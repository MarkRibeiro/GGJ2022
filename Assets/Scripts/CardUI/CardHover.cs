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
    public bool isHovered;

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        _anim.SetBool("Hover", false);
        _canvas.sortingOrder = defaultSortingOrder;
    }

    private void Awake()
    {
        isHovered = false;
        _anim = GetComponent<Animator>();
        _canvas = GetComponentInParent<Canvas>();
        Assert.IsNotNull(_anim);
        Assert.IsNotNull(_canvas);
        _canvas.sortingOrder = defaultSortingOrder;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        _anim.SetBool("Hover", true);
        _canvas.sortingOrder = hoverSortingOrder;
        var audio = AudioManager.instance;
        if(audio != null)
        {
            audio.Play("CardHover");
        }
    }


}
