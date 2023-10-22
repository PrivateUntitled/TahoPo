using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taho : TahoComponents
{
    [SerializeField] private Sprite[] componentSprite;

    public override void SetOrderSprite()
    {
        AudioManager.instance.PlaySFX(sfxenum.sfx_scoopPlastic1);
        switch (GameManager.instance.Customer.GetComponent<CustomerOrder>().GetCustomerCupChoice())
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
