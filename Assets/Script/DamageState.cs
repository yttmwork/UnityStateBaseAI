using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : IStateBase
{
    private static DamageState instance = new DamageState();
    public static DamageState Instance
    {
        get
        {
            return instance;
        }
    }

    private DamageState()
    {
    }

    public void Init(Monster monster)
    {
        // debug アニメーション実装までは時間で管理
        monster.Timer = 0.0f;
    }

    public void Update(Monster monster)
    {
        // debug アニメーション実装までは時間で管理
        monster.Timer += Time.deltaTime;

        // debug アニメーション実装までは時間で管理
        if (monster.Timer > 2.0f)
        {
            Debug.Log("待機に切り替え");
            monster.ChangeState(WaitState.Instance);
        }
    }

    private DamageState()
    {
    }

}
