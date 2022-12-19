using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject ballRespawn;
    private Vector3 checkpoint;
    private GameObject club;
    private RaycastHit hit;
    public GameObject raycastCube;
    public bool ballRolling;
    public float mushroomBounceSpeed;
    private bool insideLog;
    public GameObject boxUnderBall;
    void Start()
    {
        club = GameObject.Find("Putter");
        //Physics.IgnoreCollision(club.GetComponentInChildren<BoxCollider>(), GetComponent<SphereCollider>());
    }

    void ballVoidSpeed()
    {
        //check if ball rolling
        Vector3 ballSpeed = GetComponent<Rigidbody>().velocity;

        if (ballSpeed == new Vector3(0, 0, 0)) //hard code
        {
            ballRolling = false;
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;
        }

        else
        {
            ballRolling = true;
            GetComponent<Rigidbody>().drag += Time.deltaTime;
            GetComponent<Rigidbody>().angularDrag += Time.deltaTime * 3;
        }
        //stopt de bal als het heel langzaam gaat

        if (ballSpeed.x < 0.04f && ballSpeed.z < 0.04f && ballRolling)
        {
            GetComponent<Rigidbody>().drag = 4000;
            GetComponent<Rigidbody>().angularDrag = 4000;
            //Instantiate(boxUnderBall, transform.position - new Vector3(0, 0.4f, 0), Quaternion.identity);
        }
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

        if(collision.gameObject.tag == "Mushroom")
        {
            var direction = (transform.position - collision.transform.position).normalized;
            transform.GetComponent<Rigidbody>().AddForce(raycastCube.transform.up * mushroomBounceSpeed);
        }

        if (collision.gameObject.tag == "flag")
        {
            //kan bal pakken, nu kan de bal altijd gepakken worden
        }

        if (collision.gameObject.tag == "insideLog")
        {
            insideLog = true;
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;
        }
        //in de log
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag  == "insideLog")
        {
            insideLog = false;
        }
        //uit de log
    }

    void Update()
    {
        if (Physics.Raycast(raycastCube.transform.position, -raycastCube.transform.up, out hit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        //checkpoint
        if (insideLog == false)
        {
            ballVoidSpeed();
        }
    }
}
