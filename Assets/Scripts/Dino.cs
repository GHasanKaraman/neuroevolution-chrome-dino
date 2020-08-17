using Mathematic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public NeuralNetwork brain;

    private Rigidbody2D rigid;
    private Animator animator;

    public float velocity;

    private bool canSlidable;
    private bool isGrounded;
    public bool dead;

    public double distanceX;
    public double FlyingDinoX;

    public float age;

    public double outputSlide;
    public double outputNoSlide;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isGrounded = true;
        canSlidable = false;

        brain = new NeuralNetwork(2, 6, 6, 4);
    }

    public void generateNN(NeuralNetwork nn)
    {
        if (nn == null)
        {
            brain = new NeuralNetwork(2, 6, 6, 4);
        }

        else
        {
            brain = nn.Clone() as NeuralNetwork;
        }
    }

    void Update()
    {
        age += Time.deltaTime;

        if (getClosestObstacle() != null)
        {
            GameObject obs = getClosestObstacle();

            if (obs.transform.position.y == -1.1f)
            {
                FlyingDinoX = obs.transform.position.x - transform.position.x;
                distanceX = 0;
            }

            else
            {
                distanceX = obs.transform.position.x - transform.position.x;
                FlyingDinoX = 0;
            }
        }

        double[,] inputs = new double[,] { { distanceX, FlyingDinoX } };

        double[,] predicts = brain.Predict(inputs);

        double outputJump = predicts[0, 0];
        double outputNoJump = predicts[0, 1];
        outputSlide = predicts[0, 2];
        outputNoSlide = predicts[0, 3];

        //Debug.LogError($"Jump={outputJump} NoJump={outputNoJump} ---- Slide={outputSlide} NoSlide={outputNoSlide}");

        //Debug.LogError(FlyingDinoX);
        if (isGrounded && outputJump > outputNoJump && FlyingDinoX == 0)
        {
            isGrounded = false;
            rigid.velocity = Vector2.up * velocity;
        }

        if (isGrounded && outputSlide > outputNoSlide && distanceX == 0 && canSlidable)
        {
            canSlidable = false;
            animator.ResetTrigger("slide");
            animator.SetTrigger("slide");
        }

        if (outputNoSlide > outputSlide)
        {
            canSlidable = true;
        }

        if (dead)
        {
            AI.deadDinos.Add(this);
            AI.dinos.Remove(this);
            Destroy(gameObject);
        }
    }

    private GameObject getClosestObstacle()
    {
        foreach (GameObject item in Spawner.obstacles)
        {
            if (transform.position.x < item.transform.position.x)
            {
                return item;
            }
        }

        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = true;
            canSlidable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "FlyingDino")
        {
            dead = true;
        }
    }
}