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

    public void Init(Monster monster)
    {
        monster.ChangeAnimation((int)Monster.State.DAMAGE);
    }

    public void Update(Monster monster)
    {
        if (monster.IsAnimationEnd() == true)
        {
            Debug.Log("待機に切り替え");
            monster.ChangeState(WaitState.Instance);
        }
    }

    private DamageState()
    {
    }

}
