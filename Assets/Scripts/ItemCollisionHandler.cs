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
	Slider leftBalance;
	[SerializeField]
	BucketControl rightBucket;
	[SerializeField]
	Slider rightBalance;


	private int leftBucketWeight = 0;
	private int rightbucketWeight = 0;
	private int bucketBalance;
	[SerializeField]
	private int balanceCapacity = 10;

	private void Start()
	{
		leftBucket.OnObjectCollected += OnObjectCollected;
		rightBucket.OnObjectCollected += OnObjectCollected;

		leftBalance.maxValue = balanceCapacity;
		rightBalance.maxValue = balanceCapacity;
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
				rightbucketWeight += collectedObject.GetComponent<ItemData>().itemValues.itemWeight;
			}
		}

		int balanceDirection = leftBucketWeight - rightbucketWeight;

		switch (balanceDirection)
		{
			case int expression when balanceDirection > 0:
				leftBalance.value = balanceDirection;
				rightBalance.value = 0;
				break;
			case int expression when balanceDirection < 0:
				rightBalance.value = balanceDirection * -1;
				leftBalance.value = 0;
				break;
			default:
				Debug.Log("buckets are balanced" + balanceDirection);
				leftBalance.value = 0;
				rightBalance.value = 0;
				break;
		}

		if (balanceDirection < 0)
		{
			balanceDirection = balanceDirection * -1;
		}

		switch ((float)balanceDirection / (float)balanceCapacity)
		{
			case < 0.25f:
				leftBalance.fillRect.GetComponent<Image>().color = Color.green;
				rightBalance.fillRect.GetComponent<Image>().color = Color.green;
				break;
			case < 0.5f:
				leftBalance.fillRect.GetComponent<Image>().color = new Color(0.9803922f, 0.7176471f, 0.2f);
				rightBalance.fillRect.GetComponent<Image>().color = new Color(0.9803922f, 0.7176471f, 0.2f);
				break;
			case < 0.75f:
				leftBalance.fillRect.GetComponent<Image>().color = new Color(1, 0.5568628f, 0.08235294f);
				rightBalance.fillRect.GetComponent<Image>().color = new Color(1, 0.5568628f, 0.08235294f);
				break;
			case < 0.95f:
				leftBalance.fillRect.GetComponent<Image>().color = new Color(1, 0.3058824f, 0.06666667f);
				rightBalance.fillRect.GetComponent<Image>().color = new Color(1, 0.3058824f, 0.06666667f);
				break;
			case > 0.95f:
				leftBalance.fillRect.GetComponent<Image>().color = Color.red;
				rightBalance.fillRect.GetComponent<Image>().color = Color.red;
				break;
		}

		Destroy(collectedObject);
	}
}