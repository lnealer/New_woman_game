using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefBehavior : MonoBehaviour
{
    
    public List<Transform> points; //waypoints
    public float speed;
    public float fleeingSpeed;
    public float thievingSpeed;
    public float walkSpeed;
    [SerializeField] private int nextId = 0; // index of next point
    public int idChangeValue = 1; // change of index each iteration
    public float scale;
    public GameObject escapeGoal;
    public GameObject player;

    [SerializeField] private Transform goal;
    [SerializeField] private bool coinStolen = false;
    [SerializeField] private bool isThieving = false;
    private bool isEscaping;
    private GameObject thief;
    //private Collider2D collider;
    private Animator animator;


    private void Reset()
    {
        // // create root object
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

        // initialize the escape goal
        escapeGoal = new GameObject("ThiefEscapeGoal");
        escapeGoal.transform.SetParent(root.transform);
        escapeGoal.transform.position = root.transform.position;
    }

        private void Awake() 
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("xVelocity", walkSpeed);
    }

    private void Start()
    {
        scale = transform.localScale.x;
    }

      private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if (!isThieving)
        {
            // get the next point transform
            goal = points[nextId];
        }
        MoveToPoint(goal);
    }


    void FlipCharacter()
    {
        //float scaleX = transform.localScale.x;
        //bool flipped = false;
        // flip the enemy transform to look to point direction
        if (goal.transform.position.x > transform.position.x)
        {
            //flipped = scaleX < 0;
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            //flipped = scaleX > 0;
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }

        // if (flipped & !isThieving)
        // {
        //   PlayIdle();
        // }
    }

    void MoveToPoint(Transform goal)
    {
        // this function handles movement to goals and updates next goal 
        // including for escaping and running toward player behaviors

        // flip the enemy transform to look to point direction
        FlipCharacter();

        // move enemy towards goal
        transform.position = Vector2.MoveTowards(transform.position, goal.position, speed*Time.deltaTime);
        //check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goal.transform.position) < 1f)
        {
            if (isEscaping)
            {
                Destroy(gameObject);
            }
            else if (coinStolen & !isEscaping)
            {
                // if the thief successfully stole the coin, destroy once he escapes
               // Destroy(gameObject);
                Escape();
                
            }
            else if (isThieving & !coinStolen)
            {
                FindObjectOfType<PlayerBehavior>().pushBack(); 
                StealCoin();
            }
            else
            {
                // Check if you are at the last point, then make delta -1 (otherwise +1)
                if (nextId == points.Count-1)
                {
                    nextId -= idChangeValue;
                }
                else 
                {
                    nextId += idChangeValue;
                }
            }
        }
    }

    void Run(Transform point, float moveSpeed)
    {
        // set a new goal and run at speed towards it
        animator.SetFloat("xVelocity", moveSpeed);
        speed = moveSpeed;
        goal = point;
    }


    void StealCoin()
    {
        // steal coin and escape
        FindObjectOfType<CoinCount>().LostCoin(); // lose 1 coin
        coinStolen = true;
    }

    void Escape()
    {
        Run(escapeGoal.transform, fleeingSpeed);
        isEscaping = true;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!coinStolen)
            {
                isThieving = true;
                Run(collider.gameObject.transform, thievingSpeed);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!coinStolen)
        {
            isThieving = false;
            speed = walkSpeed;
            animator.SetFloat("xVelocity", walkSpeed);
        }
    }

}
