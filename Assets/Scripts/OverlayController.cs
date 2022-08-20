using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OverlayController : MonoBehaviour
{
	[SerializeField]
	private ItemCollisionHandler itemCH;

	[SerializeField]
	private GameOverMenu gameOverDisplay;

	[SerializeField]
	private TextMeshProUGUI currentTimeText;
	private float currentTime = 0;

	[SerializeField]
	private TextMeshProUGUI bestTimeText;
	private float bestTime = 0;

	private bool keepTime = false;


	private void Start()
	{
		itemCH.OnGameOver += OnGameOver;

		gameOverDisplay.gameObject.SetActive(false);

		keepTime = true;

		bestTime = PlayerPrefs.GetFloat("BestTime", 0);
		bestTimeText.text = "Best Time: " + bestTime.ToString("#.00");
	}

	public void OnGameOver(string message)
	{
		PlayerPrefs.SetFloat("BestTime", bestTime);
		keepTime = false;
		gameOverDisplay.gameObject.SetActive(true);
		gameOverDisplay.gameOverDescription.text = message;
	}

	private void Update()
	{
		if (keepTime)
		{
			currentTime += Time.deltaTime;
			if (currentTime > bestTime)
			{
				bestTime = currentTime;
				bestTimeText.text = "Best Time: " + bestTime.ToString("#.00");
			}
			currentTimeText.text = "Current Time: " + currentTime.ToString("#.00");
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(0);
	}
}
