using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Minors : MonoBehaviour
{
    float stepTime = 1f;
    float timer = 0;
    bool isEnd = false;
    [HideInInspector]
    public bool isStop = false;
    public TermsManager termsManager { private get; set; }
    Transform pivot;
    int blockCount = 4;//小方块计数器 当所有小方块都消除后就消除自身
    private void Update()
    {
        if (isEnd || isStop)
            return;
        KeyBordCtrl();
        timer += Time.deltaTime;
        if (timer >= stepTime)
        {
            timer = 0;
            MoveVertical();
        }
    }

    bool IsValiPos()
    {
        foreach (Transform item in transform)
        {
            //必须是position
            Vector2 pos = item.position.Round();
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (termsManager.IsValidPos(x, y) == false)
                return false;
        }
        return true;
    }
    public int DestroyChild()//消除一个小方块
    {
        return --blockCount;
    }

   private void MoveVertical()
    {
        transform.position += Vector3.down;
        if (IsValiPos() == false)
        {
            transform.position += Vector3.up;
            isEnd = true;
            termsManager.NextMinor(transform);
        }
    }

    private void MoveHorizontal(int step)
    {
        transform.position += Vector3.right * step;
        if (IsValiPos() == false)
            transform.position += Vector3.right * (-step);
    }

    //使方块围绕自定义的轴心点旋转
    private void Rotate()
    {
        if (pivot == null)
            pivot = transform.Find("Pivot");
        transform.RotateAround(pivot.position, transform.forward, 90);
        if (IsValiPos() == false)
            transform.RotateAround(pivot.position, transform.forward, -90);
    }

    private void KeyBordCtrl()
    {
        int step = 0;
        if (Input.GetKeyDown(KeyCode.A))
            step = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            step = 1;
        else if (Input.GetKeyDown(KeyCode.Space))
            Rotate();
        else if (Input.GetKeyDown(KeyCode.S))
            stepTime = 0.05f;
        if (step != 0)
            MoveHorizontal(step);
    }
}