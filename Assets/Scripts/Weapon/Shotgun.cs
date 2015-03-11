using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Shotgun : MonoBehaviour 
{

    public float _shootDamage = 1F;

    private ParticleSystem _particleSystem;
    private Animator _animator;

    public GameObject _bulletPrefab;

    // Recovery manager
    public float _recoveryTime = 0.5F;
    private float _currentRecovery;

    public float _shotVariance = 1F;
    public float _shotDistance = 1000F;

    public int _bulletNumber = 10;    // How much bullet the shotgun fire
    public float _bulletTime = 1F;

    private float _lightTimer;
    public float _lightTime = 0.2f; // How long the light is on

    // Use this for initialization
    void Start()
    {
        _lightTimer = 0;
        light.enabled = false;
        _currentRecovery = 0;

        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponent<Animator>();

        _particleSystem.enableEmission = false;

        if (_bulletPrefab == null)
        {
            Debug.LogError("ERROR : Bullet prefab are not attached to Shotgun");
            Destroy(gameObject);
        }
    }

    public bool IsReloading()
    {
        return (_currentRecovery > 0F);
    }

    // Update is called once per frame
    void Update()
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

    public void Fire()
    {
        audio.Play();

        _animator.SetTrigger("Fire");
        
        // Initialise the particle System
        _particleSystem.enableEmission = false;
        _particleSystem.Emit(_particleSystem.maxParticles);

        if (light)
        {
            _lightTimer = _lightTime;
            light.enabled = true;
        }

        for (int i = 0; i < _bulletNumber; i++)
        {
            Vector3 _offset = transform.up * Random.Range(0.0F, _shotVariance);

            _offset = Quaternion.AngleAxis(Random.Range(0.0F, 360.0F), transform.forward) * _offset;

            GameObject _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity) as GameObject;

            _bullet.rigidbody.AddForce((transform.forward + _offset) * _shotDistance);

            Destroy(_bullet, _bulletTime);
        }
    }
}
