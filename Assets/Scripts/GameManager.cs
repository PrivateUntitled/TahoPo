using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    [HideInInspector] public GameObject Player;

    [SerializeField] private GameObject customerPrefab;
    [HideInInspector] public GameObject Customer;

    [SerializeField] private GameObject clockPrefab;
    private GameObject clock;

    private int customerCount;
    [SerializeField] private int customerToServe;

    public int CustomerToServe { get { return customerToServe; } }

    private void Start()
    {
        customerCount = 0;
        StartCoroutine(LoadMainMenuScene());
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_GentleWarmth);
    }

    public void StartGame()
    {
        AudioManager.instance.StopBackgroundMusicSound();
        AudioManager.instance.PlayBackgroundMusic(bgmenum.GJP23_SomethingToLookForwardTo_drft1);

        Player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneByName("MainGame"));

        clock = Instantiate(clockPrefab, clockPrefab.transform.position, clockPrefab.transform.rotation);
        SceneManager.MoveGameObjectToScene(clock, SceneManager.GetSceneByName("MainGame"));

        CallNextCustomer();
    }

    public void CallNextCustomer()
    {
        if(customerCount >= customerToServe)
        {
            Debug.Log("Served All Customer");
            return;
        }

        if (Customer != null)
        {
            Destroy(Customer.gameObject);
            customerCount++;
            clock.GetComponent<Clock>().RotateArrow(customerCount);
        }

        Customer = Instantiate(customerPrefab, customerPrefab.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(Customer, SceneManager.GetSceneByName("MainGame"));
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
}
    