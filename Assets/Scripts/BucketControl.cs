using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketControl : MonoBehaviour
{
	public event Action<string, GameObject> OnObjectCollected;

	private void OnTriggerEnter(Collider other)
	{
		OnObjectCollected?.Invoke(gameObject.name, other.gameObject);
	}
}