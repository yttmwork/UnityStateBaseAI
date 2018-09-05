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

    [SerializeField]
    private float waitChangeTime;       // 待機の切り替え時間

    [SerializeField]
    private float chaseRange;           // 追跡範囲

    [SerializeField]
    private GameObject targetObject;    // 追跡オブジェクト

    private IStateBase state;           // 状態インスタンス

    private float timer;                // タイマー

    public float ChaseRange
    {
        get
        {
            return chaseRange;
        }
    }

    public float Timer
    {
        get
        {
            return this.timer;
        }

        set
        {
            this.timer = value;
        }
    }


    // 状態切り替え
    public void ChangeState(IStateBase next_state)
    {
        this.state = next_state;
        this.state.Init(this);
    }

    // 待機終了判定
    public bool IsWaitEnd()
    {
        if (this.timer >= this.waitChangeTime)
        {
            return true;
        }

        return false;
    }

    // 範囲判定
    public bool IsWithinRange(float range)
    {
        if (this.targetObject == null)
        {
            return false;
        }

        float distance = Vector3.Distance(this.targetObject.transform.position, this.transform.position);

        if (distance < range)
        {
            return true;
        }

        return false;
    }

    // Use this for initialization
    void Start ()
    {
        switch (this.activeState)
        {
            case State.WAIT:
                ChangeState(WaitState.Instance);
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
