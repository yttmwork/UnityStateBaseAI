using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    
    // 状態用定数
    public enum State
    {
        WAIT,           // 待機
        WALK,           // 移動
        CHASE,          // 追跡
        ATTACK,         // 攻撃
        ATTACK_WAIT,    // 攻撃待機
        DAMAGE,         // ダメージ
        DIE,            // 死亡
    }

    [SerializeField]
    private State activeState;          // 今の状態

    private IStateBase state;           // 状態インスタンス

    // 状態切り替え
    public void ChangeState(IStateBase next_state)
    {
        this.state = next_state;
        this.state.Init(this);
    }

    // Use this for initialization
    void Start ()
    {
        switch (this.activeState)
        {
            case State.WAIT:

                break;
            case State.WALK:

                break;
            case State.CHASE:

                break;
            case State.ATTACK:

                break;
            case State.ATTACK_WAIT:

                break;
            case State.DAMAGE:

                break;
            case State.DIE:

                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        this.state.Update(this);
    }
}
