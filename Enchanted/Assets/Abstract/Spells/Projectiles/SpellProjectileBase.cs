using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>The base for a spell projectile.</para>
/// It does not it itself and its children do not store the stats about the spell.
/// They get their stats by getting them assigned.
/// </summary>
public abstract class SpellProjectileBase : MonoBehaviour
{
    // Stats about the spell.
    public EntityBase caster;
    public float speed;
    public float lifeTime;

    // Direction of the spell.
    protected Vector3 direction;

    /// <summary>
    /// Sends the projectile in a certain direction.
    /// </summary>
    /// <param name="caster">The caster of the spell.</param>
    /// <param name="direction">The direction of the spell sent.</param>
public virtual void Initialize(EntityBase caster, IProjectileData data, Vector3 direction)
{
    // Set the caster of the spell to track damage routes.
    this.caster = caster;

    // Set up the data of the projectile.
    speed = data.Speed;
    lifeTime = data.LifeTime;

    // Set direction to go in.
    this.direction = direction;
}

    /// <summary>
    /// The function running and checking the lifetime of the projectile.
    /// </summary>
    void Update()
    {
        // Track life left.
        lifeTime -= Time.deltaTime;

        // Destroy if life reaches 0.
        if (lifeTime <= 0)
            Destroy(gameObject);

        // If still alive, move the projectile forward.
        else
            transform.Translate(speed * Time.deltaTime * direction);
    }

    /// <summary>
    /// The function checking collisions and calling methods when colliding with certain objects.
    /// </summary>
    protected virtual void OnTriggerEnter(Collider other)
    {
        // If colided with the environment, destroy itself.
        if (other.gameObject.tag == "Environment")
            OnCollideEnvironment(other);

        // If collided with an entity.
        else if (other.gameObject.TryGetComponent(out EntityBase entity))
        {
            // If allied
            if (caster.tag == other.gameObject.tag)
                OnCollideAlliedEntity(other, entity);
            else
                OnCollideEnemyEntity(other, entity);
        }
    }

    /// <summary>
    /// <para>The collision event when hit the environment.</para>
    /// Usually useful for simply destroying the projectile.
    /// </summary>
    /// <param name="other">The collider hit.</param>
    protected abstract void OnCollideEnvironment(Collider other);

    /// <summary>
    /// <para>The collision event when hit an allied entity.</para>
    /// Usually useful for healing and affecting the enemy entity in some way.
    /// </summary>
    /// <param name="other">The collider hit.</param>
    /// <param name="entity">The allied entity.</param>
    protected virtual void OnCollideAlliedEntity(Collider other, EntityBase entity) { }

    /// <summary>
    /// <para>The collision event when hit an enemy entity.</para>
    /// Usually useful for damaging and affecting the enemy entity in some way.
    /// </summary>
    /// <param name="other">The collider hit.</param>
    /// <param name="entity">The enemy entity.</param>
    protected virtual void OnCollideEnemyEntity(Collider other, EntityBase entity) { }
}
