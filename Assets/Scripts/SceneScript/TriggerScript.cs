using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TriggerScript : MonoBehaviour 
{
    struct SEvent
    {
        public GameObject _gameObject;
        public float _timeToStart;
        public bool _isPlayed;
    }

    public int _triggerLayer = 10;

    private SEvent [] _events;  // List of events

    // Event manager
    private bool _eventPlayed;
    private float _timeFromBegining;

    // Time for each events
    public float [] _timeEvent;

	// Use this for initialization
	void Start () 
    {
        gameObject.layer = _triggerLayer;

        collider.isTrigger = true;  // Set the collider to trigger!

        _events = new SEvent[transform.childCount];

        _eventPlayed = false;
        _timeFromBegining = 0F;
        int _itr = 0;

        // Disable all the child components
	    foreach (Transform _child in transform)
        {
            _child.gameObject.SetActive(false);

            _events[_itr]._gameObject = _child.gameObject;

            if (_itr <= _timeEvent.Length - 1)
                _events[_itr]._timeToStart = _timeEvent[_itr];
            else
                _events[_itr]._timeToStart = 0F;

            _events[_itr]._isPlayed = false;

            _itr++;
        }
	}
	
    void ManageEvent()
    {
        int _itr = 0;
        int _eventPlayed = 0;   // Manage the destroy of the trigger or not

        foreach (SEvent _event in _events)
        {
            if (!_event._isPlayed)
            {
                if (_event._timeToStart <= _timeFromBegining)
                {
                    _event._gameObject.SetActive(true);

                    // Set the boolean
                    _events[_itr]._isPlayed = true;
                }
            }
            else
            {
                if (_event._gameObject.transform.childCount == 0)
                    _eventPlayed++;
            }

            _itr++;
        }

        // Destroy the object?
        if (_eventPlayed == _events.Length)
        {
            Destroy(gameObject);    // Destroy the event
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        // If the player enter to the trigger
        if (_collider.GetComponent<PlayerScript>() != null)
        {
            _eventPlayed = true;

            collider.enabled = false;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (_eventPlayed == true)
        {
            _timeFromBegining += Time.deltaTime;

            ManageEvent();
        }
	}
}
