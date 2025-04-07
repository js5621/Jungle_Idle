using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.GraphicsBuffer;

public class PlayerManager : MonoBehaviour
{
    Vector3 randomVector;
    private int playercharSpeed = 2;
    // 플레이어의 캐릭터가 좌우로 이동한다.
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

    public Vector3 tempVector = Vector3.zero;
    public Vector3 destinationVector;
    public Vector2 moveTarget;

    public GameObject skillObject;
    
    
    public float moveableDistance = 10.0f;
    private RandomPointGenerator randomPointGenerator;
    private FieldStandardBattleController fieldSBattleController;
    private FiedMonsterController fiedMonsterController;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    FieldGameOperator fieldGameOperator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fieldGameOperator = FindAnyObjectByType<FieldGameOperator>();
        fiedMonsterController = FindAnyObjectByType<FiedMonsterController>();   
        randomPointGenerator = FindAnyObjectByType<RandomPointGenerator>();
        playerAnimator = GetComponent<Animator>();
        //fieldSBattleController = FindAnyObjectByType<FieldStandardBattleController>();
        //playerSpriteRenderer = GetComponent<SpriteRenderer>();




    }

    // Update is called once per frame
    async void Update()
    {
        MoveAttackSequence();
       

    }

    public async void MoveAttackSequence()
    {
        if (fiedMonsterController == null)
        {
            fiedMonsterController = FindAnyObjectByType<FiedMonsterController>();
        }
        if (Vector2.Distance(fiedMonsterController.transform.position, transform.position) > moveableDistance)
        {
            if (transform.position.x - fiedMonsterController.transform.position.x >0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.localScale = transform.localScale * 1.7f;
            }
            else if (transform.position.x - fiedMonsterController.transform.position.x <0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.localScale = transform.localScale * 1.7f;
            }

            float step = playercharSpeed * Time.deltaTime;
            moveTarget = fiedMonsterController.transform.position;
            playerAnimator.SetBool("IsWalk", true);// move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);
        }

        else
        {
            if (this.transform != null)
            {
                playerAnimator.SetBool("IsWalk", false);// move sprite towards the target location
                await PlayerAttack();
            }

        }
    }
    async UniTask OnRandomMove()
    {
        Vector3 randomVector;

        isPlayerSequenceOn = false;


        tempVector = this.transform.localPosition;
        randomVector = randomPointGenerator.RandomVectorArray[Random.Range(0, randomPointGenerator.RandomVectorArray.Length)];

        //if (randomVector.x < 0)
        //{

        //}
        //else if (randomVector.x > 0)
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //    transform.localScale = transform.localScale * 1.7f;
        //}

        // playerAnimator.SetBool("IsWalk", true);
        var step = playercharSpeed * Time.deltaTime; // calculate distance to mov
        Vector2 speed = new Vector2(0.5f, 0.5f);
        var duration = 1.0f;
        var until = Time.time + duration;
        while (Time.time < until)
        {

            transform.position = Vector2.SmoothDamp(transform.position, destinationVector, ref speed, step);
            await UniTask.Yield();
        }
        transform.localScale = new Vector3(-1, 1, 1);
        transform.localScale = transform.localScale * 1.7f;
        await FadeIn();

        await UniTask.Delay(100);
        isAttackSequenceOn = true;



    }

    async UniTask FadeIn()
    {
        float alphaVal = playerSpriteRenderer.color.a;
        Color tmp = playerSpriteRenderer.color;


        while (playerSpriteRenderer.color.a < 1.0f)
        {
            alphaVal += 0.05f;
            tmp.a = alphaVal;
            playerSpriteRenderer.color = tmp;

            await UniTask.Yield();
        }

    }

    async UniTask FadeOut()
    {
        float alphaVal = playerSpriteRenderer.color.a;
        Color tmp = playerSpriteRenderer.color;

        while (playerSpriteRenderer.color.a > 0.0f)
        {
            alphaVal -= 0.05f;
            tmp.a = alphaVal;
            playerSpriteRenderer.color = tmp;

            await UniTask.Yield();
        }
    }

    async UniTask PlayerAttack()
    {


        if (isAttaking)
        {
            return;
        }
        isAttaking = true;
        Debug.Log("반복체크 중");
        playerAnimator.SetTrigger("Attack1");
        await UniTask.Delay(100);
        skillObject.SetActive(true);
        await UniTask.Delay(1000);
        skillObject.SetActive(false);
        await UniTask.Delay(100);
        isAttaking = false;


        //await FadeOut();

        //isPlayerSequenceOff = true;
        //fieldGameOperator.OperateProcess();
    }

    async UniTask AttackSet()
    {

    }


}
