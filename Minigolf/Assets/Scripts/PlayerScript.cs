using UnityEngine;

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
    [SerializeField] GameObject ball;
    [SerializeField] bool lookingForBall;

    private void Start()
    {
        prevPos = transform.position;
    }

    private void Update()
    {
        //plays a random footstep sound when you move more than the stepsize
        Vector3 currentPos = playerOrgin.transform.position;
        currentPos.y = prevPos.y;
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
        if (lookingForBall)
        {
            Vector3 targetDirection = compass.transform.position - ball.transform.position;
            Vector3 compassRotation = Vector3.RotateTowards(compass.transform.forward, targetDirection, 180 * Time.deltaTime, 0.0f);
            compass.transform.rotation = Quaternion.LookRotation(compassRotation);
            compass.transform.rotation = Quaternion.Euler(0, compass.transform.eulerAngles.y, compass.transform.eulerAngles.z);
        }
    }
}
