using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GolfHitScript : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject rightHandRotation;
    public GameObject[] rightHandObjects;
    [SerializeField] private InputActionReference clubActionReference;
    [SerializeField] private InputActionReference ballSpawnActionReference;
    private bool hasClub;
    public GameObject clubCollider;
    public MeshRenderer[] clubMesh;
    public GameObject golfBall;
    public GameObject instantiatedGolfBall;
    private Vector3 oldClubPosition;
    public bool ballRolling;
    public float clubForce;
    public Transform spawn;

    [SerializeField] Image[] dimmedImages;
    [SerializeField] float dimSpeed;
    [SerializeField] float loadTime;


    public PlayerScript playerscript;

    public float clubSpeed;
    public float dist;
    private float stickLengthValue;
    public Transform shaft;

    static public int ballHitCounter;
    private bool ballCooldown;
    private float ballCooldownTimer;

    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
        ballSpawnActionReference.action.performed += RespawnBall;
        instantiatedGolfBall = Instantiate(golfBall, spawn.position, Quaternion.identity);
        playerscript.ball = instantiatedGolfBall;
        instantiatedGolfBall.GetComponent<BallManager>().dimmedImages = dimmedImages;
        instantiatedGolfBall.GetComponent<BallManager>().dimSpeed = dimSpeed;
        instantiatedGolfBall.GetComponent<BallManager>().loadTime = loadTime;
        //disable collision between player and golf stick
        Physics.IgnoreCollision(clubCollider.GetComponent<BoxCollider>(), playerBody.GetComponent<CharacterController>());
    }

    void Update()
    {
        clubHolding();
        clubOnGround();
        ballRolling = instantiatedGolfBall.GetComponent<BallManager>().ballRolling;
        if (ballRolling)
        {
            clubCollider.SetActive(false);
        }

        else
        {
            clubCollider.SetActive(true);
        }
        //kijken of bal rolt

        
            
        
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
        //remove or get club
    }

    void clubHolding()
    {
        if (hasClub)
        {
            for (int i = 0; i < clubMesh.Length; i++)
            {
                clubMesh[i].enabled = true;
            }
          
            for (int i = 0; i < rightHandObjects.Length; i++)
            {
                rightHandObjects[i].SetActive(false);
            }
            gameObject.transform.position = rightHandRotation.transform.position;
            gameObject.transform.rotation = rightHandRotation.transform.rotation;
        }

        if (hasClub == false)
        {
            for (int i = 0; i < clubMesh.Length; i++)
            {
                clubMesh[i].enabled = false;
            }

            for (int i = 0; i < rightHandObjects.Length; i++)
            {
                rightHandObjects[i].SetActive(true);
            }
        }
    }
  

    void clubCollision()
    {
        ballCooldownTimer += Time.deltaTime;
        if (ballRolling == false)
        {
            clubSpeed = Vector3.Distance(oldClubPosition, clubCollider.transform.position) * clubForce;
            oldClubPosition = clubCollider.transform.position;
            dist = Vector3.Distance(instantiatedGolfBall.transform.position, clubCollider.transform.position);
            if (dist < 0.3 && clubSpeed > 70 || dist < 0.08 && clubSpeed < 70 || dist < 0.03f && clubSpeed < 1 || clubSpeed < 600 && clubSpeed > 10 && dist < 0.4)
            {
                instantiatedGolfBall.transform.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 direction = (clubCollider.transform.position - instantiatedGolfBall.transform.position).normalized;
                instantiatedGolfBall.transform.GetComponent<Rigidbody>().AddForce(-direction * clubSpeed);
                if(ballCooldownTimer > 1)
                {
                    ballHitCounter++;
                    ballCooldownTimer = 0;
                }
 
                if (direction.x + direction.y + direction.z > 0)
                {
                    //instantiatedGolfBall.transform.GetComponent<Rigidbody>().AddForce(-direction * clubSpeed);
                }                
            }                                                    
        }
        //eigen gemaakte collider die nodig is
    }

    void clubOnGround()
    {
        if(Physics.Raycast(clubCollider.transform.position, -transform.up, out RaycastHit hit, 2) && hit.transform.tag == "map")
        {
            if(hit.point.y + 0.01f < clubCollider.transform.position.y && shaft.localScale.y < 2)
            {
                stickLengthValue += 0.03f;
                shaft.localScale = new Vector3(1, stickLengthValue, 1);
            }
        }

        else if(shaft.localScale.y > 1)
        {
            stickLengthValue -= 0.03f;
            shaft.localScale = new Vector3(1, stickLengthValue, 1);
        }
    }

    private void RespawnBall(InputAction.CallbackContext obj)
    {
        GameObject newBall = Instantiate(golfBall, instantiatedGolfBall.GetComponent<BallManager>().checkpoint, Quaternion.identity);
        Destroy(instantiatedGolfBall);
        instantiatedGolfBall = newBall;
    }
}
