using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(HealthScript))]
public class PlayerScript : MonoBehaviour {

    private HealthScript _health;

    // UI information
    public Color _normalState;
    public Color _midState;
    public Color _woundState;

    private Text _text;


	// Use this for initialization
	void Start () 
    {
        _health = GetComponent<HealthScript>();

        if (GameObject.FindWithTag("HealthPointGUI") == null)
        {
            Debug.LogError("ERROR : Doesn't found LifePointGUI tag (Player Script)");
        }
        else
        {
            GameObject _guiObject = GameObject.FindWithTag("HealthPointGUI");
            _text = _guiObject.GetComponent<Text>();
        }

        _health._isPlayer = true;
	}

    private Color GetColorState()
    {
        int _currentHealth = _health.GetLifePoint();

        if (_currentHealth >= 6)
            return _normalState;
        else if (_currentHealth >= 4)
            return _midState;
        else
            return _woundState;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (_text != null)
        {
            _text.color = GetColorState();

            _text.text = (10 * _health.GetLifePoint()).ToString();
        }
	}
}
