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
    [SerializeField] private Components component;
    [SerializeField] private PolygonCollider2D polyCollider;

    public void Start()
    {
    }

    private void Update()
    {
        if (!GameManager.instance.Player)
            return;

        polyCollider.enabled = !GameManager.instance.Player.GetComponent<Player>().isTalking;
    }

    public string ComponentName { get { return componentName; } }

    public Components Component { get { return component; } set { component = value; } }

    public virtual void SetOrderSprite() {  }

    public void AddComponentToOrder()
    {
        Debug.Log(componentName + " was added");
    }

    public void DisablePoly()
    {
        polyCollider.enabled = false;
    }

    public void EnablePoly()
    {
        polyCollider.enabled = true;
    }

    public void OnMouseOver()
    {
        GameManager.instance.Player.GetComponent<Player>().collideWithTahoComponent = true;
    }

    public void OnMouseExit()
    {
        GameManager.instance.Player.GetComponent<Player>().collideWithTahoComponent = false;
    }
}
