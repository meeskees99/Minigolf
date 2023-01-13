using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    private float distance;
    public float turnSpeed;
    public Quaternion enemyRotation;
    public Transform[] waypoints;
    private int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            enemyRotation = Quaternion.LookRotation(waypoints[waypointIndex].transform.position - transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, 1 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, Time.deltaTime * turnSpeed);
    }
}
