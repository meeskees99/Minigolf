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
    public Vector3 minSpeedLimitVector;
    void Start()
    {
        club = GameObject.Find("Putter");
    }

    void ballVoidSpeed()
    {
        //check if ball rolling
        Vector3 ballSpeed = GetComponent<Rigidbody>().velocity;
        if (ballSpeed == new Vector3(0, 0, 0)) //hard code
        {
            ballRolling = false;
            GetComponent<Rigidbody>().drag = 0;
        }

        else
        {
            ballRolling = true;
            GetComponent<Rigidbody>().drag += Time.deltaTime;
        }
        //stopt de bal als het heel langzaam gaat
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            ballRespawn = Instantiate(ballRespawn, checkpoint, Quaternion.identity);
            club.GetComponent<GolfHitScript>().instantiatedGolfBall = ballRespawn;
            Destroy(gameObject);
            //respawn
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "flag")
        {
            //bal kan nu uit het gat gepakt worden
        }
    }

    void Update()
    {
        if (Physics.Raycast(raycastCube.transform.position, -raycastCube.transform.up, out hit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        //checkpoint
        ballVoidSpeed();
    }
}
