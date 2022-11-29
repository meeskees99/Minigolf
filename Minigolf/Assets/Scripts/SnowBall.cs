using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    void Update()
    {
        var snowBallSpeed = GetComponent<Rigidbody>().velocity.magnitude / 1000;
        transform.localScale = transform.localScale + new Vector3(snowBallSpeed, snowBallSpeed, snowBallSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enviorment")
        {
            Destroy(gameObject);
            //particle?
        }
    }
}
