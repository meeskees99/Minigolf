using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float turnSpeed;
    public Quaternion enemyRotation;
    public Transform[] waypoints;
    private int waypointIndex;
    private bool ballInElevator;
    public float timer;
    public GameObject door;
    public float doorSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ballInElevator)
        {
            door.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 1, 1), doorSpeed);
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f && waypointIndex != 3)
            {
                waypointIndex++;
                enemyRotation = Quaternion.LookRotation(waypoints[waypointIndex].transform.position - transform.position);
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, 1 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, Time.deltaTime * turnSpeed);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "golfBall")
        {
            timer += Time.deltaTime;
            if(timer > 1)
            {
                ballInElevator = true;
            }
        }
    }
}
