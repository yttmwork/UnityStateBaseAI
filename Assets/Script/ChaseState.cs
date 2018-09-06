using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IStateBase
{
    private static ChaseState instance = new ChaseState();

    public static ChaseState Instance
    {
        get
        {
            return instance;
        }
    }

    public void Init(Monster monster)
    {
        monster.IsAttackStart = false;
    }

    public void Update(Monster monster)
    {
        // ターゲットの方を向く
        monster.TurnToObject();
        // 移動
        monster.Move(monster.ChaseSpeed);

        // 攻撃範囲なら攻撃に切り替え
        if (monster.IsAttackStart == true)
        {
            Debug.Log("攻撃に切り替え");
            monster.ChangeState(AttackState.Instance);
        }
        // 追跡範囲外なら待機に切り替え
        else if (monster.IsWithinRange(monster.ChaseRange) == false)
        {
            Debug.Log("待機に切り替え");
            monster.ChangeState(WaitState.Instance);
        }
    }
}
