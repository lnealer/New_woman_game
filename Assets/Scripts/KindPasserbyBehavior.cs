using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KindPasserbyBehavior : MonoBehaviour
{
    public float scale;
    public int dignityDamage;
    public float walkSpeed;
    public GameObject leaveGoal;

    public bool interacted = false;
    public bool isGenerous;
    private Animator animator;
    private GameObject woman;
    private bool isSpeaking;
    public string speech= "";
    public string kindSpeech;
    public string rudeSpeech;
    public float speechTimer, interval;
    public Transform chatBubble;
    public Transform chatText;


     private void Awake() 
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("xVelocity", 0f);
    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     // when the player encounters the woman, look at the player
    
    //     if (collider.gameObject.tag == "Player")
    //     {   
            
    //     }
    // }

    private void Update()
    {
        speechTimer += Time.deltaTime;
        if (interacted & !isGenerous)
        {
            Leave();
        }
        if (speechTimer >= interval)
        {
            speechTimer = 0;
            StopSpeaking();
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        // if the player presses the interact key, trigger an rng to either give the player a coin
        // or gossip and strip the player of dignity
        if (collider.gameObject.tag == "Player")
        {
            LookAt(collider.gameObject);
            if (!interacted)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    Debug.Log("Interacted with passerby");
                    isGenerous = ActionDecider();
                    if (isGenerous)
                    {
                        speech = kindSpeech;
                    }
                    else
                    {
                        speech = rudeSpeech;
                    }
                    interacted = true;
                    if (isGenerous)
                    {
                        GiveCoin();
                    }
                    else 
                    {
                        Gossip();
                    }

                    Speak();
                }
            }
            else if (interacted & !isGenerous)
            {
                LookAway(collider.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            TurnAround();
        }
    }


    void GiveCoin()
    {
        Debug.Log("Passerby was generous");
        FindObjectOfType<CoinCount>().GetCoin(); // gain 1 coin

    }

    void Gossip()
    {
        Debug.Log("Passerby was cruel");
        FindObjectOfType<Dignity>().LoseDignity(dignityDamage); // decrement player dignity by damage amount
    }

    void Leave()
    {
        Transform goal = leaveGoal.transform;
        LookAt(leaveGoal);
        // walk out of frame
        animator.SetFloat("xVelocity", walkSpeed);
        // move enemy towards goal
        transform.position = Vector2.MoveTowards(transform.position, goal.position, walkSpeed*Time.deltaTime);
        //check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goal.transform.position) < 1f)
        {
            // destroy once out of frame
            Destroy(transform.parent.gameObject);
        }
    }

    private void Start()
    {
        scale = transform.localScale.y;
    }

    private bool ActionDecider()
    {
        int randomNum = Random.Range(0,2);
        if (randomNum == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void TurnAround()
    {
        // turn object to face opposite direction of current
        transform.localScale = new Vector3(-1*transform.localScale.x, scale, scale);
    }

    void LookAt(GameObject player)
    {
        // turn towards player
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }
        else 
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
    }

    void LookAway(GameObject player)
    {
        // look away from player
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;   
        }
        else 
        {
            transform.localScale = new Vector3(-1,1,1)*scale;
        }
    }

    void Speak()
    {
        isSpeaking = true;
        chatBubble.gameObject.SetActive(true);
        chatText.gameObject.GetComponent<TMP_Text>().SetText(speech);
        chatText.gameObject.SetActive(true);
        //chatBubble = ChatBoxBehavior.Create(gameObject.transform, new Vector2(3f, 3f), text);
    }

    void StopSpeaking()
    {
        isSpeaking = false;
        chatBubble.gameObject.SetActive(false);
        chatText.gameObject.SetActive(false);
        //Destroy(chatBubble);
    }


}
