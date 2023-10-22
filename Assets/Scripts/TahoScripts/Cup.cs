using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CupSize
{
    CUP_SMALL,
    CUP_MEDIUM
}

public class Cup : TahoComponents
{
    [SerializeField] private Sprite componentSprite;

    public override void SetOrderSprite()
    {
        AudioManager.instance.PlayRandomSFX(new List<sfxenum> { sfxenum.sfx_plasticCrinkle1, sfxenum.sfx_plasticCrinkle2 });
        GameManager.instance.Order.GetComponent<SpriteRenderer>().sprite = componentSprite;
    }
}
