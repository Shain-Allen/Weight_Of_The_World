using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
	[SerializeField]
	BucketControl leftBucket;
	[SerializeField]
	BucketControl rightBucket;

	private int leftBucketWeight = 0;
	private int rightbucketWeight = 0;
	private int bucketBalance;

	private void Start()
	{
		leftBucket.OnObjectCollected += OnObjectCollected;
		rightBucket.OnObjectCollected += OnObjectCollected;
	}

	private void OnObjectCollected(string bucketName, GameObject collectedObject)
	{
		//Debug.Log(bucketName + " cought " + collectedObject.name);

		if (bucketName == "Left Bucket")
		{

		}
		else
		{

		}

		Destroy(collectedObject);
	}
}