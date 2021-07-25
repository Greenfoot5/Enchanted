using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/AI")]
public class EnemyAISO : ScriptableObject
{
    public float updateTargetFrequency = 0.5f;
    public float viewRange = 3;
    public float followRange = 5;
    
    [Header("Attacks")]
    public float maxAttackRange;
    public float minAttackRange;
    public bool usesRangedAttack;

    [Header("Genes")]
    [Tooltip("Temporarily here to trial genes in an easy way")]
    // Consider using other NavMeshAgent variables
    public float moveSpeed = 1;
}
