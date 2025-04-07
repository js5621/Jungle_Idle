using Cysharp.Threading.Tasks;
using UnityEngine;



public class FiedMonsterController : MonoBehaviour
{
    bool isDamgeSquenceOn;
    bool isDamgeSquenceOff;
    Animator monsterAnimator;
    int monsterHp = 2;
    private float speed = 0.5f;
    private Vector2 moveTarget;
    private Vector2 position;
    private bool isWalk;
    private bool isAttacking = false;
    public bool isMonSterDie = false;
    private float moveableDistance = 1.0f;
    FieldStandardBattleController fieldStandardBattleController;
    PlayerManager playerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    async void Update()
    {

        MoveAttackSequence();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name.Equals("PlayerAttack"))
        {
            if(monsterHp>0)
            {
                Debug.Log("피격 판정");
                monsterAnimator.SetTrigger("Damage");
                monsterHp -= 1;
            }

            else
            {
                Debug.Log("사망");
                monsterAnimator.SetTrigger("Die");
                isMonSterDie =false;
                Destroy(this.gameObject, 1f);

            }
        }
    }

    public async void MoveAttackSequence()
    {
        if (Vector2.Distance(playerManager.transform.position, transform.position) > moveableDistance)
        {
            float step = speed * Time.deltaTime;
            moveTarget = playerManager.transform.position;
            monsterAnimator.SetBool("IsWalk", true);// move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);
        }

        else
        {
            if (this.transform != null)
            {
                if(isMonSterDie)
                {
                    await AttackSequence();
                }
                
            }

        }
    }

    async UniTask AttackSequence()
    {
        if (isAttacking)
        {
            return;
        }
        isAttacking = true;
        monsterAnimator.SetBool("IsWalk", false);// move sprite towards the target location
        monsterAnimator.SetTrigger("Attack");
        await UniTask.Delay(700);
        isAttacking = false;
    }
}



//async UniTask GotDamage()
//{
//    isDamgeSquenceOn = false;
//    monsterHp -= 1;
//    if (monsterHp <= 0)
//    {
//        await UniTask.Delay(100);
//        monsterAnimator.SetTrigger("Die");
//        await UniTask.Delay(500);
//        Destroy(this);
//    }

//    else
//    {
//        monsterAnimator.SetTrigger("Damage");
//        await UniTask.Delay(500);
//        isDamgeSquenceOff = true;
//    }




//}

