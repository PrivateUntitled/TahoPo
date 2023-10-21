using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    PLAY,
    SETTINGS,
    QUIT,
    APPLY,
    BACK
}

public class Buttons : MonoBehaviour
{
    [SerializeField] private Sprite hoveredCup;
    [SerializeField] private Sprite unhoveredCup;
    [SerializeField] private ButtonType buttonType;
    private SpriteRenderer spriteRenderer;

    public ButtonType ButtonType { get { return buttonType; } }

    private bool isHovered;
    public bool IsHovered { get { return isHovered; } }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        isHovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        isHovered = true;
        spriteRenderer.sprite = hoveredCup;
    }

    public void OnMouseExit()
    {
        isHovered = false;
        UnHover();
    }

    public void UnHover() 
    {
        spriteRenderer.sprite = unhoveredCup;
    }
}