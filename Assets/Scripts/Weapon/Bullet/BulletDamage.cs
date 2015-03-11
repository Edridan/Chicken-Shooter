using UnityEngine;
using System.Collections;

/// <summary>
/// Graphic bullet
/// Only for polish graphics
/// </summary>

[RequireComponent(typeof(SphereCollider))]
public class BulletDamage : MonoBehaviour 
{
    private int _damage = 1;

	// Use this for initialization
	void Start () 
    {
	}

    public void OnTriggerEnter(Collider _collider)
    {
        if (_collider)
        {
            BulletDamage _otherBullet = _collider.GetComponent<BulletDamage>();

            if (_otherBullet == null)
            {
                HealthScript _healthScript = _collider.GetComponent<HealthScript>();

                if (_healthScript != null && !_healthScript._isPlayer)
                {
                    _healthScript.TakeDamage(_damage);

                    // Destroy the parent
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	}
}
