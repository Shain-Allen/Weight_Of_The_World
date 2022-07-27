using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
	public RectTransform fillRect;
	[Range(-1, 1)]
	public float value = 0;

	Vector2 anchorMaxLeft = new Vector2(0.5f, 1);
	Vector2 anchorMinLeft = new Vector2(0, 0);
	Vector2 anchorMaxRight = new Vector2(1, 1);
	Vector2 anchorMinRight = new Vector2(0.5f, 0);

	private void Start()
	{
		value = 0f;
		fillRect.anchorMin = new Vector2(0.5f, 0);
		fillRect.anchorMax = new Vector2(0.5f, 1);

		fillRect.offsetMax = new Vector2(0, 0);
		fillRect.offsetMin = new Vector2(0, 0);
	}

	private void Update()
	{
		if (value > 0)
		{
			fillRect.anchorMax = anchorMaxLeft + new Vector2(value / 2, 0);
			fillRect.anchorMin = anchorMinRight;
		}
		else if (value < 0)
		{
			fillRect.anchorMax = anchorMaxLeft;
			fillRect.anchorMin = anchorMinRight + new Vector2(value / 2, 0);
		}
		else
		{
			fillRect.offsetMax = new Vector2(0, 0);
			fillRect.offsetMin = new Vector2(0, 0);
		}
	}
}
