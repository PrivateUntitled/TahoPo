using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int tries;
    private CustomerOrder currentCustomerOrder;

    public int Tries { get { return tries; } }

    private bool infiniteTries;
    public bool InfiniteTries { set { infiniteTries = value; } }

    public CustomerOrder CurrentCustomerOrder { set { currentCustomerOrder = value; } }
    public bool collideWithTahoComponent;
    public bool isTalking;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TextWriter textWriter = GameManager.instance.Customer.GetComponent<DialogActivator>().TextWriter;
                if (textWriter.TextIsDone)
                {
                    textWriter.TimeCharacter = textWriter.TimePerCharacter;
                    textWriter.ProgressDialogue();
                }
                else
                    textWriter.TimeCharacter = 0;
            }
        }
        else
        {
            if (!collideWithTahoComponent)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, -Vector2.up);

                // If collider hits something
                if (hit.collider == null)
                    return;

                // If collider hits taho component
                if (hit.collider.GetComponent<TahoComponents>() == null)
                    return;

                TahoComponents tahoComponent = hit.collider.GetComponent<TahoComponents>();

                if (currentCustomerOrder == null || tries == 0)
                    return;

                // Check if order is correct
                if (currentCustomerOrder.ComponentMatches(tahoComponent.Component))
                {
                    tahoComponent.AddComponentToOrder();
                    tahoComponent.SetOrderSprite();
                    currentCustomerOrder.NextComponent();
                }
                else
                {
                    if (!infiniteTries)
                        tries--;

                    AudioManager.instance.PlaySFX(sfxenum.sfx_guessWrong);

                    Debug.Log("Incorrect Order, Tries Left: " + tries);
                    GameManager.instance.Customer.GetComponent<DialogActivator>().LeaveDialogue(tries);

                    if (tries <= 0)
                    {
                        GameManager.instance.CallNextCustomer();
                    }
                    else
                    {
                        GameManager.instance.Customer.GetComponent<CustomerOrder>().SetPlayerSprite();
                    }
                }
            }
        }
    }

    public void ReplenishTries()
    {
        tries = 2;
    }
}
