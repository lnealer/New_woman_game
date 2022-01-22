using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class SuitorBehavior : MonoBehaviour
{
   public List<Transform> points; //waypoints
    public float speed;
    [SerializeField] private int nextId = 0; // index of next point
    int idChangeValue = 1; // change of index each iteration
    public float scale;
    public float dignityDamage;
    
    private ChatBoxBehavior chatBox;
    private GameObject suitor;
    //private Collider2D collider;
    private Animator animator;
    public Transform chatText;
    public Transform chatBubble;
    public string speech = "Marry me!";
    public float speechTimer, interval;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        // create root object
        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position; // move root to enemy pos
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints"); // make waypoints object
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        // init points list
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    private void Start()
    {
        scale = transform.localScale.y;
    }

    private void Update()
    {
        animator.SetFloat("xVelocity", speed);
        MoveToNextPoint();

        // stop speaking after some time interval
        speechTimer += Time.deltaTime;
        if (speechTimer >= interval)
        {
            speechTimer = 0;
            StopSpeaking();
        }
    }

    void MoveToNextPoint()
    {
        // get the next point transform
        Transform goal = points[nextId];
        // flip the enemy transform to look to point direction
        if (goal.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }
        
        // move enemy towards goal
        transform.position = Vector2.MoveTowards(transform.position, goal.position, speed*Time.deltaTime);
        //check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goal.transform.position) < 1f)
        {
            // Check if you are at the last point, then make delta -1 (otherwise +1)
            if (nextId == points.Count-1)
            {
                nextId = 0;
            }
            else 
            {
                nextId += idChangeValue;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerBehavior>().pushBack();
            Speak();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FindObjectOfType<Dignity>().LoseDignity(dignityDamage); // decrement player dignity by 10
            FindObjectOfType<PlayerBehavior>().changeDignityDamageColor(); // show damage by flash color change
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerBehavior>().changeNormalColor(); // return to normal color
        }
    }

    void Speak()
    {
        chatBubble.gameObject.SetActive(true);
        chatText.gameObject.GetComponent<TMP_Text>().SetText(speech);
        chatText.gameObject.SetActive(true);
        //chatBubble = ChatBoxBehavior.Create(gameObject.transform, new Vector2(3f, 3f), text);
    }

    void StopSpeaking()
    {
        chatBubble.gameObject.SetActive(false);
        chatText.gameObject.SetActive(false);
    }
}
