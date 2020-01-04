using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MainMenuState : FSMState
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject muteIcon;

    [SerializeField]GameObject rankPanel;
    private void Awake()
    {
        stateID = StateID.MainMenu;
        AddTransition(Transition.StartBtnClick, StateID.Game);
    }

    public override void DoBeforeEntering()
    {
        Ctrl._Ins.view.ShowMainMenuPanel();
        Ctrl._Ins.ZoomOut();
        Ctrl._Ins.termsManager.StopGame();
    }

    public override void DoBeforeLeaving()
    {
        Ctrl._Ins.view.HideMainMenuPanel();
    }

    public void StartGameClick()
    {
        Ctrl._Ins.fSMSystem.PerformTransition(Transition.StartBtnClick);
        AudioManager._Ins.BtnClickAs();
    }

    public void SettingClick()
    {
        settingPanel.SetActive(true);
        AudioManager._Ins.BtnClickAs();
    }

    public void MuteClick()
    {  
        bool ismute= AudioManager._Ins.MuteSwitch();
        muteIcon.SetActive(ismute);
        if(ismute==false)
            AudioManager._Ins.BtnClickAs();
    }

    public void RankBtnClick()
    {
        rankPanel.SetActive(true);
    }

    public void RetryClick()
    {
        Ctrl._Ins.Retry();
    }
}
