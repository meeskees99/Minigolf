using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float stepSize;
    [SerializeField] GameObject[] hardMatFootstepsFX;
    [SerializeField] GameObject playerOrgin;
    [SerializeField] GameObject cam;
    [SerializeField] Vector3 heightOffset;
    Vector3 prevPos;
    [Space(20)]
    [Header("Compass")]
    [SerializeField] GameObject compass;
    public GameObject ball;
    [SerializeField] bool lookingForBall;
    [SerializeField] InputActionReference compasActionReference;
    [SerializeField] GameObject raycastGlow;
    private RaycastHit checkpointHit;
    private Vector3 checkpoint;
    private RaycastHit hit;
    public GameObject[] interactableObjects;
    [Header("Display amount of hits")]
    [SerializeField] GameObject hitsPaper;
    [SerializeField] TextMeshProUGUI hitsDisplay;

    private void Start()
    {
        prevPos = transform.position;
        compasActionReference.action.performed += Compas;
    }

    private void Update()
    {
        //plays a random footstep sound when you move more than the stepsize
        Vector3 currentPos = playerOrgin.transform.position;
        //currentPos.y = prevPos.y;
        float distanceToPrevpos = Vector3.Distance(currentPos, prevPos);
        if (distanceToPrevpos >= stepSize)
        {
            prevPos = currentPos;
            int randomizer = Random.Range(0, hardMatFootstepsFX.Length);
            Vector3 footstepRotation = cam.transform.rotation.eulerAngles;
            footstepRotation.z = 0;
            footstepRotation.x = 0;
            Instantiate(hardMatFootstepsFX[randomizer], currentPos  - heightOffset, Quaternion.Euler(footstepRotation));
        }

        //compass points to the ball
        compass.SetActive(lookingForBall);
        hitsPaper.SetActive(!lookingForBall);
        if (lookingForBall)
        {
            Vector3 targetDirection = compass.transform.position - ball.transform.position;
            Vector3 compassRotation = Vector3.RotateTowards(compass.transform.forward, targetDirection, 180 * Time.deltaTime, 0.0f);
            compass.transform.rotation = Quaternion.LookRotation(compassRotation);
            compass.transform.rotation = Quaternion.Euler(0, compass.transform.eulerAngles.y, compass.transform.eulerAngles.z);
        }
        else
        {
            Debug.Log(GolfHitScript.ballHitCounter);
            hitsDisplay.text = GolfHitScript.ballHitCounter.ToString();
        }

        if (Physics.Raycast(playerOrgin.transform.position, -playerOrgin.transform.up, out checkpointHit, 1) && checkpointHit.transform.gameObject.tag != "Boundary")
        {
            checkpoint = playerOrgin.transform.position;
            if (checkpointHit.transform.gameObject.tag == "Boundary")
            {
                playerOrgin.transform.position = checkpoint;
            }
        }
        //checkpoint systeem voor player

        RaycastInteraction();
    }

    private void Compas(InputAction.CallbackContext obj)
    {
        if(compass.activeInHierarchy)
        {
            lookingForBall = false;
        }

        else
        {
            lookingForBall = true;
        }
    }

    
    private void RaycastInteraction()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10000))
        { 
            if (hit.transform.gameObject.tag == "insideObstacle")
            {
                hit.transform.gameObject.GetComponent<Outline>().enabled = true;
            }

            else
            {
                for (int i = 0; i < interactableObjects.Length; i++)
                {
                    interactableObjects[i].transform.gameObject.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
