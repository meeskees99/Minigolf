using UnityEngine;

public class DespawnScript : MonoBehaviour
{
    [SerializeField] private float despawnTime;
    [SerializeField] private GameObject despawnObject;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= despawnTime)
        {
            Destroy(despawnObject);
        }
    }
}
