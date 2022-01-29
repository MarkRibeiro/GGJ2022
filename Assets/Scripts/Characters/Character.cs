using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour
{

    //Recursos
    public int reason;
    public int emotion;

    public int expressionNumber;
    public Character target;

    public List<Sprite> spriteSheet;

    private Image image;

    //Cartas
    public Card[] deck;
    public List<GameObject> currentHand;

    //Barras
    public int maxHP;
    public int currHP;
    public int maxShield;
    public int currShield;

    public Slider healthBar;
    public Slider shieldBar;


    private void Start()
    {
        image = GetComponentInChildren<Image>();
    }



    public void ChangeExpression(Expressions emotion)
    {
        int index;
        switch (emotion)
        {

            case (Expressions.NORMAL):
                index = 0;
                break;

            case (Expressions.SHAME):
                index = 1;
                break;


            case (Expressions.SMUG):
                index = 2;
                break;

            default:
                index = 0;

                break;
        }
        image.sprite = spriteSheet[index];
    }

}
