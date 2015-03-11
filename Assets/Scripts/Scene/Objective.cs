using UnityEngine;
using System.Collections;



public class Objective : MonoBehaviour {

    public bool _EndScene;

	// Use this for initialization
	void Start () 
    {
        _EndScene = false;
	}

    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.GetComponent<PlayerScript>())
        {
            _EndScene = true;
        }
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
