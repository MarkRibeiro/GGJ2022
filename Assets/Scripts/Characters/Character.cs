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

    public HealthBar healthBar;
    public HealthBar shieldBar;


    private void Start()
    {
        image = GetComponentInChildren<Image>();
        healthBar.SetMaxValue(maxHP);
        currHP = maxHP;
        shieldBar.SetMaxValue(maxShield);
        currShield = maxShield;
    }

    private void Update() {
        if(Input.GetKeyDown("space"))
        {
            currHP -= 10;
            healthBar.SetValue(currHP);
        }
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
