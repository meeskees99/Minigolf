using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankScript : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < 3)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            print("hoi");
        }
    }
}
