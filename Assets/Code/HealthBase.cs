using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth = 10f;
    public float damage = 1f;

    //example of inspector manipulation
    [SerializeField]
    [Range(0, 1)]
    private float privateSerializedVar;

    private float privateVar;

    [HideInInspector]
    public float publicVar;


	private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} has {currentHealth} health");
    }

	private void OnValidate() {
		currentHealth = maxHealth;
	}

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
            Debug.Log($"{gameObject.name} has {currentHealth} health remaining");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0f)
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
            if (currentHealth > 0f)
            {
                Debug.Log($"{gameObject.name} has {currentHealth} health remaining");
            }
        }
    }
}
