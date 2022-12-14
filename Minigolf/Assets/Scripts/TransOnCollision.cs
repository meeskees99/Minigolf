using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransOnCollision : MonoBehaviour
{
    [SerializeField] Material defaultMat;
    [SerializeField] Material transMat;
    [SerializeField] MeshRenderer mesh;

    [SerializeField] float range;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            mesh.material = transMat;
        }
        else
        {
            mesh.material = defaultMat;
        }
    }
}
