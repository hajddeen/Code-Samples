using UnityEngine;

public class TouchRaycast : MonoBehaviour
{
    //Other Scripts
    private BallControll ballControll;
    private GameManager gameManager;
    private ObstacleSpawner obstacleSpawner;
    private EnemiesSpawner enemiesSpawner;
    //Start once
    public bool firstHit;
    //Coroutines
    public Coroutine obstacleCorutine;
    public Coroutine enemiesCorutine;
    public void Initialize(BallControll controller, GameManager manager, ObstacleSpawner spawner, EnemiesSpawner enemies)
    {
        //Initialize
        ballControll = controller;
        enemiesSpawner = enemies;
        obstacleSpawner = spawner;
        gameManager = manager; 
    }
    private void Start()
    {
        firstHit = false;
    }
    void Update()
    {
        //MultiTouch
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //Debugging
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 2f); // Draw a red line for 2 seconds
                    if (hit.collider != null)
                    {
                        //Debug.Log("Touched object: " + hit.collider.gameObject.name);
                    }
                }
            }
        }
        StopCorutines();
    }
    //Forces
    private void FixedUpdate()
    {
        //MultiTouch
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //Debugging
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 2f); // Draw a red line for 2 seconds
                    if (hit.collider != null)
                    {
                        Debug.Log("Touched object: " + hit.collider.gameObject.name);
                        if (hit.collider.CompareTag("Ball"))
                        {
                            Debug.Log("BallTouched");
                            if (!firstHit)
                            {
                                //Start level
                                gameManager.gameStarted = true;
                                ballControll.rb.freezeRotation = false;
                                obstacleCorutine = StartCoroutine(obstacleSpawner.PlatformRepeater());
                                enemiesCorutine = StartCoroutine(enemiesSpawner.EnemiesRepeater());
                                firstHit = true;
                            }
                            //Add force to the ball
                            ballControll.AddingForce(hit.point, ballControll.hitForce);
                        }
                    }
                }
            }
        }
    }
    public void StopCorutines()
    {
        //Stop coroutines on loss
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.gameStarted && enemiesCorutine != null && obstacleCorutine != null)
            {
                StopCoroutine(enemiesCorutine);
                StopCoroutine(obstacleCorutine);
            }
        }
    }
}
