using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Character;
using InputSystem;
using UnityEngine.SceneManagement;

public class GlobalGame : MonoBehaviour
{

    public BasicInputSystem inputSystem;

    public int NumOfPlayers = 2;

    public GameObject PlayerPrefab;
    [HideInInspector]
    public List<PlayerHandler> Players = new List<PlayerHandler>();

    public int numberOfWormsPerPlayer;

    public LevelTemplate currentLevelTemplate;
    private List<GameObject> spawnPoints = new List<GameObject> ();


    public GameObject Hud;

    [HideInInspector]
    public int winner = 0;
    private Animator anim;

    public GameObject[] playerModels;
    public static GlobalGame Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        inputSystem = GetComponent<BasicInputSystem>();
        if (!inputSystem) Debug.LogError(this.name + " Has No InputSystem Component Assigned in inspector");
        anim = GetComponent<Animator>();
    }

    public void ResetGame()
    {
        spawnPoints.Clear();
        Players.Clear();
    }

    public void SetupGame()
    {
       

        for (int i = 0; i < NumOfPlayers; i++)
        {
            SpawnPlayer();
        }

        anim.Play("EndTransition");

    }

    public void StartGame()
    {
        Hud.SetActive(true);
        TurnHandler.Instance.NextTurn();
    }

    public void LoadLevel(LevelTemplate _template, int playerCount)
    {
        currentLevelTemplate = _template;
        NumOfPlayers = playerCount;
        anim.Play("StartTransition");

    }

    public void SpawnPlayer()
    {
        int _id = Players.Count + 1;
        PlayerHandler _newPlayer = Instantiate(PlayerPrefab, GetSpawnPoint(), Quaternion.identity).GetComponent<PlayerHandler>();
        Players.Add(_newPlayer);
        _newPlayer.myModels = playerModels[_id -1];
        _newPlayer.PlayerDied += PlayerDied;
        _newPlayer.playerID = _id;
        _newPlayer.SpawnWorms(numberOfWormsPerPlayer);


    }

    public Vector3 GetSpawnPoint()
    {


        int _point = Random.Range(0, spawnPoints.Count);
        Vector3 point = spawnPoints[_point].transform.position;
        spawnPoints.Remove(spawnPoints[_point]);
        return point;
    }

    private void PlayerDied()
    {
        int playersAlive = NumOfPlayers;
        int winnerIndex = 0;
        //check if everyone is still alive
        foreach (var player in Players)
        {
            if (!player.IsAlive) playersAlive--;
            else winnerIndex = player.playerID;
        }

        if (playersAlive <= 1) AnnounceWinner(winnerIndex);
    }

    private void AnnounceWinner(int winnerID)
    {
        winner = winnerID;
        Debug.Log($"Player {winner} Is The winner");
        Debug.Log("global");
        Players[winnerID-1].Dance();
        SceneManager.LoadSceneAsync("WinScreen", LoadSceneMode.Additive);
    }

    public void LoadScene() { StartCoroutine(SceneLoading(currentLevelTemplate.SceneName)); }

    IEnumerator SceneLoading(string sceneName)
    {

        ResetGame();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading new Scene . . . : " + sceneName);

            if (asyncOperation.progress >= 0.9f)
            {

                asyncOperation.allowSceneActivation = true;

                while(spawnPoints.Count <= 0) {
                    spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
                    yield return null; }

                SetupGame();
            }

            yield return null;
        }


    }
}
