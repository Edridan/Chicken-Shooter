using UnityEngine;
using System.Collections;

/// <summary>
/// Assign to all living game object
/// </summary>
public class HealthScript : MonoBehaviour 
{
    public int _maxLifePoint = 1;
    private int _lifePoint;    // Current lifepoint of the character

    public bool _isPlayer;

    public GameObject _bloodParticle;   // Blood Particle prefab
    public AudioSource _audioSource;

	// Use this for initialization
    void Start()
    {
        _lifePoint = _maxLifePoint;

        if (_bloodParticle == null && !_isPlayer)
        {
            Debug.LogError("ERROR : Blood Particle prefab doesn't attach");
            Destroy(gameObject);
        }

        if (_isPlayer)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public int GetLifePoint()
    {
        return _lifePoint;
    }

     // Object Suppression and particules add
    public void Die()
    {
        if (!_isPlayer)
        {
            if (_bloodParticle != null)
            {
                // Instanciate the particles
                GameObject _particle = GameObject.Instantiate(_bloodParticle, transform.position, _bloodParticle.transform.rotation) as GameObject;

                if (_particle.particleSystem)
                {
                    Destroy(_particle, _particle.particleSystem.startLifetime);
                }
            }
          
            Destroy(gameObject);
        }
    }

    private bool AlreadyDead()
    {
        return (_lifePoint <= 0);
    }

    public void TakeDamage(int damages)
    {
        if (!AlreadyDead())
        {
            _lifePoint -= damages;

            if (_lifePoint <= 0)
            {
                Die();
            }

            if (_isPlayer)
            {
                _audioSource.Play();    // Play the damage sound
            }
        }
    }
}
