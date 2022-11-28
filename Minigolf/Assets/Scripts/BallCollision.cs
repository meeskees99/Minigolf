using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject ballRespawn;
    private Vector3 checkpoint;
    public GameObject club;
    private RaycastHit hit;
    public GameObject raycastCube;
    public bool ballRolling;
    private Vector3 oldBallPosition;
    // Start is called before the first frame update
    void Start()
    {
        club = GameObject.Find("Putter");
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            ballRespawn = Instantiate(ballRespawn, checkpoint, Quaternion.identity);
            club.GetComponent<GolfHitScript>().instantiatedGolfBall = ballRespawn;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        bool ballRolling = club.GetComponent<GolfHitScript>().ballRolling;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        ballVoidSpeed();
    }

    void ballVoidSpeed()
    {
        float ballSpeed = Vector3.Distance(oldBallPosition, transform.position);
        oldBallPosition = transform.position;
        if (ballSpeed < 0.0001f) //hard code
        {
            ballRolling = false;
        }

        else
        {
            ballRolling = true;
        }
    }
}
