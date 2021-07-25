using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyAISO so;
    
    // Target location data
    private float _updateTargetFrequency = 0.5f;
    private float _viewRange = 3;
    private float _followRange = 5;
    
    // Attack data
    private float _maxAttackRange;
    private float _minAttackRange;
    private bool _usesRangedAttack;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _path;
    
    // Start is called before the first frame update
    private void Start()
    {
        // Setup our initial variables.
        _updateTargetFrequency = so.updateTargetFrequency;
        _viewRange = so.viewRange;
        _followRange = so.followRange;

        _maxAttackRange = so.maxAttackRange;
        _minAttackRange = so.minAttackRange;
        _usesRangedAttack = so.usesRangedAttack;

        _target = null;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = so.moveSpeed;
        _path = new NavMeshPath();
        
        // Having a follow range less then the view range is dumb.
        if (_followRange < _viewRange)
        {
            Debug.LogWarning("followRange is less than viewRange. Setting GameObject followRange to viewRange.",
                so);
            _followRange = _viewRange;
        }
        
        // Update our target on repeat
        InvokeRepeating(nameof(UpdateTarget), 0f, _updateTargetFrequency);
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
            if (distanceToEnemy > _followRange)
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
        if (nearestTarget != null && shortestDistance <= _viewRange)
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
        
        if (_minAttackRange < distanceToTarget && distanceToTarget < _maxAttackRange)
        {
            MakeRangedAttack();
            return;
        }
        
        // Generates a distance part of the way to to target
        if (_usesRangedAttack && _maxAttackRange > 0)
        {
            float percentageOfDistance;
            if (distanceToTarget > _maxAttackRange)
            {
                percentageOfDistance = 1 - _maxAttackRange / distanceToTarget;
            }
            else
            {
                percentageOfDistance = 1 - _minAttackRange / distanceToTarget;
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
        Gizmos.DrawWireSphere(origin, _viewRange);
        Gizmos.color = Color.yellow;
        // Use viewRange is followRange is smaller
        Gizmos.DrawWireSphere(origin, _followRange > _viewRange ? _followRange : _viewRange);
        if (_usesRangedAttack)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(origin, _maxAttackRange);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(origin, _minAttackRange);
        }
    }
}
