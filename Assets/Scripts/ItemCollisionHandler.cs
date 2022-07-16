using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
	[SerializeField]
	BucketControl leftBucket;
	[SerializeField]
	BucketControl RightBucket;

	private void Start()
	{
		leftBucket.OnObjectCollected += OnObjectCollected;
		RightBucket.OnObjectCollected += OnObjectCollected;
	}

	private void OnObjectCollected(string bucketName, GameObject collectedObject)
	{
		Debug.Log(bucketName + " cought " + collectedObject.name);

		Destroy(collectedObject);
	}
}