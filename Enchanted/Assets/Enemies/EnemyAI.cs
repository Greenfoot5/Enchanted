using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyAISO so;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _path;
    
    // Start is called before the first frame update
    private void Start()
    {
        _target = null;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = so.moveSpeed;
        _path = new NavMeshPath();
        
        // Having a follow range less then the view range is dumb.
        if (so.followRange < so.viewRange)
        {
            Debug.LogWarning("followRange is less than viewRange. Setting followRange to viewRange.",
                so);
            so.followRange = so.viewRange;
        }
        
        // Update our target on repeat
        InvokeRepeating(nameof(UpdateTarget), 0f, so.updateTargetFrequency);
    }
    
    /*
     * A loop that updates the current target of the enemy.
     * If the target is outside the follow range, remove it.
     * If we have no target, and a potential target enters the view range, select them.
     */
    private void UpdateTarget()
    {
        // Check if the current target is within our follow range.
        if (_target != null)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, _target.transform.position);
            if (distanceToEnemy > so.followRange)
            {
                _target = null;
            }
            else
            {
                return;
            }
        }
        
        // Create a list of players and remember shortest distance and enemy
        var targets = GameObject.FindGameObjectsWithTag("Player");
        var shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        // Loop through the enemies and find the closest
        foreach (var target in targets)
        {
            var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            
            // Save if new closest enemy
            if (!(distanceToTarget < shortestDistance)) continue;
            shortestDistance = distanceToTarget;
            nearestTarget = target;
        }

        // Check if we have a target and should shoot
        if (nearestTarget != null && shortestDistance <= so.viewRange)
        {
            _target = nearestTarget.transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_target is null)
        {
            return;
        }

        var targetPosition = _target.position;
        var curPosition = transform.position;
        var distanceToTarget = Vector3.Distance(curPosition, _target.transform.position);
        
        if (so.maxAttackRange < distanceToTarget && distanceToTarget < so.maxAttackRange)
        {
            MakeRangedAttack();
            return;
        }
        
        // If we're using a ranged attack, move so the target is within the attack range (move towards or move away)
        if (so.usesRangedAttack && so.maxAttackRange > 0)
        {
            float percentageOfDistance;
            if (distanceToTarget > so.maxAttackRange)
            {
                percentageOfDistance = 1 - so.maxAttackRange / distanceToTarget;
            }
            else
            {
                percentageOfDistance = 1 - so.minAttackRange / distanceToTarget;
            }
            var distanceVector = (_target.position - curPosition) * percentageOfDistance;
            targetPosition = curPosition + distanceVector;
        }
        
        _navMeshAgent.CalculatePath(targetPosition, _path);
        _navMeshAgent.SetPath(_path);
    }

    private void MakeRangedAttack()
    {
        // TODO - Actually make an attack at the target.
    }

    /*
     * Displays the range it will spot a target in red.
     * Displays the range it will follow a target until in yellow.
     * Displays the max attack range in blue
     * Displays the min attack range in cyan
     */
    private void OnDrawGizmosSelected()
    {
        var origin = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, so.viewRange);
        Gizmos.color = Color.yellow;
        // Use viewRange is followRange is smaller
        Gizmos.DrawWireSphere(origin, so.followRange > so.viewRange ? so.followRange : so.viewRange);
        if (so.usesRangedAttack)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(origin, so.maxAttackRange);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(origin, so.minAttackRange);
        }
    }
}
