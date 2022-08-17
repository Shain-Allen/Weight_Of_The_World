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
	private bool firstLand = true;
	private bool firstLandlerp = true;

	[SerializeField]
	private float landMoveSpeed = 15f;

	[SerializeField]
	private GameObject startingLand;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	private void Start()
	{
		StartCoroutine(spawnislands());
		StartCoroutine(Lerp(startingLand));
	}


	IEnumerator spawnislands()
	{
		while (geneateLand == true)
		{
			if (!firstLand)
				yield return new WaitForSeconds(landMoveSpeed / 2);
			GameObject newLand = Instantiate(mountainObject, landStartingPos.position, Quaternion.identity);
			StartCoroutine(Lerp(newLand));
			firstLand = false;
		}
	}

	IEnumerator Lerp(GameObject newland)
	{
		float timeElapsed = 0;

		if (firstLandlerp)
		{
			timeElapsed = landMoveSpeed / 2;
			firstLandlerp = false;
		}

		while (timeElapsed < landMoveSpeed)
		{
			newland.transform.position = Vector3.Lerp(landStartingPos.position, landEndingPos.position, timeElapsed / landMoveSpeed);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		newland.transform.position = landEndingPos.position;
		Destroy(newland);
	}
}