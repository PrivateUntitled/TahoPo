using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    [Header("Customer Info")]
    [SerializeField] Sprite[] CharacterSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Order")]
    private List<Components> customerOrder = new List<Components>();
    private int orderIndex;

    [Header("Customer Preferences")]
    [SerializeField] private List<Components> cupSizePreference;
    [SerializeField] private List<Components> sagoFlavourPreference;
    [SerializeField] private List<Components> syrupFlavourPreference;
    [SerializeField] private Components taho;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Customer";

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        
        orderIndex = 0;
        GameManager.instance.Player.GetComponent<Player>().CurrentCustomerOrder = GetComponent<CustomerOrder>();

        spriteRenderer.sprite = CharacterSprites[orderIndex];

        //Selecting Random Orders
        customerOrder.Add(SelectRandomCup());
        customerOrder.Add(taho);
        customerOrder.Add(SelectRandomSagoFlavour());
        customerOrder.Add(SelectRandomSyrupFlavour());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Randdom Function
    Components SelectRandomCup()
    {
        return cupSizePreference[Random.Range(0, cupSizePreference.Count)];
    }

    Components SelectRandomSyrupFlavour()
    {
        return syrupFlavourPreference[Random.Range(0, syrupFlavourPreference.Count)];
    }

    Components SelectRandomSagoFlavour()
    {
        return sagoFlavourPreference[Random.Range(0, sagoFlavourPreference.Count)];
    }
    #endregion

    public bool ComponentMatches(Components component)
    {
        return customerOrder[orderIndex] == component;
    }

    public void NextComponent()
    {
        orderIndex++;
        
        if (orderIndex >= customerOrder.Count)
        {
            Debug.Log("Order Done");
            GameManager.instance.CallNextCustomer();
        }
        else
        {
            spriteRenderer.sprite = CharacterSprites[orderIndex];
        }
    }
}
