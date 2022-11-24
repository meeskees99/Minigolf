using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject ballRespawn;
    public Vector3 checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionExit(Collision collision)
    {
        checkpoint = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            GameObject ball = Instantiate(ballRespawn, checkpoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
