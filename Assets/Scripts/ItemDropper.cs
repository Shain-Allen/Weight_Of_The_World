using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
	[SerializeField]
	private float spawnTimerMax = 10f;
	[SerializeField]
	private float spawnTimerMin = 3f;

	[SerializeField]
	private Transform minSpawnArea;
	[SerializeField]
	private Transform maxSpawnArea;

	[SerializeField]
	private GameObject[] items;

	public bool spawnItems = true;

	[SerializeField]
	private ItemCollisionHandler itemCollisionHandler;

	[SerializeField]
	private LayerMask Obsticles;

	private void Start()
	{
		StartCoroutine(spawnItem());

		itemCollisionHandler.OnGameOver += OnGameOver;
	}

	private void OnGameOver(string message)
	{
		spawnItems = false;
	}

	IEnumerator spawnItem()
	{
		Vector3 spawnPos = new Vector3();

		RaycastHit hit;

		while (spawnItems)
		{
			spawnPos = PickSpawnLocation(transform, minSpawnArea.position, maxSpawnArea.position);

			while(Physics.Raycast(spawnPos, Vector3.down,out hit, 100, Obsticles))
            {
				spawnPos = PickSpawnLocation(transform, minSpawnArea.position, maxSpawnArea.position);
            }

			yield return new WaitForSeconds(Random.Range(spawnTimerMin, spawnTimerMax));
			GameObject item;
			item = Instantiate(items[Random.Range(0, items.Length)], spawnPos, Quaternion.identity);

			if (item.GetComponent<ItemData>())
			{
				item.GetComponent<ItemData>().SetParameters();
			}

			item = null;
		}
	}

	Vector3 PickSpawnLocation(Transform spawner ,Vector3 minSpawnArea, Vector3 maxSpawnArea)
    {
		Vector3 spawnPos = new Vector3();

		spawnPos.x = Random.Range(minSpawnArea.x, maxSpawnArea.x);
		spawnPos.y = spawner.position.y;
		spawnPos.z = Random.Range(minSpawnArea.z, maxSpawnArea.z);

		return spawnPos;
    }
}