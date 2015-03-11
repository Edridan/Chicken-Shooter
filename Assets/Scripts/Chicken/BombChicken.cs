using UnityEngine;
using System.Collections;


public class BombChicken : ChickenScript
{
    // AI parameter
    public float _jumpDistance = 10F;
    public float _explodeDistance = 2F;

    public GameObject _explosionPrefab = null;

	// Use this for initialization
	new void Start () 
    {
	    base.Start();
	}

    void Explode()
    {
        if (_explosionPrefab != null)
        {
            GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            // Die
            _life.Die();
        }
    }

	// Update is called once per frame
	new void Update () 
    {
        float _distance = Vector3.Distance(transform.position, _player.transform.position);

        if (_distance <= _jumpDistance)
        {
            _characterMotor.inputJump = true;
        }


        if (_distance <= _explodeDistance)
        {
            Explode();
        }
        else
        {
            // Update the direction
            _direction = (_player.transform.position - transform.position);

            ManageRotation();

            _characterMotor.inputMoveDirection = Vector3.Normalize(_direction);
        }        
	}
}
