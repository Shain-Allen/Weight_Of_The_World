using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
	[SerializeField]
	private float radius = 5f;

	[SerializeField]
	private float spawnTimerMax = 10f;
	[SerializeField]
	private float spawnTimerMin = 3f;

	[SerializeField]
	private GameObject[] items;

	public bool spawnItems = true;


	private float NumPoints = 32;

	private void OnDrawGizmos()
	{
		float lastPoint = 0;
		for (int i = 0; i < NumPoints; i++)
		{
			float newPoint = lastPoint + (360 / NumPoints);
			Gizmos.DrawLine(PointOnCircle(transform.position, radius, lastPoint), PointOnCircle(transform.position, radius, newPoint));
			lastPoint = newPoint;
		}

	}

	private void Start()
	{
		StartCoroutine(spawnItem());
	}

	IEnumerator spawnItem()
	{
		Vector3 spawnPos = new Vector3();

		while (spawnItems)
		{
			float angle = Random.Range(0, 360);
			spawnPos = PointOnCircle(transform.position, radius, angle);

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

	Vector3 PointOnCircle(Vector3 _Origin, float _radius, float angle)
	{
		return new Vector3(_Origin.x + radius * Mathf.Sin(angle * (Mathf.PI / 180)), _Origin.y, _Origin.z + radius * Mathf.Cos(angle * (Mathf.PI / 180)));
	}
}