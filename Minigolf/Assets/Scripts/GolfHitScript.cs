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
        /*
        if(Input.GetKeyDown(KeyCode.E))
        {
            transform.position = rightHand.transform.position - new Vector3(0, 0, 0.3f);
            transform.rotation = rightHand.transform.rotation;
            holdingStick = true;
            rightHand.SetActive(false);
            gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.E) && holdingStick)
        {
            gameObject.SetActive(false);
            rightHand.SetActive(true);
        }
        */
        
    }

    private void DestroyClub(InputAction.CallbackContext obj)
    {
        if(hasClub)
        {
            club.SetActive(false);
            print("hoi");
            hasClub = false;
            //rightHand.SetActive(true);

        }

        if(hasClub == false)
        {
            club.SetActive(true);
            hasClub = true;
            club.transform.position = rightHand.transform.position - new Vector3(0, 0, 0.3f);
            club.transform.rotation = rightHand.transform.rotation;
            //rightHand.SetActive(false);
        }
    }
}
