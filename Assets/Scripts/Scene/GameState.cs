using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour
{
    enum State
    {
        Begin,
        GamePlay,
        Ending,
    }

    private State _currentState;

    public int _chickenLayer;
    public int _bulletDamageLayer;
    public int _bulletColliderLayer;
    public int _terrainLayer;
    public int _triggerLayer;
    public int _playerLayer;

    private Image _BlackTexture;
    private GameObject _BeginText;
    private GameObject _EndText;

    public float _BeginTextDisplay = 1F;

    public float _TimeToFade = 2F;
    private float _timer;   // Timer of the gamestate

    public int _sceneNumber = 0;

    public bool _lockCursor = true;

    #region Singleton
    private static GameState s_instance;

    public static GameState GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = new GameState();
        }
        return s_instance;
    }
    #endregion

    // Use this for initialization
	void Start () 
    {
        if (s_instance == null)
            s_instance = this;
        else
        {
            Debug.LogError("ERROR : Multiple Game state instanciation!");
        }

        if (_sceneNumber == 0)
        {
            Debug.LogWarning("Scene number is initialized to 0");
        }

        // Ignore collision
        // between bullet and trigger
        Physics.IgnoreLayerCollision(_bulletDamageLayer, _triggerLayer, true);
        Physics.IgnoreLayerCollision(_bulletColliderLayer, _triggerLayer, true);

        // between bullet themselves
        Physics.IgnoreLayerCollision(_bulletDamageLayer, _bulletDamageLayer, true);
        Physics.IgnoreLayerCollision(_bulletColliderLayer, _bulletColliderLayer, true);
        Physics.IgnoreLayerCollision(_bulletDamageLayer, _bulletColliderLayer, true);

        // between bullet and land
        Physics.IgnoreLayerCollision(_bulletDamageLayer, _terrainLayer, true);
        Physics.IgnoreLayerCollision(_bulletDamageLayer, 0, true);
        Physics.IgnoreLayerCollision(_bulletDamageLayer, _playerLayer, true);
        Physics.IgnoreLayerCollision(_bulletColliderLayer, _chickenLayer, true);

        // between chicken themself
        Physics.IgnoreLayerCollision(_chickenLayer, _chickenLayer, true);

        // GameState ignition
        _currentState = State.Begin;
        _timer = _TimeToFade;
        Screen.lockCursor = _lockCursor;

        // Get the UI Screen and Text
        GameObject _BlackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        _BlackTexture = _BlackScreen.GetComponent<Image>();

        _BeginText = GameObject.FindGameObjectWithTag("BeginText");
        _EndText = GameObject.FindGameObjectWithTag("EndText");

        _EndText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch (_currentState)
        {
            case State.Begin:
                if (_BeginTextDisplay <= 0)
                {
                    _timer -= Time.deltaTime;

                    if (_timer <= 0F)
                    {
                        _BeginText.SetActive(false);
                        StartCoroutine(FadeToWhite());
                        _currentState = State.GamePlay;
                        _timer = _TimeToFade + _BeginTextDisplay;
                    }
                }
                else
                    _BeginTextDisplay -= Time.deltaTime;
                break;
            case State.Ending:
                _timer -= Time.deltaTime;

                if (_timer <= _BeginTextDisplay)
                {
                    _EndText.SetActive(true);

                    if (_timer <= 0)
                        Application.LoadLevel(0);
                }

                break;

            default:
                break;
        }

	    // Lock the cursor
        Screen.lockCursor = _lockCursor;
	}

    public void EndScene()
    {
    }

    // Exit the application
    public void Exit(int code)
    {
        Debug.Log("Exit the application with the stop code : " + code);
        // Clear the application
        Application.Quit();
    }

    public void OnTriggerEnter(Collider _Collider)
    {
        // End the scene
        StartCoroutine(FadeToBlack());

        _currentState = State.Ending;   // Set the state to ending
    }

    

    #region ScreenEffect

    IEnumerator FadeToWhite()
    {
        float _Time = 0;
        float _Alpha = 0;

        while (_Time <= _TimeToFade)
        {
            _Alpha = Mathf.Lerp(255, 0, _Time / _TimeToFade);

            // Apply the alpha on the texture
            _BlackTexture.color = new Color(0, 0, 0, _Alpha / 255F);
            _Time += Time.deltaTime;
            
            yield return null;
        }

        _BlackTexture.color = new Color(0, 0, 0, 0);

        yield return null;
    }

    IEnumerator FadeToBlack()
    {
        float _Time = 0;
        float _Alpha = 0;

        while (_Time <= _TimeToFade)
        {
            _Alpha = Mathf.Lerp(0, 255, _Time / _TimeToFade);

            // Apply the alpha on the texture
            _BlackTexture.color = new Color(0, 0, 0, _Alpha / 255F);
            _Time += Time.deltaTime;

            yield return null;
        }

        _BlackTexture.color = new Color(0, 0, 0, 255);

        yield return null;
    }

    #endregion
}
