using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float movementSpeed = 10f;

	private Vector3 moveDir = new Vector2(0, 0);

	private Transform playerTrans;

	[SerializeField]
	private Transform MaxOuterBound;

	[SerializeField]
	private Transform MinOuterBound;

	[SerializeField]
	private Transform HipLocation;

	[SerializeField]
	private LayerMask floorLayer;


	void Awake()
	{
		// 0 for no sync, 1 for panel refresh rate, 2 for 1/2 panel rate
		QualitySettings.vSyncCount = 1;

		playerTrans = GetComponent<Transform>();
	}


	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}


		moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		playerTrans.position += moveDir * movementSpeed * Time.deltaTime;

		playerTrans.position = ClampVector3(playerTrans.position, MinOuterBound.position, MaxOuterBound.position);

		RaycastHit raycastResults;

		if (Physics.Raycast(HipLocation.position, Vector3.down, out raycastResults, 100f, floorLayer))
		{
			playerTrans.position = new Vector3(playerTrans.position.x, raycastResults.point.y, playerTrans.position.z);
		}
	}


	Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max)
	{
		Vector3 clampedValue = new Vector3(0, 0, 0);

		clampedValue.x = Mathf.Clamp(value.x, min.x, max.x);
		clampedValue.y = Mathf.Clamp(value.y, min.y, max.y);
		clampedValue.z = Mathf.Clamp(value.z, min.z, max.z);

		return clampedValue;
	}
}