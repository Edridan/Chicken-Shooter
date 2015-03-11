using UnityEngine;
using System.Collections;

/// <summary>
/// Basic chicken script
/// Basic mother class for the 
/// </summary>
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(HealthScript))]
public class ChickenScript : MonoBehaviour
{
    // Direction
    protected GameObject _player;
    protected Vector3 _direction;

    protected HealthScript _life;

    // Character Controller
    protected CharacterMotor _characterMotor;

    // Use this for initialization
    public void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _characterMotor = GetComponent<CharacterMotor>();

        // Life script parameter
        _life = GetComponent<HealthScript>();
        _life._isPlayer = false;

        _direction = Vector3.zero;

        if (_player == null)
        {
            Debug.LogError("ERROR : Player not found");
            Destroy(gameObject);
        }
    }

    // Update the rotation of the chicken
    protected void ManageRotation()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.Normalize(_direction));
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
