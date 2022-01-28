using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _anim;

    public void OnPointerExit(PointerEventData eventData)
    {
        _anim.SetBool("Hover", false);
    }

    private void Awake() {
        _anim = GetComponent<Animator>();
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _anim.SetBool("Hover", true);
    }


}
