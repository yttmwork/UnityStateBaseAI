using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IStateBase
{
    private static WaitState instance = new WaitState();
    public static WaitState Instance
    {
        get
        {
            return instance;
        }
    }

    public void Init(Monster monster)
    {
        monster.Timer = 0.0f;
    }

    public void Update(Monster monster)
    {
        monster.Timer += Time.deltaTime;

        // 時間経過なら移動に切り替え
        if (monster.IsWaitEnd())
        {
            Debug.Log("移動に切り替え");
        }
        // 追跡範囲内なら追跡に切り替え
        else if (monster.IsWithinRange(monster.ChaseRange) == true)
        {
            Debug.Log("追跡に切り替え");
        }
    }
}
