using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class ExplosionScript : MonoBehaviour 
{

    public int _explosionDamage = 1;

    public float _explosionTime = 0.2F;

    public float _explosionForce = 15F;

    private SphereCollider _sphereCollider;

	// Use this for initialization
	void Start () 
    {
       _sphereCollider = GetComponent<SphereCollider>();
	}

    // Manage the physics
    void ManagePhysics(Collider collider)
    {
        // Character Motor
        CharacterMotor _characterMotor = collider.GetComponent<CharacterMotor>();
        float _distance = Vector3.Distance(collider.transform.position, transform.position);

        // If it's a character
        if (_characterMotor != null)
        {
            Vector3 _force = (collider.transform.position - transform.position) * _explosionForce ;

            // Apply physic to the character Motor
            //Debug.Log("Apply force : " + _force + " to : " + collider.name + " mult coeff = " + _explosionForce );
            _characterMotor.SetVelocity(_force);
        }
        else
        {
            Rigidbody _rigidBody = collider.GetComponent<Rigidbody>();

            if (_rigidBody != null)
            {
                // Apply physic to rigidBody

                _rigidBody.velocity = ((collider.transform.position - transform.position) * (_explosionForce / _distance));
                _rigidBody.AddExplosionForce(_explosionForce, transform.position, _sphereCollider.radius);
            }
        }


        // Rigid Body
    }

    // Collision detection
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger enter " + collider.name);

        HealthScript _life = collider.GetComponent<HealthScript>(); // Get the life script

        ManagePhysics(collider);

        if (_life != null)
        {
            _life.TakeDamage(_explosionDamage);
        }
    }

    void Update()
    {
        // Fix for the moment
        transform.Translate(new Vector3(0, 0, 0));

        _explosionTime -= Time.deltaTime;   // Manage the explosion time
        if (_explosionTime <= 0)
        {
            _sphereCollider.enabled = false;
        }
    }
}
