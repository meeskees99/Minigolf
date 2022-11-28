using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "map")
        {
            Destroy(gameObject);
        }
    }
}
