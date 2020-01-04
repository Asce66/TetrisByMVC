using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] RectTransform gameMenu;

    [SerializeField] RectTransform titleRect, mainMenuRect;
    [SerializeField] GameObject retry;

    [SerializeField] Text socreTxt, topScoreTxt, playTimesTxt;
    [SerializeField] Text rankSocreTxt, rankTopScoreTxt;
    [SerializeField]Text finalScoreTxt;

    [SerializeField] GameObject endGamePanle;
    private void Start()
    {
        UpdateUI();
    }

    public void ShowGamePanel()
    {
        gameMenu.gameObject.SetActive(true);
        gameMenu.DOAnchorPosY(-220.6f, 0.5f);
    }

    public void HideGamePanel()
    {
        gameMenu.DOAnchorPosY(220.6f, 0.5f).OnComplete(() => gameMenu.gameObject.SetActive(false));
    }

    public void ShowMainMenuPanel()
    {
        titleRect.gameObject.SetActive(true);
        titleRect.DOAnchorPosY(-270f, 0.5f);

        mainMenuRect.gameObject.SetActive(true);
        mainMenuRect.DOAnchorPosY(142, 0.5f);
    }

    public void HideMainMenuPanel()
    {
        titleRect.DOAnchorPosY(270f, 0.5f).OnComplete(() => 
        titleRect.gameObject.SetActive(false));

        mainMenuRect.DOAnchorPosY(-142, .5f).OnComplete(() => 
        mainMenuRect.gameObject.SetActive(false));
    }

    public void ShowRetryBtn()
    {
        if (retry.activeSelf == false)
            retry.SetActive(true);
    }

    public void UpdateUI()
    {
        socreTxt.text = Ctrl._Ins.model.score.ToString();
        topScoreTxt.text = Ctrl._Ins.model.topScore.ToString();

        playTimesTxt.text = Ctrl._Ins.model.playTimes.ToString();
        rankSocreTxt.text = Ctrl._Ins.model.score.ToString();
        rankTopScoreTxt.text = Ctrl._Ins.model.topScore.ToString();

        finalScoreTxt.text = Ctrl._Ins.model.score.ToString();
    }

    public void ShowEndGamePanel()
    {
        endGamePanle.SetActive(true);
    }

    public void HideEndGamePanel()
    {
        endGamePanle.SetActive(false);
    }
}
