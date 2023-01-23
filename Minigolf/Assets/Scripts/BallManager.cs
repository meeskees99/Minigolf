using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    public GameObject ballRespawn;
    public Vector3 checkpoint;
    private GameObject club;
    private RaycastHit checkpointHit;
    private RaycastHit hit;
    public GameObject raycastCube;
    public bool ballRolling;
    public float mushroomBounceSpeed;
    private bool insideLog;
    private bool insideObstacle;
    [SerializeField] AudioSource rollAudio;
    private bool rollSoundTriggered;
    [SerializeField] GameObject confetti;
    [SerializeField] string targetLeaderboard;
    private Vector3 oldSpeed;
    //private Transform oldPreviousTransform;
    //private Transform oldNewTransform;
    public float canonSpeed;
    [SerializeField] float waitTimeForNextScene;
    public Image[] dimmedImages;
    public float dimSpeed;
    public float loadTime;
    void Start()
    {
        club = GameObject.Find("Putter");
        //Physics.IgnoreCollision(club.GetComponentInChildren<BoxCollider>(), GetComponent<SphereCollider>());
    }

    void ballVoidSpeed()
    {
        //check if ball rolling
        Vector3 ballSpeed = GetComponent<Rigidbody>().velocity;

        if (ballSpeed == new Vector3(0, 0, 0)) //hard code
        {
            ballRolling = false;
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;
            //drag reset
        }

        else
        {
            ballRolling = true;
            if(insideLog == false && insideObstacle == false) //in de log die de bal snel maakt?
            {
                GetComponent<Rigidbody>().drag += Time.deltaTime;
                GetComponent<Rigidbody>().angularDrag += Time.deltaTime * 3;
                //bal gaat slomer na zo veel tijd
            }
        }

        if (ballSpeed.x < 0.04f && ballSpeed.z < 0.04f && ballRolling && insideLog == false && insideObstacle == false)
        {
            
            Vector3 newSpeed = GetComponent<Rigidbody>().velocity;
            //kijken of de bal slomer gaat
            if (Physics.Raycast(transform.position, -raycastCube.transform.up, out hit, 2))
            {
                if (hit.transform.gameObject.tag == "map" && oldSpeed.x + oldSpeed.z > newSpeed.x + newSpeed.z)
                {
                    GetComponent<Rigidbody>().drag = 4000;
                    GetComponent<Rigidbody>().angularDrag = 4000;
                    //stopt de bal als het heel langzaam gaat
                    print("hallo");
                }
            }
            oldSpeed = GetComponent<Rigidbody>().velocity;
            //GetComponent<Rigidbody>().drag = 4000;
            //GetComponent<Rigidbody>().angularDrag = 4000;
        }

        if (ballRolling && !rollSoundTriggered)
        {
            rollAudio.Play();
            rollSoundTriggered = true;
        }
        else if (!ballRolling && rollSoundTriggered)
        {
            rollAudio.Pause();
            rollSoundTriggered = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            //ballRespawn = Instantiate(ballRespawn, checkpoint, Quaternion.identity);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = checkpoint;
            //club.GetComponent<GolfHitScript>().instantiatedGolfBall = ballRespawn;
            //Destroy(gameObject);
            Destroy(gameObject);
            //respawn
        }

        if(collision.gameObject.tag == "Mushroom")
        {
            var direction = (transform.position - collision.transform.position).normalized;
            transform.GetComponent<Rigidbody>().AddForce(raycastCube.transform.up * mushroomBounceSpeed);
            //tegen mushroom met bounce
        }

        if (collision.gameObject.tag == "flag")
        {
            gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            //kan bal pakken, nu kan de bal altijd gepakken worden
        }

        if(collision.gameObject.tag == "canon")
        {
            transform.position = collision.transform.position + new Vector3(-1, 1, -1);
            GetComponent<Rigidbody>().AddForce(collision.transform.forward * canonSpeed);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "insideLog")
        {
            GameObject logCheckpoint = GameObject.FindGameObjectWithTag("logCheckpoint");
            insideLog = true;
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;

            Vector3 dir = logCheckpoint.transform.position - transform.position;
            dir = dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * Time.deltaTime);
        }

        if(collision.gameObject.tag == "insideObstacle")
        {
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().angularDrag = 0;
            insideObstacle = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag  == "insideLog")
        {
            insideLog = false;
        }

        if(collision.gameObject.tag == "insideObstacle")
        {
            insideObstacle = false;
        }
        //uit de log

        if(collision.gameObject.tag == "flag")
        {
            //ga naar andere scene
            StartCoroutine(Transition("MainMenu", waitTimeForNextScene));
            Instantiate(confetti, collision.transform);
            //SceneManager.LoadScene(nextScene);
            SendLeaderboard(GolfHitScript.ballHitCounter);
            GolfHitScript.ballHitCounter = 0;
        }
    }

    void Update()
    {
        if (Physics.Raycast(raycastCube.transform.position, -raycastCube.transform.up, out checkpointHit, 1) && ballRolling == false)
        {
            checkpoint = transform.position;
        }
        //checkpoint
        if (insideLog == false && insideObstacle == false)
        {
            ballVoidSpeed();
        }
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = targetLeaderboard,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while sending leaderboard!");
        Debug.Log(error.GenerateErrorReport());
    }

    IEnumerator Transition(string sceneName, float waitTime)
    {
        foreach (Image dimmedImage in dimmedImages)
        {
            var tempColor = dimmedImage.color;
            tempColor.a += dimSpeed * Time.deltaTime;
            dimmedImage.color = tempColor;
        }
        yield return new WaitForEndOfFrame();
        if (dimmedImages[0].color.a < 1)
        {
            StartCoroutine(Transition(sceneName, 0));
        }
        else
        {
            yield return new WaitForSeconds(loadTime);
            SceneManager.LoadScene(sceneName);
        }
    }
}
