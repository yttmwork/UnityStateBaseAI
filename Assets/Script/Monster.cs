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
    private float walkSpeed;            // 歩き速度

    [SerializeField]
    private float walkChangeTime;       // 歩き切り替え時間

    [SerializeField]
    private float chaseSpeed;           // 追跡速度

    [SerializeField]
    private float chaseRange;           // 追跡範囲

    [SerializeField]
    private float attackWaitChangeTime; // 攻撃待機切り替え時間

    [SerializeField]
    private GameObject targetObject;    // 追跡オブジェクト

    [SerializeField]
    private int hp;                     // 体力

    private IStateBase state;           // 状態インスタンス

    private float timer;                // タイマー

    private Vector3 moveDirection;      // 方向

    private bool isAttackStart;         // 攻撃開始トリガ

    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }
    }

    public float ChaseSpeed
    {
        get
        {
            return chaseSpeed;
        }
    }

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

    public Vector3 MoveDirection
    {
        get
        {
            return moveDirection;
        }

        set
        {
            moveDirection = value;
        }
    }

    public bool IsAttackStart
    {
        get
        {
            return isAttackStart;
        }

        set
        {
            isAttackStart = value;
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

    // 歩き終了判定
    public bool IsWalkEnd()
    {
        if (this.timer >= this.walkChangeTime)
        {
            return true;
        }

        return false;
    }

    // 攻撃待機終了判定
    public bool IsAttackWaitEnd()
    {
        if (this.timer >= this.attackWaitChangeTime)
        {
            return true;
        }

        return false;
    }

    // 移動
    public void Move(float speed)
    {
        this.transform.Translate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
    }

    // ターゲットの方に向く
    public void TurnToObject()
    {
        if (this.targetObject == null)
        {
            return;
        }

        this.transform.LookAt(this.targetObject.transform);
        this.transform.eulerAngles = new Vector3(0.0f, this.transform.eulerAngles.y, 0.0f);
    }

    // 死亡処理
    public void Die()
    {
        Destroy(this.gameObject);
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
                ChangeState(WalkState.Instance);
                break;
            case State.CHASE:
                ChangeState(ChaseState.Instance);
                break;
            case State.ATTACK:
                ChangeState(AttackState.Instance);
                break;
            case State.ATTACK_WAIT:
                ChangeState(AttackWaitState.Instance);
                break;
            case State.DAMAGE:
                ChangeState(DamageState.Instance);
                break;
            case State.DIE:
                ChangeState(DieState.Instance);
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        this.state.Update(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAttackStart = true;
        }
        else if (other.tag == "PlayerAttack")
        {
            // ダメージ状態じゃなかったら
            if (state is DamageState == false)
            {
                this.hp--;
                if (this.hp > 0)
                {
                    ChangeState(DamageState.Instance);
                }
                else
                {
                    ChangeState(DieState.Instance);
                }
            }
        }
    }

}
