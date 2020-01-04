using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsManager : MonoBehaviour
{
    [SerializeField] Minors[] minors;
    [SerializeField] Transform minorParent;
    const int x = 10;
    const int y = 20;
    //考虑到方块一开始是在地图外面生成的,如果此时进行判断就会越界 所以y+4
    Transform[,] minorTrans = new Transform[x, y + 4];
    bool isStopGame = true;
    Minors nowMinors;

    private void Start()
    {
        nowMinors = Instantiate(minors[Random.Range(0, minors.Length)]);
        nowMinors.termsManager = this;
        nowMinors.transform.SetParent(minorParent);
        StopGame();
    }

    public void StopGame()
    {
        isStopGame = true;
        if (nowMinors != null)
        {
            nowMinors.isStop = true;
        }
    }

    public void Resume()
    {
        isStopGame = false;
        if (nowMinors != null)
        {
            nowMinors.isStop = false;
        }
    }

    private bool IsInMap(int x, int y)
    {
        return x >= 0 && y >= 0 && x <= 9;
    }

    public bool IsValidPos(int x, int y)
    {
        if (IsInMap(x, y) == false)
            return false;
        if (minorTrans[x, y] != null)
            return false;
        return true;
    }

    //当一个方块落地后，把该方块的子物体的所有trnasform信息记录进入数组
    //用于之后的方块下落判定 并进行一系列逻辑判断
    public void NextMinor(Transform trans)
    {
        nowMinors = null;
        //直接遍历trnas(方块)的所有子物体的trnasform组件
        foreach (Transform item in trans)
        {//方块的子物体中有一个空的子物体作为轴心点用于旋转
         //所以需要排除这个空物体的干扰
            if (item.CompareTag("Block"))
            {
                int x = Mathf.RoundToInt(item.position.x);
                int y = Mathf.RoundToInt(item.position.y);
                minorTrans[x, y] = item;
            }
        }
        CheckFull();
        CheckIsGameOver();
        if (isStopGame == false)
        {
            nowMinors = Instantiate(minors[Random.Range(0, minors.Length)]);
            nowMinors.termsManager = this;
            nowMinors.transform.SetParent(minorParent);
        }
    }

    //判断当前行是不是满了
    bool IsFullRow(int y)
    {
        for (int i = 0; i < x; i++)
        {
            if (minorTrans[i, y] == null)
                return false;
        }
        return true;
    }

    //判断有没有满的行
    private void CheckFull()
    {
        int count = 0;
        for (int i = 0; i < y; ++i)
        {
            if (IsFullRow(i))
            {
                ClearRow(i);
                MoveMinors(i + 1);
                --i;//这一行消除后上一行落下来填补,可能这一行因此又满了,所以停在这一行再判断
                ++count;
            }
        }
        if (count > 0)
            Ctrl._Ins.AddScore(count * 200);
    }

    private void ClearRow(int y)
    {
        AudioManager._Ins.ClearAs();
        for (int i = 0; i < x; i++)
        {
            //当方块的所有小方块都删除完后就删除方块自身
            if (minorTrans[i, y].GetComponentInParent<Minors>().DestroyChild() <= 0)
            {
                Destroy(minorTrans[i, y].parent.gameObject);
            }
            else
                Destroy(minorTrans[i, y].gameObject);
            minorTrans[i, y] = null;
        }
    }

    //当有一行的方块满了被消除后，其上的方块都下落
    private void MoveMinors(int row)
    {
        for (int i = row; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (minorTrans[j, i] != null)
                {
                    minorTrans[j, i].position += Vector3.down;
                    minorTrans[j, i - 1] = minorTrans[j, i];
                    minorTrans[j, i] = null;
                }
            }
        }
    }

    private void CheckIsGameOver()
    {
        for (int i = 0; i < x; ++i)
        {
            if (minorTrans[i, y] != null)
            {
                isStopGame = true;
                Ctrl._Ins.fSMSystem.PerformTransition(Transition.PauseBtnClick);
                Ctrl._Ins.view.ShowEndGamePanel();
                break;
            }
        }
    }

    //清除所有的方块以及方块transform信息
    public void Clear()
    {
        for (int i = 0; i < x; ++i)
        {
            for (int j = 0; j < y + 4; ++j)
            {
                minorTrans[i, j] = null;
            }
        }
        for (int i = 0; i < minorParent.childCount; ++i)
        {
            Destroy(minorParent.GetChild(i).gameObject);
        }
        if (nowMinors != null)
        {
            Destroy(nowMinors.gameObject);
            nowMinors = null;
        }
    }

}
