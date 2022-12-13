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
    public GameObject grabBall;
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
            GetComponent<Rigidbody>().angularDrag = 0;
        }

        else
        {
            ballRolling = true;
            GetComponent<Rigidbody>().drag += Time.deltaTime;
            GetComponent<Rigidbody>().angularDrag += Time.deltaTime;
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

        if(collision.gameObject.tag == "Mushroom")
        {
            var direction = (transform.position - collision.transform.position).normalized;
            transform.GetComponent<Rigidbody>().AddForce(raycastCube.transform.up * mushroomBounceSpeed);
        }

        if (collision.gameObject.tag == "flag")
        {
            Instantiate(grabBall, transform.position, Quaternion.identity);
            club.GetComponent<GolfHitScript>().instantiatedGolfBall = grabBall;
            Destroy(gameObject);
            //spawn een bal die de speler wel op kan pakken op de positie van de bal
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
