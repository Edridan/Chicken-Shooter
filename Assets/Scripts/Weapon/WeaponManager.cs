using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon Management script
/// Manage the different Weapon of the player
/// </summary>

[RequireComponent(typeof(Animator))]
public class WeaponManager : MonoBehaviour 
{
    private GameObject _rocketLauncher = null;
    private GameObject _shotgun = null;
    private GameObject _tommyGun = null;

    private Shotgun _shotgunSpec;
    private TommyGun _tommyGunSpec;
    private RocketLauncher _rocketLauncherSpec;

    private Animator _animator;

    private enum EWeapon
    {
        RocketLauncher = 0,
        Shotgun = 1,
        TommyGun = 2,
    }

    private EWeapon _currentWeapon;

	// Use this for initialization
	void Start () 
    {
	    // Start the component :
       _rocketLauncher = GameObject.Find("Rocket Launcher");
       _shotgun = GameObject.Find("Shotgun");
       _tommyGun = GameObject.Find("Tommy Gun");

        // ERROR Management
        if (_rocketLauncher == null)
        {
            Debug.LogError("ERROR : Unable to find GameObject Named Rocket Launcher");
        }
        else
        {
            _rocketLauncherSpec = _rocketLauncher.GetComponent<RocketLauncher>();
        }

        if (_shotgun == null)
        {
            Debug.LogError("ERROR : Unable to find GameObject Named Shotgun");
        }
        else
        {
            _shotgunSpec = _shotgun.GetComponent<Shotgun>();
        }

        if (_tommyGun == null)
        {
            Debug.LogError("ERROR : Unable to find GameObject Named Tommy Gun");
        }
        else
        {
            _tommyGunSpec = _tommyGun.GetComponent<TommyGun>();
        }

        _animator = GetComponent<Animator>();

        _currentWeapon = EWeapon.Shotgun;

        ChangeWeapon();
	}

    void ManageWeapon()
    {
        float _mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (_currentWeapon == EWeapon.Shotgun && _shotgunSpec.IsReloading())
        {
            return;
        }

        if (_mouseScroll != 0)
        {
            _animator.SetTrigger("Change");

            if (_mouseScroll > 0F)
            {
                _currentWeapon++;

                if (_currentWeapon > EWeapon.TommyGun)
                    _currentWeapon = EWeapon.RocketLauncher;
            }
            else if (_mouseScroll < 0F)
            {
                _currentWeapon--;

                if (_currentWeapon < EWeapon.RocketLauncher)
                    _currentWeapon = EWeapon.TommyGun;
            }
         }
    }

    // Update the active weapon
    void ChangeWeapon()
    {
        switch (_currentWeapon)
        {
            case EWeapon.TommyGun:
                _rocketLauncher.SetActive(false);
                _shotgun.SetActive(false);
                _tommyGun.SetActive(true);
                break;
            case EWeapon.RocketLauncher: 
                _rocketLauncher.SetActive(true);
                _shotgun.SetActive(false);
                _tommyGun.SetActive(false);
                break;
            case EWeapon.Shotgun: 
                _rocketLauncher.SetActive(false);
                _shotgun.SetActive(true);
                _tommyGun.SetActive(false);
                break;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        ManageWeapon();
	}
}
