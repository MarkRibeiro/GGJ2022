using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
[System.Serializable]
public class Character : MonoBehaviour
{

    public int ID;

    //Recursos
    public int reason;
    public int emotion;

    public Character target;
    public Expressions currentExpression;

    public List<Sprite> spriteSheet;

    private Image image;

    //Cartas
    public Card[] deck;
    public List<GameObject> currentHand;

    public RectTransform handArea;

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
        SetExpression();
        healthBar.SetMaxValue(maxHP);
        currHP = maxHP;
        shieldBar.SetMaxValue(maxShield);
        currShield = maxShield;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            currHP -= 10;
            healthBar.SetValue(currHP);
        }
    }

    public void ChangeExpression(Expressions emotion)
    {
        switch (emotion)
        {
            case (Expressions.NORMAL):
                currentExpression = Expressions.NORMAL;
                break;
            case (Expressions.SHAME):
                currentExpression = Expressions.SHAME;
                break;
            case (Expressions.SMUG):
                currentExpression = Expressions.SMUG;
                break;
            case (Expressions.RANDOM):
                currentExpression = Expressions.RANDOM;
                break;
            default:
                currentExpression = Expressions.NORMAL;
                break;
        }
        SetExpression();
    }
    public void SetExpression()
    {
        int index;
        switch (currentExpression)
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
        Debug.Log(index);
        Debug.Log(spriteSheet);
        Debug.Log(image);
        image.sprite = spriteSheet[index];
    }
}
