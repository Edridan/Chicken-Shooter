using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class DoorScript : MonoBehaviour {

    private Animator _animator;
    private AudioSource _audioSource;
    
    private bool _isOpen;   // If true : the gate is open


	// Use this for initialization
	void Start () 
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        _isOpen = false;
	}
	
    void OnTriggerEnter(Collider _collider)
    {
        if (!_isOpen && _collider.GetComponent<PlayerScript>())
        {
            _animator.SetTrigger("Open");
            _audioSource.Play();

            _isOpen = true;
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        if (_isOpen && _collider.GetComponent<PlayerScript>())
        {
            _animator.SetTrigger("Close");
            _audioSource.Play();

            _isOpen = false;
        }
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
