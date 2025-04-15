using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMoveController : MonoBehaviour
{
    Animator bossAnimator;
    public GameObject player;
    public bool isBossAppeared =false;

    private float attackDistance;
    public int Hp =1000;
    PlayerManager playerManager;
    BossBattleSquenceController bossBattleSquenceController;
    GameUIController gameUIController;
    private bool isAttacking= false;
    bool isBossDying =false;
    public GameObject bossAtkObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBossAppeared=true;
        bossBattleSquenceController = FindAnyObjectByType<BossBattleSquenceController>();
        bossBattleSquenceController.SequnceBossToCamera();
        gameUIController = FindAnyObjectByType<GameUIController>();
        playerManager =FindAnyObjectByType<PlayerManager>();
        bossAnimator = GetComponent<Animator>();


        attackDistance = 2.0f;
    }

    // Update is called once per frame
    async void Update()
    {
        if (Vector2.Distance(playerManager.gameObject.transform.position, transform.position) < attackDistance)
        {
            if(!gameUIController.isBossHpOver())
            {
                await BossAttack();
            }
            else
            {
                await BossDieSequence();
            }
            
        }

    }

    async UniTask BossAttack()
    {
        
        bossBattleSquenceController.BossWatchPlayerCheck(true);
        if(!bossBattleSquenceController.isBattleStartCondition()||this.gameObject ==null)
        {
            return;
        }


        if (isAttacking)
        {
            return;
        }
            isAttacking = true;
        Debug.Log("반복체크 중");
        bossAnimator.SetTrigger("Attack");
        await UniTask.Delay(300);
        bossAtkObject.SetActive(true);
        await UniTask.Delay(1000);
        bossAtkObject.SetActive(false);
        await UniTask.Delay(100);
        isAttacking = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("보스 골렘 충돌 감지 확인");

        if (collision.gameObject.name.Equals("PlayerAttack"))
        {
            Debug.Log("충돌 감지 확인");
           
        }
    }

    async UniTask BossDieSequence()
    {
        if(this.gameObject ==null)
        { 
            return;
        }
        if(isBossDying)
        {
            return;
        }
        isBossDying = true;
        bossAnimator.SetTrigger("Die");
        gameUIController.BattleBossUISetOff();
        await UniTask.Delay(1000);
        this.gameObject.SetActive(false);
        if (gameUIController.isTimeOver())
        {
            bossBattleSquenceController.isPlayerWin = false;
        }
        else if (gameUIController.isBossHpOver())
        {
            bossBattleSquenceController.isPlayerWin =true;
        }
        bossBattleSquenceController.BossWatchPlayerCheck(false);
        Destroy(this.gameObject, 2f);
    }
}
