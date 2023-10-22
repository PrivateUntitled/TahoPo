using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SyrupFlavours
{
    FLAVOUR_NORMAL,
    FLAVOUR_STRAWBERRY,
    FLAVOUR_UBE
}

public class Syrup : TahoComponents
{
    [SerializeField] private Sprite[] componentSprite;

    public override void SetOrderSprite()
    {
        AudioManager.instance.PlayRandomSFX(new List<sfxenum> { sfxenum.sfx_bottleliquidPour1, sfxenum.sfx_bottleliquidPour2 });
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
