using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProtection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            Destroy(gameObject);
        }
    }
}
