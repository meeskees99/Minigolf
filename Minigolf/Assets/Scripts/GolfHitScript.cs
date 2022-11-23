using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class GolfHitScript : MonoBehaviour
{
    public GameObject rightHandRotation;
    public GameObject[] rightHandObjects;
    [SerializeField] private InputActionReference clubActionReference;
    private bool hasClub;
    public GameObject club;
    public MeshRenderer clubMesh;
    public GameObject golfBall;
    private Vector3 oldClubPosition;
    private Vector3 oldBallPosition;
    public float ballSpeed;
    public bool ballRolling;
    // Start is called before the first frame update
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
    }

    void Update()
    {
        clubHolding();
        ballHit();
    }

    void FixedUpdate()
    {
        clubCollision();
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
            club.transform.position = rightHandRotation.transform.position - new Vector3(0, 0, 0.4f);
            club.transform.rotation = rightHandRotation.transform.rotation;
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
        float dist = Vector3.Distance(golfBall.transform.position, GetComponent<Collider>().transform.position);
        float clubSpeed = Vector3.Distance(oldClubPosition, GetComponent<Collider>().transform.position);
        oldClubPosition = GetComponent<Collider>().transform.position;
        if (dist < 0.1f && ballRolling == false)
        {
            //golfBall.GetComponent<Rigidbody>().AddForce(GetComponent<Collider>().transform.right * clubSpeed * Time.deltaTime);
            var direction = (GetComponent<Collider>().transform.position - golfBall.transform.position).normalized;
            print("hoi");
            golfBall.transform.GetComponent<Rigidbody>().AddForce(direction * clubSpeed);
        }
    }

    void ballHit()
    {
        ballSpeed = Vector3.Distance(oldBallPosition, golfBall.transform.position);
        if(ballSpeed < 0.001f) //hard code
        {
            ballRolling = false;
        }

        else
        {
            ballRolling = true;
        }
        oldBallPosition = golfBall.transform.position;
    }
}
