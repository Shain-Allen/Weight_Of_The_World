using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollisionHandler : MonoBehaviour
{
	[SerializeField]
	BucketControl leftBucket;
	[SerializeField]
	BucketControl rightBucket;
	[SerializeField]
	BalanceBar balanceDisplay;
	[SerializeField]
	FloorHandler floor;

	private int leftBucketWeight = 0;
	private int rightBucketWeight = 0;
	[SerializeField]
	private int balanceCapacity = 10;

	public float publicbalanceDirection = 0;

	[SerializeField]
	private int ggCapacity = 4;
	private int godsGrace = 0;

	[SerializeField]
	private GameObject missedObjectsDisplay;

	public event Action<string> OnGameOver;

	public AudioSource audioSauce;

	public AudioClip catchSound;
	public AudioClip itemMissed;

	private void Start()
	{
		leftBucket.OnObjectCollected += OnObjectCollected;
		rightBucket.OnObjectCollected += OnObjectCollected;
		floor.OnObjectMissed += OnObjectMissed;

		missedObjectsDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText($"Missed Objecs: {godsGrace}/{ggCapacity}");
	}

	private void OnObjectCollected(string bucketName, GameObject collectedObject)
	{
		//Debug.Log(bucketName + " cought " + collectedObject.name);
		if (collectedObject.GetComponent<ItemData>())
		{

			if (bucketName == "Left Bucket")
			{
				leftBucketWeight += collectedObject.GetComponent<ItemData>().itemValues.itemWeight;
			}
			else
			{
				rightBucketWeight += collectedObject.GetComponent<ItemData>().itemValues.itemWeight;
			}
		}
		
		if(collectedObject.tag == "forgiveness")
		{
            godsGrace--;
			UpdateMissedObjectsText();
        }

		float balanceDirection = rightBucketWeight - leftBucketWeight;

		publicbalanceDirection = balanceDirection;

		balanceDisplay.value = Mathf.Clamp((float)balanceDirection / (float)balanceCapacity, -1, 1);

		//Debug.Log("sliderValue: " + Mathf.Clamp((float)balanceDirection / (float)balanceCapacity, -1, 1));

		if (balanceDirection < 0)
		{
			balanceDirection *= -1;
		}

		switch (Mathf.Clamp((float)balanceDirection / (float)balanceCapacity, -1, 1))
		{
			case < 0.25f:
				balanceDisplay.fillRect.GetComponent<Image>().color = Color.green;
				break;
			case < 0.5f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(0.9803922f, 0.7176471f, 0.2f);
				break;
			case < 0.75f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(1, 0.5568628f, 0.08235294f);
				break;
			case < 0.95f:
				balanceDisplay.fillRect.GetComponent<Image>().color = new Color(1, 0.3058824f, 0.06666667f);
				break;
			case > 0.95f:
				balanceDisplay.fillRect.GetComponent<Image>().color = Color.red;
				break;
		}

		Destroy(collectedObject);

		audioSauce.PlayOneShot(catchSound);

		if (balanceDirection >= balanceCapacity)
		{
			OnGameOver?.Invoke("You have become unbalanced and fell down the mountain");
		}
	}

	private void OnObjectMissed(GameObject other)
	{
		Destroy(other);

		

		if (other.tag != "forgiveness")
		{
            audioSauce.PlayOneShot(itemMissed);
            godsGrace++;
        }
		
		UpdateMissedObjectsText();

		if (godsGrace == ggCapacity)
		{
			OnGameOver?.Invoke("you have displeased the Gods. They dislike having their things touch the ground");
		}
	}

	private void UpdateMissedObjectsText()
	{
        missedObjectsDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText($"Missed Objecs: {godsGrace}/{ggCapacity}");
    }
}