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
        GameManager.instance.Order.GetComponent<SpriteRenderer>().sprite = componentSprite;
    }
}
