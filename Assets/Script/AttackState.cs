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
        monster.ChangeAnimation((int)Monster.State.ATTACK);
    }

    public void Update(Monster monster)
    {
        // アニメーション終了で切り替え
        if (monster.IsAnimationEnd() == true)
        {
            Debug.Log("攻撃待機に切り替え");
            monster.ChangeState(AttackWaitState.Instance);
        }
    }

    private AttackState()
    {
    }

}
