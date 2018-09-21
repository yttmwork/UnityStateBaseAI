using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IStateBase
{
    private static DieState instance = new DieState();
    public static DieState Instance
    {
        get
        {
            return instance;
        }
    }

    public void Init(Monster monster)
    {
        // debug アニメーション実装までは時間で管理
        monster.Timer = 0.0f;
        monster.ChangeAnimation((int)Monster.State.DIE);
    }

    public void Update(Monster monster)
    {
        // debug アニメーション実装までは時間で管理
        monster.Timer += Time.deltaTime;

        // debug アニメーション実装までは時間で管理
        if (monster.Timer > 2.0f)
        {
            // 死亡
            monster.Die();
        }
    }

    private DieState()
    {
    }

}
