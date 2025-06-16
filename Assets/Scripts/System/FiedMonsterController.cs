using Cysharp.Threading.Tasks;
using UnityEngine;
public class FiedMonsterController : MonoBehaviour
{
    bool isDamgeSquenceOn;
    bool isDamgeSquenceOff;
    
    Animator monsterAnimator;
    int monsterHp = 1;
    private float speed = 0.5f;
    
    private Vector2 moveTarget;
    private Vector2 position;
    
    private bool isWalk;
    private bool isAttacking = false;
    public bool isMonSterDie = false;
    
    private float moveableDistance = 0.3f;

    PlayerManager playerManager;
    Vector3 initialLocalScale;
    public BossGenerateController bossController;
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        position = gameObject.transform.position;
        speed += Random.Range(0.5f, 2.5f);
        moveableDistance = 1.2f;
        initialLocalScale = gameObject.transform.localScale;
        bossController = FindAnyObjectByType<BossGenerateController>();
    }
    
    void Update()
    {
        if (!bossController.isBossTime)
        {
            MoveAttackSequence();
        }

        if (bossController.isBossTime)
        {
            if (this.gameObject != null)
            {
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("PlayerAttack"))
        {
            if (monsterHp > 0)
            {
                monsterAnimator.SetTrigger("Damage");
                monsterHp -= 1;
                if (monsterHp <= 0)
                {
                    monsterAnimator.SetTrigger("Die");
                    isMonSterDie = true;
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
    public async void MoveAttackSequence()
    {
        if (Vector2.Distance(playerManager.transform.position, transform.position) > moveableDistance)
        {
            float step = speed * Time.deltaTime;
            moveTarget = playerManager.transform.position;
            ChangeLocalScale();
            monsterAnimator.SetBool("IsWalk", true);// move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);
        }
        else
        {
            if (transform != null)
            {
                if (!isMonSterDie)
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
        monsterAnimator.SetBool("IsWalk", false);
        monsterAnimator.SetTrigger("Attack");
        await UniTask.Delay(700);
        isAttacking = false;
    }
    void KnockBack()
    {

        float step = speed * Time.deltaTime;
        if (transform.localScale.x > 0f)
        {
            Vector2 knockbackPosition = (Vector2)transform.position + Vector2.right * 5f;
            transform.position = Vector2.MoveTowards(transform.position, knockbackPosition, step);
        }
        else
        {
            Vector2 knockbackPosition = (Vector2)transform.position + Vector2.left * 5f;
            transform.position = Vector2.MoveTowards(transform.position, knockbackPosition, step);
        }

    }
    void ChangeLocalScale()
    {
        if (transform.position.x - playerManager.transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y / Mathf.Abs(transform.localScale.y), 1);
            transform.localScale = transform.localScale * initialLocalScale.x;
        }
        else if (transform.position.x - playerManager.transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y / Mathf.Abs(transform.localScale.y), 1);
            transform.localScale = transform.localScale * initialLocalScale.x;
        }
    }
}

