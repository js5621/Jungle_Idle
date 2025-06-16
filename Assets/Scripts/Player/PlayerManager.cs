using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector3 randomVector;
    private int playercharSpeed = 8;
    private int playerAtk = 30;
    int atkSpeedBonus = 0;

    public bool isPlayCharMove = false;
    private bool isEncounter = false;
    public bool isPlayerStop = false;
    public bool isPlayerMoveStart = false;
    public bool isPlayerMoveEnd = false;
    public bool isPlayerSequenceOn = false;
    public bool isPlayerSequenceOff = false;
    public bool isAttackSequenceOn = false;
    public bool isAttaking = false;
    public bool isAttackSequenceOff = false;
    public bool isPlayerBossBattleMode = false;
    
    public Vector3 tempVector = Vector3.zero;
    public Vector3 destinationVector;
    public Vector2 moveTarget;
    Vector3 initialPlayerLocalScale;


    public GameObject skillObject;
    private GameObject SearchObject;
    private GameObject targetObject;

    public float moveableDistance = 1.0f;

    Animator playerAnimator;

    SpriteRenderer playerSpriteRenderer;

    //FieldGameOperator fieldGameOperator;
    EnemySearchController enemySearchController;
    BossGenerateController bossGenerateController;
    GameUIController gameUIController;
    BossBattleSquenceController bossBattleSquenceController;
    GameFlowController gameFlowController;
    MonsterSpawnController monsterSpawnController;

    void Start()
    {
        enemySearchController = FindAnyObjectByType<EnemySearchController>();
        bossGenerateController = FindAnyObjectByType<BossGenerateController>();
        bossBattleSquenceController = FindAnyObjectByType<BossBattleSquenceController>();
        gameFlowController = FindAnyObjectByType<GameFlowController>();
        gameUIController = FindAnyObjectByType<GameUIController>();
        monsterSpawnController = FindAnyObjectByType<MonsterSpawnController>();
        playerAnimator = GetComponent<Animator>();

        moveableDistance = 1.0f;
        initialPlayerLocalScale = transform.localScale;
    }

    void Update()
    {
        MoveAttackSequence();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (targetObject != null)
            {
                return;
            }

            targetObject = collision.gameObject;
            SearchObject = collision.gameObject;
        }
    }

    public void SetPlayerAtkSpeed(int minusSpeedValue)
    {
        atkSpeedBonus = minusSpeedValue;
    }

    public void RollBackAtkSpeed(int minusSpeedValue)
    {
        if (atkSpeedBonus > 0)
        {
            atkSpeedBonus -= minusSpeedValue;
        }
        else if (atkSpeedBonus < 0)
        {
            atkSpeedBonus += minusSpeedValue;
        }
    }

    public void SetplayerAtk(int addAtkaValue)
    {
        playerAtk += addAtkaValue;
    }

    public void RollBackAtk(int addAtkaValue)
    {
        playerAtk -= addAtkaValue;
    }

    public int GetPlayerAtk()
    {
        return playerAtk;
    }

    public async void MoveAttackSequence()
    {
        if (gameFlowController.gameState != GameFlowState.Field)
        {
            return;
        }
        if (!isPlayerBossBattleMode)
        {
            if (SearchObject == null)
            {
                SearchObject = monsterSpawnController.spawnedMonsterQueue.Dequeue();

                if (SearchObject == null)
                {
                    return;
                }
            }
        }
        else
        {
            if (enemySearchController.isEnemyNull(SearchObject))
            {
                SearchObject = enemySearchController.FoundEnemy();
                if (SearchObject == null)
                {
                    return;
                }
            }
        }

        playercharSpeed = 5;

        if (SearchObject.gameObject.tag.Equals("Boss"))
        {
            if (!isPlayerBossBattleMode)
            {
                return;
            }
            else
            {
                moveableDistance = 1.5f;
            }
        }

        if (Vector2.Distance(SearchObject.transform.position, transform.position) > moveableDistance)
        {
            if (transform.position.x - SearchObject.transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.localScale = transform.localScale * initialPlayerLocalScale.x;
            }
            else if (transform.position.x - SearchObject.transform.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.localScale = transform.localScale * initialPlayerLocalScale.x;
            }

            float step = playercharSpeed * Time.deltaTime;
            moveTarget = SearchObject.transform.position;
            playerAnimator.SetBool("IsWalk", true); // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);
        }

        else
        {
            if (isPlayerBossBattleMode)
            {
                await gameUIController.BattleBossUISetOn();
            }
            
            playercharSpeed = 0;

            if (transform != null)
            {
                playerAnimator.SetBool("IsWalk", false); // move sprite towards the target location
                await PlayerAttack();
            }
        }
    }

    async UniTask PlayerAttack()
    {
        if (isPlayerBossBattleMode)
        {
            bossBattleSquenceController.PlayerArrivalCheck(true);
        }
        if (!bossBattleSquenceController.isBattleStartCondition() && isPlayerBossBattleMode)
        {
            return;
        }
        if (isAttaking)
        {
            return;
        }

        isAttaking = true;

        playerAnimator.SetTrigger("Attack1");
        await UniTask.Delay(300);

        skillObject.SetActive(true);
        await UniTask.Delay(800 - atkSpeedBonus);

        skillObject.SetActive(false);
        await UniTask.Delay(800 - atkSpeedBonus);

        isAttaking = false;
    }
}