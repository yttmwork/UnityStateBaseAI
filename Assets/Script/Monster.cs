using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    
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

    private Animator animator;          // アニメータ

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

    public float WalkSpeed
    {
        get
        {
            return this.walkSpeed;
        }
    }

    public float ChaseSpeed
    {
        get
        {
            return this.chaseSpeed;
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
            return this.moveDirection;
        }

        set
        {
            this.moveDirection = value;
        }
    }

    public bool IsAttackStart
    {
        get
        {
            return this.isAttackStart;
        }

        set
        {
            this.isAttackStart = value;
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

    public void ChangeAnimation(int state)
    {
        GetComponent<Animator>().SetInteger("State", state);
    }

    // Use this for initialization
    void Start ()
    {
        switch (this.activeState)
        {
            case State.WAIT:
                this.ChangeState(WaitState.Instance);
                break;
            case State.WALK:
                this.ChangeState(WalkState.Instance);
                break;
            case State.CHASE:
                this.ChangeState(ChaseState.Instance);
                break;
            case State.ATTACK:
                this.ChangeState(AttackState.Instance);
                break;
            case State.ATTACK_WAIT:
                this.ChangeState(AttackWaitState.Instance);
                break;
            case State.DAMAGE:
                this.ChangeState(DamageState.Instance);
                break;
            case State.DIE:
                this.ChangeState(DieState.Instance);
                break;
        }

        animator = GetComponent<Animator>();
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
            this.isAttackStart = true;
        }
        else if (other.tag == "PlayerAttack")
        {
            // ダメージ状態じゃなかったらダメージ
            if (this.state is DamageState == false)
            {
                this.hp--;
                if (this.hp > 0)
                {
                    this.ChangeState(DamageState.Instance);
                }
                else
                {
                    this.ChangeState(DieState.Instance);
                }
            }
        }
    }

}
