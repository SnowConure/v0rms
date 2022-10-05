using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Winner : MonoBehaviour
{
    public TMP_Text winner;

    private void Start()
    {
        CameraController.main.WideFocus();
        winner.text = $"{GlobalGame.Instance.Players[GlobalGame.Instance.winner-1].myModels.name} Wins!";
    }

    private void OnEnable()
    {
        GlobalGame.Instance.inputSystem.Jump += EndMatch;

    }
    private void OnDisable()
    {
        GlobalGame.Instance.inputSystem.Jump -= EndMatch;

    }

    public void EndMatch()
    {
        Destroy(GlobalGame.Instance.gameObject);
        SceneManager.LoadSceneAsync("MainMenu");

    }
}
