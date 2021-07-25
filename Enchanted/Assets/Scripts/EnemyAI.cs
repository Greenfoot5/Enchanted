using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    [Header("Enemy Location")]
    public float updateTargetFrequency = 0.5f;
    public float viewRange = 3;
    public float followRange = 5;
    
    [Header("Attacks")]
    public float attackRange;
    public bool usesRangedAttack;
    public SpellBase spell;

    [Header("Genes")]
    [Tooltip("Temporarily here to trial genes in an easy way")]
    public float moveSpeed = 0;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        // Setup our initial variables.
        _target = null;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = moveSpeed;
        
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
        var players = GameObject.FindGameObjectsWithTag("Player");
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through the enemies and find the closest
        foreach (var player in players)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, player.transform.position);
            
            // Save if new closest enemy
            if (!(distanceToEnemy < shortestDistance)) continue;
            shortestDistance = distanceToEnemy;
            nearestEnemy = player;
        }

        // Check if we have a target and should shoot
        if (nearestEnemy != null && shortestDistance <= viewRange)
        {
            _target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_target is null)
        {
            return;
        }
        // TODO - Generate a path to allow the enemy to stop before reaching the target
        
        _navMeshAgent.SetDestination(_target.position);
    }

    /*
     * Displays the range it will spot a target in red.
     * Displays the range it will follow a target until in yellow.
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}
