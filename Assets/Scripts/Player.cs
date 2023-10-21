using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int tries;
    private CustomerOrder currentCustomerOrder;

    public CustomerOrder CurrentCustomerOrder { set { currentCustomerOrder = value; } }

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, -Vector2.up);

            // If collider hits something
            if (hit.collider == null)
                return;

            TahoComponents tahoComponent = hit.collider.GetComponent<TahoComponents>();

            // If collider hits taho component
            if (tahoComponent == null)
                return;

            // Check if order is correct
            if (currentCustomerOrder.ComponentMatches(tahoComponent.Component))
            {
                tahoComponent.AddComponentToOrder();
                currentCustomerOrder.NextComponent();
            }
            else
            {
                tries--;
                Debug.Log("Incorrect Order, Tries Left: " + tries);

                if (tries <= 0)
                {
                    tries = 2;
                    GameManager.instance.CallNextCustomer();
                }
            }
        }
    }
}
