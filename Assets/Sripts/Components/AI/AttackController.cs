using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("!!!");
            Attack();
        }
    }

    private void Attack()
    {
        Bootstrap.ChangeGameState(EGamestate.Lose);
    }
}
