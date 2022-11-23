using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private GameObject club;
    public GameObject ballRespawn;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        club = GameObject.Find("Putter");
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary" && isGrounded == false)
        {
            Vector3 checkpoint = club.GetComponent<GolfHitScript>().checkpointGolfBall;
            GameObject ball = Instantiate(ballRespawn, checkpoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
