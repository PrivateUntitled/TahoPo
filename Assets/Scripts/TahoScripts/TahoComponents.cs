using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Components
{
    CUP_SMALL,
    CUP_MEDIUM,
    TAHO,
    SYRUP_NORMAL,
    SYRUP_STRAWBERRY,
    SYRUP_UBE,
    SAGO_NORMAL,
    SAGO_STRAWBERRY,
    SAGO_UBE,
}
public class TahoComponents : MonoBehaviour
{
    [SerializeField] private string componentName;
    [SerializeField] private Sprite componentSprite;
    [SerializeField] private Components component;

    public string ComponentName { get { return componentName; } }
    public Sprite ComponentSprite { get { return componentSprite; } }

    public Components Component { get { return component; } set { component = value; } }

    public void AddComponentToOrder()
    {
        Debug.Log(componentName + " was added");
    }
}
