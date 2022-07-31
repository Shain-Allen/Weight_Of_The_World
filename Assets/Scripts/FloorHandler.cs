using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
	public event Action<GameObject> OnObjectMissed;
	private void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.gameObject.name);
		OnObjectMissed?.Invoke(other.gameObject);
	}
}
