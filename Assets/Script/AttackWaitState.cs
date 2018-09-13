using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaitState : IStateBase
{
    private static AttackWaitState instance = new AttackWaitState();
    public static AttackWaitState Instance
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

        if (monster.IsAttackWaitEnd() == true)
        {
            // 追跡範囲内なら追跡
            if (monster.IsWithinRange(monster.ChaseRange) == true)
            {
                Debug.Log("追跡に切り替え");
                monster.ChangeState(ChaseState.Instance);
            }
            // 攻撃範囲外は待機
            else
            {
                Debug.Log("待機に切り替え");
                monster.ChangeState(WaitState.Instance);
            }
        }
    }

    private AttackWaitState()
    {
    }

}
