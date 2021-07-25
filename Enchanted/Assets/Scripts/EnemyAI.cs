using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Location")]
    public float updateTargetFrequency = 0.5f;
    public float viewRange = 3;
    public float followRange = 5;
    public float pathUpdateFrequency = 0.1f;
    
    [Header("Attacks")]
    public float maxAttackRange;
    public float minAttackRange;
    public bool usesRangedAttack;
    public SpellBase spell;

    [Header("Genes")]
    [Tooltip("Temporarily here to trial genes in an easy way")]
    public float moveSpeed;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _path;
    
    // Start is called before the first frame update
    private void Start()
    {
        // Setup our initial variables.
        _target = null;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = moveSpeed;
        _path = new NavMeshPath();
        
        // Having a follow range less then the view range is dumb.
        if (followRange < viewRange)
        {
            Debug.LogWarning("followRange is less than viewRange. Setting followRange to viewRange.",
                gameObject);
            followRange = viewRange;
        }
        
        // Update our target on repeat
        InvokeRepeating(nameof(UpdateTarget), 0f, updateTargetFrequency);
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
            if (distanceToEnemy > followRange)
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
        if (nearestTarget != null && shortestDistance <= viewRange)
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
        
        if (minAttackRange < distanceToTarget && distanceToTarget < maxAttackRange)
        {
            return;
        }
        
        // Generates a distance part of the way to to target
        if (usesRangedAttack && maxAttackRange > 0)
        {
            float percentageOfDistance;
            if (distanceToTarget > maxAttackRange)
            {
                percentageOfDistance = 1 - maxAttackRange / distanceToTarget;
            }
            else
            {
                percentageOfDistance = 1 - minAttackRange / distanceToTarget;
            }
            var distanceVector = (_target.position - curPosition) * percentageOfDistance;
            targetPosition = curPosition + distanceVector;
        }
        
        _navMeshAgent.CalculatePath(targetPosition, _path);
        _navMeshAgent.SetPath(_path);
    }

    /*
     * Displays the range it will spot a target in red.
     * Displays the range it will follow a target until in yellow.
     * Displays the max attack range in blue
     * Displays the min attack range in cyan
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.yellow;
        // Use viewRange is followRange is smaller
        Gizmos.DrawWireSphere(transform.position, followRange > viewRange ? followRange : viewRange);
        if (usesRangedAttack)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxAttackRange);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, minAttackRange);
        }
    }
}
