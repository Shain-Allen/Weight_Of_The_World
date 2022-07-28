using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
	public event Action<GameObject> OnObjectMissed;

	private void OnCollisionEnter(Collision other)
	{
		OnObjectMissed?.Invoke(other.gameObject);
	}
}
