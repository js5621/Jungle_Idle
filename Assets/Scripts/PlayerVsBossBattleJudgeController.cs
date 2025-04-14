using Cysharp.Threading.Tasks;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GameUIController gameUIController;
    BossMoveController bossMoveController;
    BossGenerateController bossGenerateController;
    GameFlowController gameFlowController;
    
    bool isBossDieSequenceOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
        
        bossGenerateController = FindAnyObjectByType<BossGenerateController>();
        gameFlowController = FindAnyObjectByType<GameFlowController>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (gameUIController.isTimeOver())
        {

            
        }

        else
        {

            if (gameUIController.isBossHpOver())
            {
                await BossDieSequence();
            }
        }
    }

    async UniTask BossDieSequence()
    {

        if (isBossDieSequenceOn)
        {
            return;
        }
        isBossDieSequenceOn =true;
        
        bossMoveController = FindAnyObjectByType<BossMoveController>();
        bossMoveController.KillBoss();
        gameUIController.BattleBossUISetOff();
        await UniTask.Delay(2000);
        bossGenerateController.isBossTime =false;
        gameFlowController.gameState =GameFlowState.Ready;
        bossGenerateController.isBossTime = false;
        bossGenerateController.isBossSpawn = false;


    }

}
