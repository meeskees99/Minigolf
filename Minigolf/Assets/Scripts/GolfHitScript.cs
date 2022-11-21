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
    // Start is called before the first frame update
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
    }

    void Update()
    {
        clubHolding();

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
}
