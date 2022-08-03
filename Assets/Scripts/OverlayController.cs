using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
	[SerializeField]
	private ItemCollisionHandler itemCH;

	[SerializeField]
	private GameOverMenu gameOverDisplay;


	private void Start()
	{
		itemCH.OnGameOver += OnGameOver;

		gameOverDisplay.gameObject.SetActive(false);
	}

	public void OnGameOver(string message)
	{
		Debug.Log("yes, game over");
		gameOverDisplay.gameObject.SetActive(true);
	}
}
