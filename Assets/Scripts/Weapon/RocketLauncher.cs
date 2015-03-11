using UnityEngine;
using System.Collections;

/// <summary>
/// GunScriptScript prefab for all Gun
/// </summary>
/// 
[RequireComponent(typeof(AudioSource))]
public class RocketLauncher : MonoBehaviour
{
    public float _shootDamage = 1F;

    // Recovery manager
    public float _recoveryTime = 0.5F;
    private float _currentRecovery;

    public GameObject _rocketPrefab;

    // Use this for initialization
    void Start()
    {
        _currentRecovery = 0;

        if (_rocketPrefab == null)
        {
            Debug.LogError("ERROR : No Rocket Prefab attached to the RocketLauncher");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentRecovery -= Time.deltaTime;

        if (Input.GetMouseButton(0) && _currentRecovery <= 0)
        {
            Fire();

            _currentRecovery = _recoveryTime;
        }
    }

    public void Fire()
    {
        audio.Play();

        GameObject _rocket = (GameObject)Instantiate(_rocketPrefab, transform.position, transform.rotation);

        float _force = _rocket.GetComponent<Rocket>()._speed;
        _rocket.rigidbody.AddForce(_rocket.transform.forward * _force);
    }
}

