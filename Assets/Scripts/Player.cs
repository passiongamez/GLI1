using System.Linq.Expressions;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] float _canFire = -1f;
    [SerializeField] float _fireRate = 1f;

    [SerializeField] Transform _rayPosition;

    AIBehavior _enemyScript;
    UIManager _uiManager;
    Sniper _sniper;
    Barrel _barrel;

    Camera _mainCam;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _barrierSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        _mainCam = GetComponentInChildren<Camera>();
        if( _mainCam == null)
        {
            Debug.Log("camera is null");
        }

        _uiManager = FindFirstObjectByType<UIManager>();
        if( _uiManager == null)
        {
            Debug.Log("ui manager is null");
        }

        _sniper = GetComponentInChildren<Sniper>();
        if( _sniper == null)
        {
            Debug.Log("Sniper is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _canFire < Time.time && _uiManager._ammo > 0)
        {
            _uiManager.SubtractAmmo(1);
            _sniper.PlayGunShot();
            Fire();
        }

        if (Input.GetMouseButton(1))
        {
            _mainCam.fieldOfView = 20;
        }
        else
        {
            _mainCam.fieldOfView = 60;
        }
    }

    // Function to handle the firing logic
    void Fire()
    {
        // Set the next possible fire time based on current time and fire rate
        _canFire = Time.time + _fireRate;

        // Create a ray from the center of the screen to simulate the firing direction
        Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Variable to store information about what the ray hit
        RaycastHit _hitInfo;

        // Perform the raycast to check if something was hit
        if (Physics.Raycast(rayOrigin, out _hitInfo, Mathf.Infinity))
        {
            // Draw a magenta ray in the scene for debugging, showing the firing direction
            Debug.DrawRay(rayOrigin.origin, transform.TransformDirection(Vector3.forward) * 5, Color.magenta, 5);

            // Try to get the AIBehavior script component from the object hit by the ray
            _enemyScript = _hitInfo.transform.GetComponent<AIBehavior>();

            // If the object has an AIBehavior script (likely an enemy), call the Death function
            if (_enemyScript != null)
            {
                // Log the name of the collider that was hit (useful for debugging)
                Debug.Log(_hitInfo.collider.name);

                // Call the Death function on the enemy, which could trigger enemy death behavior
                _enemyScript.Death();
            }
            if(_hitInfo.collider.tag == "Barrier")
            {
                _audioSource.PlayOneShot(_barrierSound);
            }

            if(_hitInfo.collider.tag == "Barrel")
            {
               _barrel = _hitInfo.collider.GetComponent<Barrel>();
                if (_barrel != null)
                {
                    _barrel.StartExplosion();
                    _barrel.ExplosionOccurence();
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay((_rayPosition.position), _rayPosition.forward * 5);
    }
}
