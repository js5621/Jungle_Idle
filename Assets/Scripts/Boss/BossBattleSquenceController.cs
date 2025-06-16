using Cysharp.Threading.Tasks;
using UnityEngine;

public class BossBattleSquenceController : MonoBehaviour
{
    BossGenerateController bossGenerateController;
    BossMoveController bossMoveController;
    CameraController cameraController;
    PlayerManager playerManager;
    GameUIController gameUIController;
    GameFlowController gameFlowController;
    StageController stageController;
    TimerController timerController;

    bool isPlayerArrived;
    bool isBossWatch;
    private bool isGameEndSequnceOn;
    public bool isPlayerWin = false;
    public bool isTimeOverLose = false;
    private void Start()
    {
        bossGenerateController = FindAnyObjectByType<BossGenerateController>();
        gameFlowController = FindAnyObjectByType<GameFlowController>();
        cameraController = FindAnyObjectByType<CameraController>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        gameUIController = FindAnyObjectByType<GameUIController>();
        stageController = FindAnyObjectByType<StageController>();
        timerController = FindAnyObjectByType<TimerController>();
    }
    
    private async void Update()
    {
        if ((isTimeOverLose || isPlayerWin) && gameFlowController.gameState == GameFlowState.Field)
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
        if (bossMoveController.isBossAppeared)
        {
            cameraController.isBossCameraMode = true;
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
        if (isGameEndSequnceOn)
        {
            return;
        }
        isGameEndSequnceOn = true;

        await UniTask.Delay(2000);
        bossGenerateController.isBossTime = false;

        if (isPlayerWin)
        {
            string rsltTxt = "???? ??!";
            stageController.ResultStageUIOn(rsltTxt);
            await UniTask.Delay(1000);
            stageController.ResultStageUIOff();
            stageController.goToNextSubStage();
            bossGenerateController.UpdgradeBossDefenceStatus();
        }

        if (isTimeOverLose)
        {
            string rsltTxt = "?? ??";
            stageController.ResultStageUIOn(rsltTxt);
            await UniTask.Delay(1000);
            stageController.ResultStageUIOff();
        }

        gameFlowController.gameState = GameFlowState.Ready;

        isPlayerWin = false;
        isTimeOverLose = false;
        bossGenerateController.isBossTime = false;
        bossGenerateController.isBossSpawn = false;
        playerManager.isPlayerBossBattleMode = false;

        gameUIController.BackUpBossHpData();
        gameUIController.BackupTimeData();
        timerController.BackUpTimeCount();

        await UniTask.Delay(1000);
        isGameEndSequnceOn = false;
    }
}
