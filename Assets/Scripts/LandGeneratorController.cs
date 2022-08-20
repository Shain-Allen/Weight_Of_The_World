using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGeneratorController : MonoBehaviour
{
	//where new lands will spawn
	[SerializeField]
	private Transform landStartingPos;

	//where new lands will get deleted
	[SerializeField]
	private Transform landEndingPos;

	[SerializeField]
	private GameObject mountainObject;

	//obstacles to spawn on land
	[SerializeField]
	private GameObject[] obstacles;

	//layer mask to know what obstacles to look for to prevent overlaping
	[SerializeField]
	private LayerMask obstacleMask;

	//layermask for the land
	[SerializeField]
	private LayerMask islandMask;

	//the bottom left corner of an land
	[SerializeField]
	private Transform minLandBound;

	//the top right corner of the land
	[SerializeField]
	private Transform maxLandBound;

	private bool geneateLand = true;

	[SerializeField]
	private float landMoveSpeed = 15f;
	
	//how many islands will exist at any given time
	private float islandAmount = 6;

	[SerializeField]
	private GameObject startingLand;
	private bool firstLand = true;
	private bool startingLandlerp = true;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	private void Start()
	{
		StartCoroutine(Lerp(startingLand));
		StartCoroutine(spawnislands());
	}


	IEnumerator spawnislands()
	{
		while (geneateLand == true)
		{
			if (!firstLand)
				yield return new WaitForSeconds(landMoveSpeed / islandAmount + -0.1f);
			GameObject newLand = Instantiate(mountainObject, landStartingPos.position, Quaternion.identity);
			if (!firstLand)
			{
				SpawnObstacles(newLand.transform, obstacles[0], 3);
				SpawnObstacles(newLand.transform.GetChild(0), obstacles[0], 12);
				SpawnObstacles(newLand.transform.GetChild(1), obstacles[0], 12);
			}
			StartCoroutine(Lerp(newLand));
			firstLand = false;
		}
	}

	void SpawnObstacles(Transform newland, GameObject obstacle, int numObstacles)
	{
		RaycastHit hit;

		for (var i = 0; i < numObstacles; i++)
		{
			Vector3 spawnPos = PickSpawnLocation(newland, minLandBound.position + newland.position, maxLandBound.position + newland.position);

			if (!Physics.Raycast(spawnPos + (Vector3.up * 10), Vector3.down, 50, obstacleMask) && Physics.Raycast(spawnPos + (Vector3.up * 10), Vector3.down, out hit, 50, islandMask))
			{
				Instantiate(obstacle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.SetParent(newland, true);
			}
		}
	}

	IEnumerator Lerp(GameObject newland)
	{
		float timeElapsed = 0;

		if (startingLandlerp)
		{
			startingLandlerp = false;
			timeElapsed = landMoveSpeed * (4f / islandAmount);
			newland.transform.position = Vector3.Lerp(landStartingPos.position, landEndingPos.position, timeElapsed / landMoveSpeed);
			yield return new WaitForSeconds(landMoveSpeed * (3f / islandAmount));
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

	Vector3 PickSpawnLocation(Transform spawner, Vector3 minSpawnArea, Vector3 maxSpawnArea)
	{
		Vector3 spawnPos = new Vector3();

		spawnPos.x = Random.Range(minSpawnArea.x, maxSpawnArea.x);
		spawnPos.y = spawner.position.y;
		spawnPos.z = Random.Range(minSpawnArea.z, maxSpawnArea.z);

		return spawnPos;
	}
}