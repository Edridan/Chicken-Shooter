using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class TommyGun : MonoBehaviour 
{
    
    public float _shootDamage = 1F;

    private ParticleSystem _particleSystem;

    public GameObject _bulletPrefab;

    // Recovery manager
    public float _recoveryTime = 0.1F;
    private float _currentRecovery;

    public float _bulletTime = 1F;

    public float _shotVariance = 0.05F;
    public float _shotDistance = 1000F;

    private float _lightTimer;
    public float _lightTime = 0.2f; // How long the light is on

	// Use this for initialization
	void Start () 
    {
        _lightTimer = 0;
        light.enabled = false;
        _currentRecovery = 0;

        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _particleSystem.enableEmission = false;

        if (_bulletPrefab == null)
        {
            Debug.LogError("ERROR : Bullet prefab are not attached to Shotgun");
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        _currentRecovery -= Time.deltaTime;

        if (_lightTimer <= 0)
        {
            light.enabled = false;
        }
        else
            _lightTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && _currentRecovery <= 0)
        {
            Fire();

            _currentRecovery = _recoveryTime;
        }
	}

    void Fire()
    {
        // Reset and play the audiosource
        audio.Stop();
        audio.Play();

        // Initialise the particle System
        _particleSystem.enableEmission = false;
        _particleSystem.Emit(_particleSystem.maxParticles);

        if (light)
        {
            _lightTimer = _lightTime;
            light.enabled = true;
        }

        Vector3 _offset = transform.up * Random.Range(0.0F, _shotVariance);

        _offset = Quaternion.AngleAxis(Random.Range(0.0F, 360.0F), transform.forward) * _offset;

        GameObject _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity) as GameObject;

        _bullet.rigidbody.AddForce((transform.forward + _offset) * _shotDistance);

        Destroy(_bullet, _bulletTime);
    }
}
