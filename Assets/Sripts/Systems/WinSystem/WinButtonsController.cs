using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class WinButtonsController : GameSystem
{
    public void NextLevel()
    {
        LevelGenerator.Instance.Rebuild(true);
        Bootstrap.ChangeGameState(EGamestate.Game);
    }

    public void RepeatLevel()
    {
        LevelGenerator.Instance.Rebuild(false);
        Bootstrap.ChangeGameState(EGamestate.Game);
    }

    public void ExitOnStartScreen()
    {
        LevelGenerator.Instance.ClearLevel();
        Bootstrap.ChangeGameState(EGamestate.Start);
    }
}
