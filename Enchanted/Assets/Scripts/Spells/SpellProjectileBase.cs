using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellProjectileBase : MonoBehaviour
{
    protected EntityBase caster;
    protected float speed;
    protected Vector3 direction;
    protected float lifeTime;

    public virtual void Initialize(Vector3 direction, float speed, float lifeTime, EntityBase caster)
    {
        this.speed = speed;
        this.direction = direction;
        this.lifeTime = lifeTime;
        this.caster = caster;
    }

    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Environment")
            OnCollideEnvironment(other);
        else if (other.gameObject.tag == "Team2")
        {
            EntityBase entity = other.gameObject.GetComponent<EntityBase>();
            OnCollideEntity(other, entity);
        }
    }

    protected abstract void OnCollideEnvironment(Collider other);
    protected abstract void OnCollideEntity(Collider other, EntityBase entity);
}
