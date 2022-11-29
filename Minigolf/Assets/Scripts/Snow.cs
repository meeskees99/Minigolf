using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    private float shrinkSpeed = 0.01f;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "torch")
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - shrinkSpeed, transform.localScale.z);
        }
    }

    void Update()
    {
        if(transform.localScale.y < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
