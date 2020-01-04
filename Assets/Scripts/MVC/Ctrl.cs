using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ctrl : SingleIton<Ctrl>
{
    public View view { get; private set; }
    public Model model { get; private set; }
    CameraCtrl cameraCtrl;
    public FSMSystem fSMSystem { get; private set; }
    public TermsManager termsManager { get; private set; }
    public override void Awake()
    {
        base.Awake();
        view = GetComponent<View>();
        model = GetComponent<Model>();
        cameraCtrl = GetComponent<CameraCtrl>();
        termsManager = GetComponent<TermsManager>();
    }
    private void Start()
    {
        MakeFSmSystem();
    }

    void MakeFSmSystem()
    {
        fSMSystem = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        for (int i = 0; i < states.Length; i++)
        {
            fSMSystem.AddState(states[i]);
            if (states[i] is MainMenuState)
                fSMSystem.SetDefaultState(states[i]);
        }      
    }

    public void ZoomIn()
    {
        cameraCtrl.ZoomIn();
    }

    public void ZoomOut()
    {
        cameraCtrl.ZoomOut();
    }

    public void AddScore(int score)
    {
        model.score += score;
        if (model.score > model.topScore)
            model.topScore = model.score;
        view.UpdateUI();
    }

    public void Retry()
    {
        model.NextGame();
        model.SaveData();
        view.UpdateUI();
        termsManager.Clear();
        fSMSystem.PerformTransition(Transition.StartBtnClick);
    }
}
