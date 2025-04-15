using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

public class BossBattleSquenceController : MonoBehaviour
{
    BossGenerateController bossGenerateController;
    BossMoveController bossMoveController;
    CameraController cameraController;
    PlayerManager playerManager;
    GameUIController gameUIController;
    GameFlowController gameFlowController;
    bool isPlayerArrived;
    bool isBossWatch;
    private bool isGameEndSequnceOn;
    public bool isPlayerWin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        bossGenerateController = FindAnyObjectByType<BossGenerateController>();
        gameFlowController = FindAnyObjectByType<GameFlowController>();
        cameraController = FindAnyObjectByType<CameraController>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        gameUIController = FindAnyObjectByType<GameUIController>();
    }
    private async void Update()
    {
        if (isBattleStartCondition())
        {
            await gameUIController.BattleBossUISetOn();
        }

        if(isPlayerWin&&gameFlowController.gameState ==GameFlowState.Field)
        {
            await GameEndSequence();
        }
        
    }
    public void PlayerArrivalCheck(bool setArrival)
    {
        if (setArrival)
        {
            isPlayerArrived = true;
        }



    }

    



    public void BossWatchPlayerCheck(bool setWatch)
    {
        if (setWatch)
        {
            isBossWatch = true;
        }

        else
        {
            isBossWatch = false;
        }
    }

    public bool isBattleStartCondition()
    {
        if (isPlayerArrived && isBossWatch)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void SequnceBossToCamera()
    {
        bossMoveController = FindAnyObjectByType<BossMoveController>();
        if (bossMoveController.isBossAppeared)// 보스가 등장하면 
        {
            cameraController.isBossCameraMode = true; // 카메라  시점을 바꾼다.
        }

    }

    public Vector2 GetBossPosition()
    {
        bossMoveController = FindAnyObjectByType<BossMoveController>();
        return bossMoveController.gameObject.transform.position;
    }

    public void SequeneceCameraToPlayer()
    {
        cameraController.isBossCameraMode = false;
        playerManager.isPlayerBossBattleMode = true;
    }


    async UniTask GameEndSequence()
    {
        Debug.Log("보스 사망 후처리 시작");
        if (isGameEndSequnceOn)
        {
            return;
        }
        isGameEndSequnceOn = true;



        await UniTask.Delay(2000);
        bossGenerateController.isBossTime = false;
        gameFlowController.gameState = GameFlowState.Ready;
        isPlayerWin =false;
        bossGenerateController.isBossTime = false;
        bossGenerateController.isBossSpawn = false;
        playerManager.isPlayerBossBattleMode =false;

        await UniTask.Delay(1000);
        isGameEndSequnceOn = false;

       

    }
}
