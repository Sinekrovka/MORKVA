
using UnityEngine;

public class PatrolAgent : MonoBehaviour
{
    private Transform levelPalete;
    private void Awake()
    {
        levelPalete = LevelGenerator.Instance.GetLevelContainer;
    }
}
