using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Player
    [SerializeField] private GameObject playerPrefab;
    [HideInInspector] public GameObject Player;

    // Customer
    [SerializeField] private List<GameObject> customerPrefab;
    [HideInInspector] public GameObject Customer;

    // Customer's Order 
    [SerializeField] private GameObject orderPrefab;
    [HideInInspector] public GameObject Order;

    // Clock
    [SerializeField] private GameObject clockPrefab;
    private GameObject clock;

    // Customer Count
    private int customerCount;
    [SerializeField] private int customerToServe;
    public int CustomerToServe { get { return customerToServe; } }

    // Days
    [SerializeField, Min(1)] private int maxDays;
    private int currentDay = 1;

    [SerializeField] private CustomerListPerDay[] TotalDays;

    private void Start()
    {   
        StartCoroutine(LoadMainMenuScene());
    }

    public void StartGame()
    {
        currentDay = 0;
        maxDays = TotalDays.Length;

        customerCount = 0;
        customerToServe = TotalDays[currentDay].Customers.Count;

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_SomethingToLookForwardTo_drft1);

        // Spawn Player
        Player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneByName("MainGame"));

        // Spawn Clock
        clock = Instantiate(clockPrefab, clockPrefab.transform.position, clockPrefab.transform.rotation);
        SceneManager.MoveGameObjectToScene(clock, SceneManager.GetSceneByName("MainGame"));

        // Spawn Order Location
        Order = Instantiate(orderPrefab, orderPrefab.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Order, SceneManager.GetSceneByName("MainGame"));

        StartCoroutine(TransitiontoNextCustomer());
    }

    public void StartNewDay()
    {
        currentDay++;
        Destroy(Player);
        Destroy(clock);
        Destroy(Order);

        StartGame();
    }

    public void CallNextCustomer()
    {
        StartCoroutine(CustomerLeaving());
    }

    IEnumerator CustomerLeaving()
    {
        yield return new WaitForSeconds(2);

        // not first customer
        if (Customer != null)
        {
            Destroy(Customer.gameObject);
            customerCount++;
            clock.GetComponent<Clock>().RotateArrow(customerCount);
            Order.GetComponent<SpriteRenderer>().sprite = null;
        }

        // If All Customers of the Day are Served
        if (customerCount >= customerToServe)
        {
            Debug.Log("Served All Customer");
            // Do Chismis

            if (maxDays <= currentDay)
            {
                // Piece it together
            }
            else
            {
                StartNewDay();
            }
        }
        else
        {
            StartCoroutine(TransitiontoNextCustomer());
        }
    }

    IEnumerator TransitiontoNextCustomer()
    {
        yield return new WaitForSeconds(2);

        int tahoScream = Random.Range(0, 2);

        // Random Jason Taho is Speaking
        switch(tahoScream)
        {
            case 0:
                AudioManager.instance.PlaySFX(sfxenum.Male1_Taho1);
                break;
            case 1:
                AudioManager.instance.PlaySFX(sfxenum.Male1_Taho2);
                break;
        }

        //Get Random Character - Archived
        //GameObject randomChosenCharacter = customerPrefab[Random.Range(0, customerPrefab.Count)];
        //Customer = Instantiate(randomChosenCharacter, randomChosenCharacter.transform.position, Quaternion.identity);

        Customer = Instantiate(TotalDays[currentDay].Customers[customerCount], TotalDays[currentDay].Customers[customerCount].transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Customer, SceneManager.GetSceneByName("MainGame"));

        Player.GetComponent<Player>().ReplenishTries();
    }

    #region SceneManagement Scripts
    public void LoadMainGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        StartCoroutine(LoadMainGameScene());
    }

    IEnumerator LoadMainMenuScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_GentleWarmth);
    }

    IEnumerator LoadMainGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartGame();
    }
    #endregion

    [System.Serializable]
    public struct CustomerListPerDay
    {
        public List<GameObject> Customers;
    }
}
    