using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    private Transform levelPalete;
    private NavMeshAgent _navMeshAgent;
    private bool maxNoise;
    private float runSpeed;
    private Vector3 _lastPosition;
    private void Awake()
    {
        levelPalete = LevelGenerator.Instance.GetLevelContainer.GetChild(0);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        maxNoise = false;
        runSpeed = _navMeshAgent.speed;
        NoiseController noise = FindObjectOfType<NoiseController>();
        noise._maxNoise += SetPlayerPosition;
       SetRandomPoint();
    }

    private void Update()
    {
        if (maxNoise)
        {
            maxNoise = false;
            SetLastPlayerPosition();
        }
        else
        {
            if (_navMeshAgent.stoppingDistance >= Vector3.Distance(_navMeshAgent.destination, transform.position))
            {
                SetRandomPoint();
            }
        }
    }

    private void SetRandomPoint()
    {
        Transform randString = levelPalete.GetChild(Random.Range(0, levelPalete.childCount));
        _navMeshAgent.SetDestination(randString.GetChild(Random.Range(0, randString.childCount)).position);
        _navMeshAgent.speed = runSpeed;
    }

    private void SetLastPlayerPosition()
    {
        _navMeshAgent.ResetPath();
        _navMeshAgent.speed = runSpeed * 2;
        _navMeshAgent.SetDestination(_lastPosition);
    }

    private void SetPlayerPosition(Vector3 lastPosition, bool noiseCount)
    {
        maxNoise = noiseCount;
        _lastPosition = lastPosition;
    }
}
