using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public float health = 10f;
    public float damage = 1f;

    private void Start()
    {
        health = MaxHealth();
        Debug.Log($"{gameObject.name} has {health} health");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
            Debug.Log($"{gameObject.name} has {health} health remaining");
        }
    }

    protected abstract float MaxHealth();

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            OnDeath();
            Debug.Log($"{gameObject.name} has died");
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<HealthBase>(out HealthBase target))
        {
            TakeDamage(target.damage);
            if (health > 0f)
            {
                Debug.Log($"{gameObject.name} has {health} health remaining");
            }
        }
    }

}
