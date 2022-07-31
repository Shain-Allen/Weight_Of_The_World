using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
	[SerializeField]
	private ItemCollisionHandler itemCH;

	[SerializeField]
	private GameOverMenu gameOverDisplay;

	private void start()
	{
		itemCH.OnGameOver += OnGameOver;

		gameOverDisplay.gameObject.SetActive(false);
	}

	public void OnGameOver(string message)
	{
		gameOverDisplay.gameObject.SetActive(true);
	}


}
