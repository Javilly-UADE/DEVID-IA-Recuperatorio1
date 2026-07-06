using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    private DecisionNode rootNode;

    private void Awake()
    {
        ActionNode patrolNode = new ActionNode(enemy => enemy.Patrol());
        ActionNode pursueNode = new ActionNode(enemy => enemy.PursuePlayer());
        ActionNode fleeNode = new ActionNode(enemy => enemy.FleePlayer());

        rootNode = new QuestionNode(
            context =>
                context.los.isInRange(context.self, context.player) &&
                context.los.isInAngle(context.self, context.player) &&
                context.los.hasLineOfSight(context.self, context.player),
            pursueNode,
            patrolNode
        );
    }

    public void Evaluate(EnemyControllerDT enemy, EnemyContext context)
    {
        rootNode.Evaluate(enemy, context);
    }
}