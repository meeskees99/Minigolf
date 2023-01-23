using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankScript : MonoBehaviour
{
    public Transform player;
    public Transform plankRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < 3)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, plankRotation.rotation, Time.deltaTime);
            
            print("hoi");
        }
    }
}
