using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Applied to any enemy that needs to attack/follow players
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [Tooltip("Carries the base variables and gene ranges")]
    public EnemyAISO so;
    
    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _path;
    
    // Private Genes
    private float _attackCooldown;
    private float _timeUntilNextAttack;

    // Start is called before the first frame update
    private void Start()
    {
        // Setup variables
        _target = null;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        
        // Setup genes
        _navMeshAgent.speed = Random.Range(so.moveSpeed.minValue, so.moveSpeed.maxValue);
        _attackCooldown = Random.Range(so.attackSpeed.minValue, so.attackSpeed.maxValue);
        _timeUntilNextAttack = _attackCooldown;
        
        // Having a follow range less then the view range is dumb.
        if (so.followRange < so.viewRange)
        {
            Debug.LogWarning("followRange is less than viewRange. Setting followRange to viewRange.", so);
            so.followRange = so.viewRange;
        }
        
        // Update our target on repeat
        InvokeRepeating(nameof(UpdateTarget), 0f, so.updateTargetFrequency);
    }
    
    /// <summary>
    /// A loop that updates the current target of the enemy.
    /// If the target is outside the follow range, remove it.
    /// If we have no target, and a potential target enters the view range, select them.
    /// </summary>
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
        // Decreases the attack cooldown
        _timeUntilNextAttack -= Time.deltaTime;
        
        // Next parts are movement/attack, which we don't do if there's no target
        if (_target is null)
        {
            return;
        }
        
        // Add references that we use multiple times
        var targetPosition = _target.position;
        var curPosition = transform.position;
        var distanceToTarget = Vector3.Distance(curPosition, _target.transform.position);
        
        // Check if target is within attack range
        if (so.attackRange.minValue < distanceToTarget && distanceToTarget < so.attackRange.maxValue)
        {
            // If we're not ready to attack, we won't
            if (!(_timeUntilNextAttack < 0)) return;
            
            // Attempt an attack and reset _timeUntilNextAttack
            so.MakeAttack(this, targetPosition - curPosition);
            _timeUntilNextAttack = _attackCooldown;
            
            // Tolerance for movement.
            if (so.attackRange.minValue * 1.1 < distanceToTarget && distanceToTarget < so.attackRange.maxValue * 0.9)
                return;
        }
        
        // Move so the target is within the attack range (move towards or move away)
        if (so.attackRange.maxValue > 0)
        {
            float percentageOfDistance;
            if (distanceToTarget > so.attackRange.maxValue)
            {
                percentageOfDistance = 1 - so.attackRange.maxValue / distanceToTarget;
            }
            else
            {
                percentageOfDistance = 1 - so.attackRange.minValue / distanceToTarget;
            }
            var distanceVector = (_target.position - curPosition) * percentageOfDistance;
            targetPosition = curPosition + distanceVector;
        }
        
        // Set the path to move along
        _navMeshAgent.CalculatePath(targetPosition, _path);
        _navMeshAgent.SetPath(_path);
    }

    ///<summary>
    /// Displays the range it will spot a target in red.
    /// Displays the range it will follow a target until in yellow.
    /// Displays the max attack range in blue
    /// Displays the min attack range in cyan
    /// </summary>
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
            Gizmos.DrawWireSphere(origin, so.attackRange.maxValue);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(origin, so.attackRange.minValue);
        }
    }
}
