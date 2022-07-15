using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float rotationspeed = 5f;

	// Update is called once per frame
	void Update()
	{
		gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationspeed, 0));
	}
}