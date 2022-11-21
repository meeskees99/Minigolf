using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class GolfHitScript : MonoBehaviour
{
    public GameObject rightHand;
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
            club.transform.position = rightHand.transform.position - new Vector3(0, 0, 0.3f);
            club.transform.rotation = rightHand.transform.rotation;
            //rightHand.SetActive(false);
            
        }

        if (hasClub == false)
        {
            club.SetActive(false);
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
