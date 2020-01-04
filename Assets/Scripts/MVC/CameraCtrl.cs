using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCtrl : MonoBehaviour
{
    Camera mainCamera;
    public const float gameOrthSize = 14f;
    public const float mainMenuOrthSize = 16f;

    private void Awake()//必须在Awakez中获得相机，要不会报空指针
    {
        mainCamera = Camera.main;
    }

    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(gameOrthSize, 0.5f);
    }

    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(mainMenuOrthSize, 0.5f);
    }
}
