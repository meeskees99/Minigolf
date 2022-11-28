using UnityEngine;

public class SoundsTest : MonoBehaviour
{
    [Header("Footsteps")]
    [Header("Import these to the player movement script")]
    [SerializeField] private GameObject[] footstepsFX;
    [SerializeField] private float footstepDelay;
    private float timer;
    [Space(8)]
    [Header("This bool is for testing purposes")]
    public bool walking;
    [Space(20)]
    [Header("Golfball roll")]
    [Header("Import these to the golfball script")]
    [SerializeField] AudioSource rollAudio;
    private bool rollSoundTriggered;
    [Space(8)]
    [Header("This bool is for testing purposes")]
    public bool rolling;

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

        //play roll sound when the ball is rolling
        //replace the rolling bool with a condition that is triggered when the ball is rolling
        if (rolling && !rollSoundTriggered)
        {
            rollAudio.Play();
            rollSoundTriggered = true;
        }
        else if (!rolling && rollSoundTriggered)
        {
            rollAudio.Pause();
            rollSoundTriggered = false;
        }
    }
}
