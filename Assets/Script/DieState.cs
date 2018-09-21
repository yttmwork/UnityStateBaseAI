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
        monster.ChangeAnimation((int)Monster.State.DIE);
    }

    public void Update(Monster monster)
    {
        if (monster.IsAnimationEnd() == true)
        {
            // 死亡
            monster.Die();
        }
    }

    private DieState()
    {
    }

}
