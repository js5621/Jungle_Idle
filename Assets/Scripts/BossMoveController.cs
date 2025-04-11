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
    private bool isAttacking= false;
    public GameObject bossAtkObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBossAppeared=true;
        bossBattleSquenceController = FindAnyObjectByType<BossBattleSquenceController>();
        bossBattleSquenceController.SequnceBossToCamera();
        playerManager =FindAnyObjectByType<PlayerManager>();
        bossAnimator = GetComponent<Animator>();


        attackDistance = 2.0f;
    }

    // Update is called once per frame
    async void Update()
    {
        if (Vector2.Distance(playerManager.gameObject.transform.position, transform.position) < attackDistance)
        {
            await BossAttack();
        }

    }

    async UniTask BossAttack()
    {
        bossBattleSquenceController.BossWatchPlayerCheck(true);
        if(!bossBattleSquenceController.isBattleStartCondition())
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
}
