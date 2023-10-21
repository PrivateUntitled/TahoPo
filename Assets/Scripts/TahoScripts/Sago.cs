using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SagoFlavours
{
    INGREDIENT_NORMAL,
    INGREDIENT_STRAWBERRY,
    INGREDIENT_UBE
}

public class Sago : TahoComponents
{
    [SerializeField] private Sprite[] componentSprite;

    public override void SetOrderSprite()
    {
        switch(GameManager.instance.Customer.GetComponent<CustomerOrder>().GetCustomerCupChoice())
        {
            case Components.CUP_SMALL:
                GameManager.instance.Order.GetComponent<SpriteRenderer>().sprite = componentSprite[0];
                break;
            case Components.CUP_MEDIUM:
                GameManager.instance.Order.GetComponent<SpriteRenderer>().sprite = componentSprite[1];
                break;
        }
    }
}
