using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class LoseButtonsController : GameSystem, IIniting
{
    public void OnInit()
    {
        //Time.timeScale = 0;
    }
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
