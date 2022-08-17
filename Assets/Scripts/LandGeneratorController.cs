using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGeneratorController : MonoBehaviour
{
	[SerializeField]
	private Transform landStartingPos;
	[SerializeField]
	private Transform landEndingPos;
	[SerializeField]
	private GameObject mountainObject;

	private bool geneateLand = true;

	[SerializeField]
	private float landMoveSpeed = 15f;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update()
	{

	}

	IEnumerable spawnislands()
	{
		while (geneateLand == true)
		{
			//yield return WaitForSeconds()
			GameObject newLand = Instantiate(mountainObject, landStartingPos.position, Quaternion.identity);

			//newLand.transform.position = Vector3.Lerp(landStartingPos, landEndingPos, )

			yield return null;
		}
	}
}