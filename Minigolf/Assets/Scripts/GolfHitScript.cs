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
    public GameObject clubCollider;
    public MeshRenderer clubMesh;
    public GameObject golfBall;
    private GameObject instantiatedGolfBall;
    private Vector3 oldClubPosition;
    private Vector3 oldBallPosition;
    public float ballSpeed;
    public bool ballRolling;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
        instantiatedGolfBall = Instantiate(golfBall, new Vector3(0.3f, 1.6f, 1), Quaternion.identity);
        //de spawn location is voor test, idk waar de bal aan het begin gaat spawnen
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
            gameObject.transform.position = rightHandRotation.transform.position - new Vector3(0, 0, 0.4f);
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
        dist = Vector3.Distance(instantiatedGolfBall.transform.position, clubCollider.transform.position);
        float clubSpeed = Vector3.Distance(oldClubPosition, GetComponent<Collider>().transform.position);
        oldClubPosition = GetComponent<Collider>().transform.position;
        if (dist < 0.1f && ballRolling == false)
        {
            var direction = (GetComponent<Collider>().transform.position - golfBall.transform.position).normalized;
            print("hoi");
            golfBall.transform.GetComponent<Rigidbody>().AddForce(direction * clubSpeed);
        }
    }

    void ballHit()
    {
        ballSpeed = Vector3.Distance(oldBallPosition, golfBall.transform.position);
        if(ballSpeed < 0.0001f) //hard code
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
