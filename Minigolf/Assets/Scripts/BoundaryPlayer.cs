using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  "Player")
        {
            collision.transform.parent.position = 
        }
    }
}
