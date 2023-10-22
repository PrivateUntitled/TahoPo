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

    // Background
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private GameObject backgroundPrefab;
    private GameObject background;

    // Customer Count
    private int customerCount;
    [SerializeField] private int customerToServe;
    public int CustomerToServe { get { return customerToServe; } }

    // Days
    [SerializeField, Min(1)] private int maxDays;
    private int currentDay;
    public int CurrentDay { get { return currentDay; } }

    [SerializeField] private CustomerListPerDay[] TotalDays;

    private void Start()
    {   
        StartCoroutine(LoadMainMenuScene());
    }

    public void StartNewGame()
    {
        currentDay = 0;
        maxDays = TotalDays.Length;

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_SomethingToLookForwardTo_final);

        StartCoroutine(PlayAmbience());

        StartGame();
    }

    public void StartGame()
    {
        customerCount = 0;
        customerToServe = TotalDays[currentDay].Customers.Count;

        // Spawn Player
        Player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneByName("MainGame"));

        // Spawn Clock
        clock = Instantiate(clockPrefab, clockPrefab.transform.position, clockPrefab.transform.rotation);
        SceneManager.MoveGameObjectToScene(clock, SceneManager.GetSceneByName("MainGame"));

        // Spawn Order Location
        Order = Instantiate(orderPrefab, orderPrefab.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Order, SceneManager.GetSceneByName("MainGame"));

        background = Instantiate(backgroundPrefab, backgroundPrefab.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(background, SceneManager.GetSceneByName("MainGame"));

        StartCoroutine(TransitiontoNextCustomer());
    }

    public void StartNewDay()
    {
        currentDay++;
        Destroy(Player);
        Destroy(clock);
        Destroy(Order);

        Debug.Log("Current Day: " + currentDay + "Max Days" + maxDays);

        if (maxDays <= currentDay)
        {
            // Piece it together
            EndingScene();
        }
        else
        {
            StartGame();
        }
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

        SetBackground();

        // If All Customers of the Day are Served
        if (customerCount >= customerToServe)
        {
            Debug.Log("Served All Customer");
            // Do Chismis
            TsismisScene();
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

        //AudioManager.instance.PlayRandomSFX(new List<sfxenum> { sfxenum.Male1_Taho1, sfxenum.Male1_Taho2 });

        // Start Tutorial
        if (currentDay == 0 && customerCount == 0)
        {
            Player.GetComponent<Player>().InfiniteTries = true;
        }
        else
        {
            Player.GetComponent<Player>().InfiniteTries = false;
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

    public IEnumerator LoadMainMenuScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_GentleWarmth_final);
    }

    public IEnumerator LoadMainMenuScene2()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("EndingScene");
        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_GentleWarmth_final);
    }

    IEnumerator LoadMainGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartNewGame();
    }

    void EndingScene()
    {
        SceneManager.UnloadSceneAsync("TsismisScene");
        StopAllCoroutines();
        StartCoroutine(LoadEndingScene());
    }

    IEnumerator LoadEndingScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("EndingScene", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_BerryFlavored_final);
    }

    void TsismisScene()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        StopAllCoroutines();
        StartCoroutine(LoadTsismisScene());
    }

    IEnumerator LoadTsismisScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TsismisScene", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_BerryFlavored_final);
    }

    public IEnumerator GameToTsismis()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_BerryFlavored_final);
        SceneManager.UnloadSceneAsync("TsismisScene");
        StartNewDay();
    }
    #endregion

    IEnumerator PlayAmbience()
    {
        while(true)
        {
            AudioManager.instance.PlayAmbience(new List<sfxenum> { sfxenum.sfx_birds1, sfxenum.sfx_birds2, sfxenum.sfx_birds3, sfxenum.sfx_birds4, 
                                                                   sfxenum.sfx_leaves1, sfxenum.sfx_leaves2, sfxenum.sfx_leaves3, sfxenum.sfx_leaves4,
                                                                   sfxenum.sfx_longAmbience_windAndLeaves});

            yield return new WaitForSeconds(Random.Range(10, 20));
        }
    }

    public void SetBackground()
    {
        if (customerCount == 0)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0];
        }
        else if (customerCount == CustomerToServe - 1)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[2];
        }
        else
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[1];
        }
    }

    [System.Serializable]
    public struct CustomerListPerDay
    {
        public List<GameObject> Customers;
    }
}
    