using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ChoseButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    Color unselectedColor = new Color(0.745283f, 0.745283f, 0.745283f);

    public int ID;


    public List<Sprite> Sprites;

    private Toggle toggle;
    private Vector2 initialScale;

    private Button button;

    private void Start()
    {
        toggle = this.GetComponent<Toggle>();
        initialScale = this.transform.localScale;
        button = GameObject.Find("Jogar-button").GetComponent<Button>();
    }


    private void Update()
    {
        if (toggle.isOn)
        {
            Selected();
            toggle.group.GetComponent<ToggleGroup>().allowSwitchOff = false;
            button.interactable = true;

        }
        else
        {
            if (mouse_over == false)
            {
                Unselected();
            }
        }


    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;

        Selected();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        this.transform.localScale = initialScale;
        Unselected();

    }

    public void Selected()
    {
        toggle.GetComponent<Image>().color = Color.white;
        this.GetComponent<Image>().sprite = Sprites[0];
        this.transform.localScale = initialScale * 1.3f;
        CharacterManager.playerID = ID;

    }
    public void Unselected()
    {

        toggle.GetComponent<Image>().color = unselectedColor;
        this.GetComponent<Image>().sprite = Sprites[1];
        this.transform.localScale = initialScale * 1;


    }

}
