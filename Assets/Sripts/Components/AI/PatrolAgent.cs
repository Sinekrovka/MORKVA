
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    private Transform levelPalete;
    private NavMeshAgent _navMeshAgent;
    private bool stop;
    private void Awake()
    {
        levelPalete = LevelGenerator.Instance.GetLevelContainer.GetChild(0);
        _navMeshAgent = GetComponent<NavMeshAgent>();
       SetRandomPoint();
    }

    private void Update()
    {
        if (_navMeshAgent.stoppingDistance >= Vector3.Distance(_navMeshAgent.destination, transform.position))
        {
            SetRandomPoint();
        }
    }

    private void SetRandomPoint()
    {
        Transform randString = levelPalete.GetChild(Random.Range(0, levelPalete.childCount));
        _navMeshAgent.SetDestination(randString.GetChild(Random.Range(0, randString.childCount)).position);
    }
    
    
}
