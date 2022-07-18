using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
	public ItemValue itemValues;

	public void SetParameters()
	{
		this.GetComponent<Rigidbody>().drag = itemValues.itemDrag;
	}
}
