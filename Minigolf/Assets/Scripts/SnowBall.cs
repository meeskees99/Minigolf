using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    private bool snowBallTooBig;
    void Update()
    {
        if(gameObject.tag == "bigSnowBall")
        {
            var snowBallSpeed = GetComponent<Rigidbody>().velocity.magnitude / 1000;
            if(snowBallTooBig == false)
            {
                GetComponent<Rigidbody>().mass += snowBallSpeed;
                transform.localScale += new Vector3(snowBallSpeed, snowBallSpeed, snowBallSpeed);
            }

            if(transform.localScale.x > 1)
            {
                snowBallTooBig = true;
            }
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
