using UnityEngine;
using System.Collections;

/// <summary>
/// Manage the bullet collision with the world (not with the other characters)
/// </summary>
public class BulletCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider _collider)
    {
        Destroy(transform.parent.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
