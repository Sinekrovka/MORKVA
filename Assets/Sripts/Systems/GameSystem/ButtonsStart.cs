using Kuhpik;
using UnityEngine;

public class ButtonsStart : GameSystem
{
    public void StartGame()
    {
        LevelGenerator.Instance.GenerateLevel();
        Bootstrap.ChangeGameState(EGamestate.Game);
    }
}
