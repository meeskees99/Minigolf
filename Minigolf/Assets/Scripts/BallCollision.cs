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
    public float minSpeedLimit;
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
        if (Physics.Raycast(raycastCube.transform.position, -raycastCube.transform.up, out hit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        ballVoidSpeed();
    }

    void ballVoidSpeed()
    {
        //check if ball rolling
        var ballSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        if (ballSpeed == 0) //hard code
        {
            ballRolling = false;
        }

        else
        {
            ballRolling = true;
        }

        if(ballSpeed < minSpeedLimit)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        //minSpeedLimit altijd onder de 0.6, anders uh boem, grapje
    }
}
