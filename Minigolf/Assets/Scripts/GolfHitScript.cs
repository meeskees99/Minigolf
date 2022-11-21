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
    private Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
    }

    void Update()
    {
        clubHolding();
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
        float speed = Vector3.Distance(oldPosition, GetComponent<Collider>().transform.position);
        oldPosition = GetComponent<Collider>().transform.position;
        if (dist < 1)
        {
            golfBall.GetComponent<Rigidbody>().AddForce(GetComponent<Collider>().transform.right * speed);
        }
    }
}
