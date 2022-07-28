using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollisionHandler : MonoBehaviour
{
	[SerializeField]
	BucketControl leftBucket;
	[SerializeField]
	BucketControl rightBucket;
	[SerializeField]
	BalanceBar balanceDisplay;
	[SerializeField]
	FloorHandler floor;

	private int leftBucketWeight = 0;
	private int rightBucketWeight = 0;
	private int bucketBalance;
	[SerializeField]
	private int balanceCapacity = 10;

	[SerializeField]
	private int godsGraceCapacity = 4;
	private int godsGrace;

	public event Action OnGameOver;

	private void Start()
	{
		leftBucket.OnObjectCollected += OnObjectCollected;
		rightBucket.OnObjectCollected += OnObjectCollected;
		floor.OnObjectMissed += OnObjectMissed;

		godsGrace = godsGraceCapacity;
	}

	private void OnObjectCollected(string bucketName, GameObject collectedObject)
	{
		//Debug.Log(bucketName + " cought " + collectedObject.name);
		if (collectedObject.GetComponent<ItemData>())
		{

			if (bucketName == "Left Bucket")
			{
				leftBucketWeight += collectedObject.GetComponent<ItemData>().itemValues.itemWeight;
			}
			else
			{
				rightBucketWeight += collectedObject.GetComponent<ItemData>().itemValues.itemWeight;
			}
		}

		float balanceDirection = leftBucketWeight - rightBucketWeight;

		balanceDisplay.value = Mathf.Clamp((float)balanceDirection / (float)balanceCapacity, -1, 1);

		if (balanceDirection < 0)
		{
			balanceDirection = balanceDirection * -1;
		}

		switch (Mathf.Clamp((float)balanceDirection / (float)balanceCapacity, -1, 1))
		{
			case < 0.25f:
				balanceDisplay.fillRect.GetComponent<Image>().color = Color.green;
				break;
			case < 0.5f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(0.9803922f, 0.7176471f, 0.2f);
				break;
			case < 0.75f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(1, 0.5568628f, 0.08235294f);
				break;
			case < 0.95f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(1, 0.3058824f, 0.06666667f);
				break;
			case > 0.95f:
				balanceDisplay.fillRect.GetComponent<Image>().color = Color.red;
				break;
		}

		Destroy(collectedObject);
	}

	private void OnObjectMissed(GameObject other)
	{
		Destroy(other);
		godsGrace--;

		if (godsGrace == 0)
		{
			OnGameOver?.Invoke();
		}
	}
}