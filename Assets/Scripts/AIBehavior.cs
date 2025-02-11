using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    enum AIStates
    {
        Run,
        Hide,
        Death
    }

    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] Transform _spawnPos;

    [SerializeField]
    Transform[] _path;
    [SerializeField] int _currentPath = 0;

    NavMeshAgent _agent;

    Transform _currentColumn;

    AIStates _currentState;
    [SerializeField] int _trueOrFalse;

    [SerializeField] bool _isRunning = true;
    [SerializeField] bool _isHiding = false;
    [SerializeField] bool _isDead = false;
    bool _reachedPath = false;

    WaitForSeconds _hideTime = new WaitForSeconds(3);

    Column _column;
    GameManager _gameManager;
    UIManager _uiManager;

    Collider _collider;
    Animator _animator;

   [SerializeField] AudioSource _audio;


    private void OnEnable()
    {
        _hideTime = new WaitForSeconds(Random.Range(3, 5));

        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.Log("Collider is null");
        }

        _gameManager = FindFirstObjectByType<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Game manager null");
        }

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.Log("Agent is null");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator is null");
        }

        _uiManager = FindFirstObjectByType<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("ui manager is null");

        }
        gameObject.transform.position = _spawnPos.position;
        _currentPath = 0;
        _agent.enabled = false;
        _isRunning = true;
        _currentState = AIStates.Run;
        AddEnemyCount();
    }


    void Start()
    {

    }

    void Update()
    {
        switch(_currentState)
        {
            case AIStates.Run:
                if(_isRunning == true)
                {
                    if (_currentPath <= _path.Length - 1)
                    {
                        Debug.Log("Running");
                        SetPath();
                        if(_agent.speed < 3f)
                        {
                            _animator.SetFloat("Speed", .1f);
                            _animator.SetBool("Hiding", false);
                        }
                        if(_agent.speed >= 3f)
                        {
                            _animator.SetFloat("Speed", 3f);
                            _animator.SetBool("Hiding", false);
                        }
                    }
                    _isHiding = false;
                    _isDead = false;
                }
                break;
            case AIStates.Hide:
                if(_isHiding == true)
                {
                    Debug.Log("Calling hide method");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _animator.SetBool("Hiding", true);
                }
                _isRunning = false;
                _isDead = false;
                break;
            case AIStates.Death:
                if(_isDead == true)
                {
                    Debug.Log("Enemy has died");
                    _animator.SetTrigger("Death");
                    _animator.SetBool("Hiding", false);
                    _animator.SetFloat("Speed", 0f);
                }
                _isRunning = false;
                _isHiding = false;
                break;
        }
    }

    void SetPath()
    {
        _currentState = AIStates.Run;
        _agent.enabled = true;
        _agent.speed = UnityEngine.Random.Range(4f, 8f);
        _agent.destination = _path[_currentPath].position;
        if (_agent.remainingDistance <= .5f && _reachedPath == true)
        {
            _currentPath += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "End")
        {
            _gameManager.TrackFinish();
            _agent.enabled = false;
            _uiManager.EnemiesRemainingMinus(1);
            gameObject.SetActive(false);
        }

        if(other.tag == "Waypoint")
        {
            _reachedPath = true;
        }

        if(other.tag == "Barrier")
        {
            _column = other.GetComponent<Column>();
            if(_column == null)
            {
                return;
            }
                _currentColumn = other.gameObject.transform;
                HideDecision();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Waypoint")
        {
            _reachedPath = false;
        }
    }

    void HideDecision()
    {
            _trueOrFalse = UnityEngine.Random.Range(0, 2);
            if (_trueOrFalse == 0)
            {
            if (_column._isOccupied == false)
            {
                _column.IsOccupied();
                _currentState = AIStates.Hide;
                _isHiding = true;
                _agent.enabled = false;
                StartCoroutine(WaitAtColumn());
            }

            }
            else if (_trueOrFalse == 1)
            {
                return;
            }
    }

    IEnumerator WaitAtColumn()
    {
        _agent.speed = 0;
        Vector3.MoveTowards(transform.position, _currentColumn.position + new Vector3(1f, 0, -1f), 2f);
        yield return _hideTime;
        _agent.speed = 3.5f;
        _isHiding = false;
        _agent.enabled = true;
        _currentState = AIStates.Run;
        _isRunning = true;
        _column.Vacant();
    }

    public void Death()
    {
        _uiManager.AddScore(50);
        _agent.enabled = false;
        _collider.enabled = false;
        _currentState = AIStates.Death;
        _isDead = true;
        _isRunning = false;
        _isHiding = false;
        StartCoroutine(DeathWait());
    }

    IEnumerator DeathWait()
    {
        _audio.Play();
        yield return new WaitForSeconds(3f);
        _uiManager.EnemiesRemainingMinus(1);
        _audio.enabled = false;
        gameObject.SetActive(false);
    }

    void AddEnemyCount()
    {
        _uiManager.EnemiesRemainingAdd(1);
    }

    public void ExplosionDeath()
    {
        _uiManager.AddScore(50);
        _uiManager.EnemiesRemainingMinus(1);
        gameObject.SetActive(false);
    }
}
