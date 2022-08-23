using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject Gamereadout;

    private void Start()
    {
        gameOverMenu.SetActive(false);
        Gamereadout.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Gamereadout.SetActive(!Gamereadout.activeSelf);
            gameOverMenu.SetActive(!gameOverMenu.activeSelf);
            Time.timeScale = gameOverMenu.activeSelf ? 0 : 1;
        }
    }

    public void ResumeGame()
    {
        Gamereadout.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
