using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Game;
        AddTransition(Transition.PauseBtnClick, StateID.MainMenu);
        AddTransition(Transition.End, StateID.EndGame);
    }

    public override void DoBeforeEntering()
    {
        Ctrl._Ins.view.HideEndGamePanel();
        Ctrl._Ins.termsManager.Resume();
        Ctrl._Ins.view.ShowGamePanel();
        Ctrl._Ins.ZoomIn();
    }

    public override void DoBeforeLeaving()
    {
        Ctrl._Ins.view.HideGamePanel();
        Ctrl._Ins.view.ShowRetryBtn();
    }

    public void PauseClick()
    {
        Ctrl._Ins.fSMSystem.PerformTransition(Transition.PauseBtnClick);
        AudioManager._Ins.BtnClickAs();
    }
}
