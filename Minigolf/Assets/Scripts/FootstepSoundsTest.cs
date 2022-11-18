using UnityEngine;

public class FootstepSoundsTest : MonoBehaviour
{
    [Header("Import these to the player movement script")]
    [SerializeField] private GameObject[] footstepsFX;
    [SerializeField] private float footstepDelay;
    private float timer;
    [Space(20)]
    [Header("This bool is for testing purposes")]
    public bool walking;

    private void Update()
    {
        //instantiate a random footstep sound
        //replace the walking bool with a condition that is triggered when the player is walking
        if (walking)
        {
            timer += Time.deltaTime;

            if (timer >= footstepDelay)
            {
                int randomizer = Random.Range(0, footstepsFX.Length);
                Instantiate(footstepsFX[randomizer], transform.position, Quaternion.identity);
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }
}
