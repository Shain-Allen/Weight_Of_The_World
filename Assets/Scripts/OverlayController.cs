using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
	[SerializeField]
	private ItemCollisionHandler itemCH;

	[SerializeField]
	private GameObject gameOverDisplay;

	private void Start()
	{
		itemCH.OnGameOver += OnGameOver;

		gameOverDisplay.SetActive(false);
	}

	public void OnGameOver()
	{
		gameOverDisplay.SetActive(true);
	}
}
