using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class GolfHitScript : MonoBehaviour
{
    public GameObject rightHandRotation;
    public GameObject rightHandObject;
    [SerializeField] private InputActionReference clubActionReference;
    private bool hasClub;
    public GameObject club;
    // Start is called before the first frame update
    void Start()
    {
        clubActionReference.action.performed += DestroyClub;
    }

    void Update()
    {

        if(hasClub)
        {
            club.SetActive(true);
            rightHandObject.SetActive(false);
            club.transform.position = rightHandRotation.transform.position - new Vector3(0, 0, 0.6f);
            club.transform.rotation = rightHandRotation.transform.rotation;
            //rightHand.SetActive(false);
            
        }

        if (hasClub == false)
        {
            club.SetActive(false);
            rightHandObject.SetActive(true);
        }

    }

    private void DestroyClub(InputAction.CallbackContext obj)
    {
        if(hasClub)
        {
            hasClub = false;
            //rightHand.SetActive(true);

        }

        else
        {
            hasClub = true;
        }
    }
}
