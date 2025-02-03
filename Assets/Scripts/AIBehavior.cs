using System;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField]
    Transform[] _path;
    [SerializeField] int _currentPath = 0;
    NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.Log("Agent is null");
        }
    }

    void Update()
    {
        if (_currentPath <= _path.Length - 1)
        {
            SetPath();
        }
    }

    void SetPath()
    {
        _agent.destination = _path[_currentPath].position;
        if (_agent.remainingDistance < .5f)
        {
            _currentPath += 1;
        }
    }
}
