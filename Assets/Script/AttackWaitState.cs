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
            // 攻撃範囲内なら攻撃
            if (monster.IsWithinRange(monster.AttackRange) == true)
            {
                Debug.Log("攻撃に切り替え");
            }
            // 攻撃範囲外は待機
            else
            {
                Debug.Log("待機に切り替え");
            }
        }
    }
}
