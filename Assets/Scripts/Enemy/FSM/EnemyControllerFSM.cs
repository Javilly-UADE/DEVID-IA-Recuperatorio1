using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerFSM : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform player;

    private Rigidbody playerRb;
    private LineOfSight los;
    private FSM_Classes fsm;

    [SerializeField]
    private List<Transform> patrolPoints;

    [Header("Movement")]
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float predictionTime = 0.5f;

    [SerializeField]
    private float fleeDistance = 3f;

    [SerializeField]
    private float patrolPointReachDistance = 0.5f;

    private Rigidbody rb;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        los = GetComponent<LineOfSight>();
        fsm = GetComponent<FSM_Classes>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool canSeePlayer =
            los.isInRange(transform, player) &&
            los.isInAngle(transform, player) &&
            los.hasLineOfSight(transform, player);

        bool isClose =
            Vector3.Distance(transform.position, player.position) <= fleeDistance;

        fsm.UpdateState(canSeePlayer, isClose);

        ExecuteState();
    }

    private void ExecuteState()
    {
        if (fsm.CurrentState is PatrolState)
        {
            Patrol();
        }
        else if (fsm.CurrentState is PursuitState)
        {
            PursuePlayer();
        }
        else if (fsm.CurrentState is FleeState)
        {
        }
    }

    private void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Count == 0)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Transform target = patrolPoints[currentPatrolIndex];

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        if (toTarget.magnitude < patrolPointReachDistance)
        {
            rb.linearVelocity = Vector3.zero;
            currentPatrolIndex++;

            if (currentPatrolIndex >= patrolPoints.Count)
            {
                currentPatrolIndex = 0;
            }

            return;
        }

        Vector3 dir = SteeringBehaviours.Seek(transform, target.position);
        Move(dir);
    }

    private void PursuePlayer()
    {
        Vector3 dir = SteeringBehaviours.Pursue(transform, player, playerRb, predictionTime);
        Move(dir);
    }

    private void FleePlayer()
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