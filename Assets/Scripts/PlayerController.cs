using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float rotationspeed = 5f;

	void Awake()
	{
		// 0 for no sync, 1 for panel refresh rate, 2 for 1/2 panel rate
		QualitySettings.vSyncCount = 1;
	}


	// Update is called once per frame
	void Update()
	{
		gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationspeed, 0));

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}