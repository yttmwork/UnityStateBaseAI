using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IStateBase
{
    private static WalkState instance = new WalkState();
    public static WalkState Instance
    {
        get
        {
            return instance;
        }
    }

    public void Init(Monster monster)
    {
        monster.Timer = 0.0f;
        // 移動方向の決定
        float degree = Random.Range(0.0f, 360.0f);
        monster.MoveDirection = new Vector3(0.0f, degree, 0.0f);
        monster.transform.Rotate(monster.MoveDirection);
    }

    public void Update(Monster monster)
    {
        monster.Timer += Time.deltaTime;

        monster.Move(monster.WalkSpeed);

        // 一定時間移動したら待機に切り替え
        if (monster.IsWalkEnd())
        {
            Debug.Log("待機に切り替え");
            monster.ChangeState(WaitState.Instance);
        }
        // 追跡範囲内なら追跡に切り替え
        else if (monster.IsWithinRange(monster.ChaseRange) == true)
        {
            Debug.Log("追跡に切り替え");
            monster.ChangeState(ChaseState.Instance);
        }
    }
}
