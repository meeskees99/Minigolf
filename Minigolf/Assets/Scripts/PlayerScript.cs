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
    //RaycastHit floorCheck;

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
        //Physics.Raycast(playerOrgin.transform.position, -Vector3.up, out floorCheck, 4);
        //if (distanceToPrevpos >= stepSize && floorCheck.transform.gameObject.CompareTag("HardFloor"))
        //{
        //    prevPos = currentPos;
        //    int randomizer = Random.Range(0, hardMatFootstepsFX.Length);
        //    Instantiate(hardMatFootstepsFX[randomizer], playerOrgin.transform.position, Quaternion.identity);
        //}
        if (distanceToPrevpos >= stepSize)
        {
            prevPos = currentPos;
            int randomizer = Random.Range(0, hardMatFootstepsFX.Length);
            Vector3 footstepRotation = cam.transform.rotation.eulerAngles;
            footstepRotation.z = 0;
            footstepRotation.x = 0;
            GameObject footstep = Instantiate(hardMatFootstepsFX[randomizer], currentPos  - heightOffset, Quaternion.Euler(footstepRotation));
        }
    }
}
