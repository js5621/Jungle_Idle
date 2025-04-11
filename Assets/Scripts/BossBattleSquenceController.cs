using UnityEngine;

public class BossBattleSquenceController : MonoBehaviour
{

    BossMoveController bossMoveController;
    CameraController cameraController;
    PlayerManager playerManager;
    GameUIController gameUIController;
    bool isPlayerArrived;
    bool isBossWatch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
        cameraController = FindAnyObjectByType<CameraController>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        gameUIController = FindAnyObjectByType<GameUIController>();
    }
    private async void Update()
    {
        if(isBattleStartCondition())
        {
            await gameUIController.BattleBossUISetOn();
        }
    }
    public void PlayerArrivalCheck(bool setArrival)
    {
        if(setArrival)
        {
            isPlayerArrived = true;
        }
    


    }

    public void BossWatchPlayerCheck(bool setWatch)
    {
        if(setWatch)
        {
            isBossWatch =true;
        }
    }

    public bool isBattleStartCondition()
    {
        if(isPlayerArrived&&isBossWatch)
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
        cameraController.isBossCameraMode =false;
        playerManager.isPlayerBossBattleMode =true;
    }
}
