using UnityEngine;

public class EnemyControllerDT : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform player;
    private Rigidbody playerRb;
    private LineOfSight los;
    private EnemyDecisionTree decisionTree;

    [Header("Movement")]
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float predictionTime = 0.5f;

    private EnemyContext context;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        los = GetComponent<LineOfSight>();
        decisionTree = GetComponent<EnemyDecisionTree>();
        playerRb = player.GetComponent<Rigidbody>();

        context = new EnemyContext
        {
            self = transform,
            player = player,
            los = los
        };
    }

    private void Update()
    {
        context.player = player;
        decisionTree.Evaluate(this, context);
    }

    public void Patrol()
    {
        transform.Rotate(Vector3.up * 30f * Time.deltaTime);
    }

    public void PursuePlayer()
    {
        Vector3 dir = SteeringBehaviours.Pursue(transform, player, playerRb, predictionTime);
        Move(dir);
    }

    public void FleePlayer()
    {
        Vector3 dir = SteeringBehaviours.Evade(transform, player, playerRb, predictionTime);
        Move(dir);
    }

    private void Move(Vector3 dir)
    {
        Vector3 velocity = dir * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        if (dir != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(
                transform.forward,
                dir,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}