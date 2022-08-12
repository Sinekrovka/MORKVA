using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Bootstrap.ChangeGameState(EGamestate.Win);
        }
    }
}
