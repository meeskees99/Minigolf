using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class GolfHitScript : MonoBehaviour
{
    private bool holdingStick;
    public GameObject rightHand;
    [SerializeField] private InputActionReference clubActionReference;
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
        Destroy(gameObject);
        print("hoi");
    }
}
