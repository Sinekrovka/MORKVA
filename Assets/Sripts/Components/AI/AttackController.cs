using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private PatrolAgent _patrolAgent;
    private void Awake()
    {
        _patrolAgent = GetComponentInParent<PatrolAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        /*Здесь можно было бы прописать анимацию стрельбы и атаку*/
        Bootstrap.ChangeGameState(EGamestate.Lose);
    }
}
