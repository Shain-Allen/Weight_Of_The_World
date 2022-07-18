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
				//Debug.Log("left Bucket" + balanceDirection);
				break;
			case int expression when balanceDirection < 0:
				rightBalance.value = balanceDirection * -1;
				leftBalance.value = 0;
				//Debug.Log("right Bucket" + balanceDirection);
				break;
			default:
				Debug.Log("buckets are balanced" + balanceDirection);
				leftBalance.value = 0;
				rightBalance.value = 0;
				break;
		}

		Destroy(collectedObject);
	}
}