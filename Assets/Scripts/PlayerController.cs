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

	[SerializeField]
	private Animator anim;

	private ItemCollisionHandler itemCH;

    [SerializeField]
    private float weightScaleFactor = 128;


	void Awake()
	{
		// 0 for no sync, 1 for panel refresh rate, 2 for 1/2 panel rate
		QualitySettings.vSyncCount = 1;

		playerTrans = GetComponent<Transform>();

		itemCH = GetComponent<ItemCollisionHandler>();
	}


	// Update is called once per frame
	void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        AnimatePole();

        if (moveDir != Vector3.zero)
        {
            moveDir = moveDir * movementSpeed * Time.deltaTime;

            if (!anim.GetBool("PoleRight") && !anim.GetBool("PoleLeft"))
            {
                moveDir += new Vector3(itemCH.publicbalanceDirection/weightScaleFactor, 0, 0);
            }
            else if (anim.GetBool("PoleLeft"))
            {
                moveDir += new Vector3(0, 0, itemCH.publicbalanceDirection / weightScaleFactor);
            }
            else if (anim.GetBool("PoleRight"))
            {
                moveDir += new Vector3(0, 0, -1 * (itemCH.publicbalanceDirection / weightScaleFactor));
            }
        }

        playerTrans.position += moveDir;

        playerTrans.position = ClampVector3(playerTrans.position, MinOuterBound.position, MaxOuterBound.position);

        moveDir = Vector3.zero;

        RaycastHit raycastResults;

        if (Physics.Raycast(HipLocation.position, Vector3.down, out raycastResults, 100f, floorLayer))
        {
            playerTrans.position = new Vector3(playerTrans.position.x, raycastResults.point.y, playerTrans.position.z);
        }
    }

    private void AnimatePole()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("PoleLeft", true);
        }
        else
        {
            anim.SetBool("PoleLeft", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("PoleRight", true);
        }
        else
        {
            anim.SetBool("PoleRight", false);
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