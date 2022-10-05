using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;



public class MainMenu : MonoBehaviour
{
    public GameObject Start, Level, Player, Settings;
    public Slider playerNumberSlider;
    public TMP_Text playerNumText;

    public LevelTemplate[] levels;

    private void Awake()
    {
       // SceneManager.LoadSceneAsync("GamePlaySystems", LoadSceneMode.Additive);  
    }

    private void Update()
    {
        playerNumText.text = "Players: " + Mathf.RoundToInt(playerNumberSlider.value);
    }

    public void Activatemenu(GameObject menu)
    {
        Start.SetActive(false);
        Level.SetActive(false);
        Player.SetActive(false);
        Settings.SetActive(false);
        menu.SetActive(true);



    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartGame(levels[Random.Range(0, levels.Length)]);
    }
    public void StartGame(LevelTemplate level)
    {
        GlobalGame.Instance.LoadLevel(level, Mathf.RoundToInt(playerNumberSlider.value));
    }

}
