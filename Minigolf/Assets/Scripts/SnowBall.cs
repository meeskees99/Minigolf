using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    void Update()
    {
        if(gameObject.tag == "bigSnowBall")
        {
            var snowBallSpeed = GetComponent<Rigidbody>().velocity.magnitude / 1000;
            Mathf.Clamp(transform.localScale.x, 0, 1.4f); 
            transform.localScale += new Vector3(snowBallSpeed, snowBallSpeed, snowBallSpeed);
            GetComponent<Rigidbody>().mass += snowBallSpeed;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "enviorment")
        {
            Destroy(gameObject);
            //particle?
        }

        if(gameObject.tag == "smallSnowBalls")
        {
            GetComponent<BoxCollider>().isTrigger = true;
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
