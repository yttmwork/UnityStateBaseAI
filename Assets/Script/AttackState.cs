using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStateBase
{
    private static AttackState instance = new AttackState();
    public static AttackState Instance
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
        monster.ChangeAnimation((int)Monster.State.ATTACK);
    }

    public void Update(Monster monster)
    {
        // debug アニメーション実装までは時間で管理
        monster.Timer += Time.deltaTime;

        // アニメーション終了で切り替え
        // debug アニメーション実装までは時間で管理
        if (monster.Timer > 2.0f)
        {
            Debug.Log("攻撃待機に切り替え");
            monster.ChangeState(AttackWaitState.Instance);
        }
    }

    private AttackState()
    {
    }

}
