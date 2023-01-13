using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BallManager : MonoBehaviour
{
    public GameObject ballRespawn;
    public Vector3 checkpoint;
    private GameObject club;
    private RaycastHit hit;
    public GameObject raycastCube;
    public bool ballRolling;
    public float mushroomBounceSpeed;
    private bool insideLog;
    private bool insideObstacle;
    //private Transform oldPreviousTransform;
    //private Transform oldNewTransform;
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
            //drag reset
        }

        else
        {
            ballRolling = true;
            if(insideLog == false && insideObstacle == false) //in de log die de bal snel maakt?
            {
                GetComponent<Rigidbody>().drag += Time.deltaTime;
                GetComponent<Rigidbody>().angularDrag += Time.deltaTime * 3;
                //bal gaat slomer na zo veel tijd
            }
        }

        if (ballSpeed.x < 0.04f && ballSpeed.z < 0.04f && ballRolling && insideLog == false && insideObstacle == false)
        {
            /*
            Vector3 oldSpeed = GetComponent<Rigidbody>().velocity;
            Vector3 newSpeed = GetComponent<Rigidbody>().velocity;
            //kijken of de bal slomer gaat

            if(oldSpeed.x + oldSpeed.y + oldSpeed.z > newSpeed.x + newSpeed.z + newSpeed.y)
            {
                GetComponent<Rigidbody>().drag = 4000;
                GetComponent<Rigidbody>().angularDrag = 4000;
                //stopt de bal als het heel langzaam gaat
                print("hoi");
            }
            */

            GetComponent<Rigidbody>().drag = 4000;
            GetComponent<Rigidbody>().angularDrag = 4000;
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
            //tegen mushroom met bounce
        }

        if (collision.gameObject.tag == "flag")
        {
            gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            //kan bal pakken, nu kan de bal altijd gepakken worden
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "insideLog")
        {
            GameObject logCheckpoint = GameObject.FindGameObjectWithTag("logCheckpoint");
            insideLog = true;
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;

            Vector3 dir = logCheckpoint.transform.position - transform.position;
            dir = dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * Time.deltaTime);
        }

        if(collision.gameObject.tag == "insideObstacle")
        {
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;
            insideObstacle = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag  == "insideLog")
        {
            insideLog = false;
        }

        if(collision.gameObject.tag == "insideObstacle")
        {
            insideObstacle = false;
        }
        //uit de log

        if(collision.gameObject.tag == "flag")
        {
            //ga naar andere scene
        }
    }

    void Update()
    {
        if (Physics.Raycast(raycastCube.transform.position, -raycastCube.transform.up, out hit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        //checkpoint
        if (insideLog == false && insideObstacle == false)
        {
            ballVoidSpeed();
        }
    }
}
