using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    void Update()
    {
        if(gameObject.tag == "bigSnowBall")
        {
            var snowBallSpeed = GetComponent<Rigidbody>().velocity.magnitude / 10000;
            transform.localScale = transform.localScale + new Vector3(snowBallSpeed, snowBallSpeed, snowBallSpeed);
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
