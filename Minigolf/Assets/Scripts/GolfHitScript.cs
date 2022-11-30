using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class GolfHitScript : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject rightHandRotation;
    public GameObject[] rightHandObjects;
    [SerializeField] private InputActionReference clubActionReference;
    private bool hasClub;
    public GameObject clubCollider;
    public MeshRenderer clubMesh;
    public GameObject golfBall;
    public GameObject instantiatedGolfBall;
    private Vector3 oldClubPosition;
    public bool ballRolling;
    public float clubForce;
    public Transform spawn;
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
        instantiatedGolfBall = Instantiate(golfBall, spawn.position, Quaternion.identity);
    }

    void Update()
    {
        clubHolding();
        clubCollision();
        ballRolling = instantiatedGolfBall.GetComponent<BallCollision>().ballRolling;
    }
    private void DestroyClub(InputAction.CallbackContext obj)
    {
        if(hasClub)
        {
            hasClub = false;
        }

        else
        {
            hasClub = true;
        }
    }

    void clubHolding()
    {
        if (hasClub)
        {
            clubMesh.enabled = true;
            for (int i = 0; i < rightHandObjects.Length; i++)
            {
                rightHandObjects[i].SetActive(false);
            }
            gameObject.transform.position = rightHandRotation.transform.position;
            gameObject.transform.rotation = rightHandRotation.transform.rotation;
        }

        if (hasClub == false)
        {
            clubMesh.enabled = false;
            for (int i = 0; i < rightHandObjects.Length; i++)
            {
                rightHandObjects[i].SetActive(true);
            }
        }
    }
  

    void clubCollision()
    {
        /*
        float dist = Vector3.Distance(instantiatedGolfBall.transform.position, clubCollider.transform.position);
        float clubSpeed = Vector3.Distance(oldClubPosition, clubCollider.transform.position) * clubForce;
        oldClubPosition = clubCollider.transform.position;
        if (dist < 0.07f && ballRolling == false)
        {
            var direction = (clubCollider.transform.position - golfBall.transform.position).normalized;
            instantiatedGolfBall.transform.GetComponent<Rigidbody>().AddForce(-direction * clubSpeed);
        }
        
        eigen gemaakte collider die misschien nodig is
        */

        if(ballRolling)
        {
            clubCollider.SetActive(false);
        }

        else
        {
            clubCollider.SetActive(true);
        }

        //disable collision between player and golf stick
        Physics.IgnoreCollision(clubCollider.GetComponent<BoxCollider>(), playerBody.GetComponent<CharacterController>());
    }
}
