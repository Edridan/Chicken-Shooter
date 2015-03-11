using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Rocket : MonoBehaviour 
{
    public float _speed = 10F;
    public int _explosionDamage = 1;

    public GameObject _explosionPrefab = null;

	// Use this for initialization
	void Start () 
    {
	    if (_explosionPrefab == null)
        {
            Debug.LogWarning("Warning : No explosion prefab");
        }
	}

    void Update()
    {
        if (transform.position.y < 0)
        {
            OnCollisionEnter(null);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_explosionPrefab != null)
        {
            // Apply the explosion
            ExplosionScript _explosion = ((GameObject)(Instantiate(_explosionPrefab, transform.position, Quaternion.identity))).GetComponent<ExplosionScript>();
            
            // Apply the damage and explosion
            _explosion._explosionDamage = _explosionDamage;

            Destroy(gameObject);
         }
    }
}
