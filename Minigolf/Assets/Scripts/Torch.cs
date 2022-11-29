using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        //melt snow
        if(other.gameObject.tag == "snow")
        {
            //other.gameObject.transform.Translate(-Vector3.up * Time.deltaTime);
        }
    }
}
