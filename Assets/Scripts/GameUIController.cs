using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GameObject bossHpPanel;
    public GameObject timerPanel;
    public GameObject BossBattlePanel;
    RectTransform bossHpRect;
    RectTransform gameTimerRect;
    private bool isBossUITurnOn = false;

    Vector2 timeAnchoredPositionBkData;
    Vector2 timeSizeDeltaPositionBkData;
    Vector2 bossHpAnchoredPositionBkData;
    Vector2 bossSizeDeltaPostionBkData;

    PlayerManager playerManager;
    BossGenerateController bossGenerateController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHpRect = bossHpPanel.GetComponent<RectTransform>();
        gameTimerRect = timerPanel.GetComponent<RectTransform>();


        playerManager = FindAnyObjectByType<PlayerManager>();
        bossGenerateController = FindAnyObjectByType<BossGenerateController>(); 

        // 보스  HP DAta 표시 UI 데이터 초기 정보 포지션 백업
        bossHpAnchoredPositionBkData = bossHpRect.anchoredPosition;
        bossSizeDeltaPostionBkData = bossHpRect.sizeDelta;

        // 제한시간 표시 UIData 초기 정보 포지션 백업
        timeAnchoredPositionBkData = gameTimerRect.anchoredPosition;
        timeSizeDeltaPositionBkData = gameTimerRect.sizeDelta;

    }

    public void BossHPUIDamgage()
    {
       
        float sizeDeltaReduce = 55;

        float sizeDeltaReduceVal = sizeDeltaReduce + (playerManager.GetPlayerAtk() - bossGenerateController.GetBossDefenceStatusValue());
        float anchorReduceVal = sizeDeltaReduceVal / 2;
        if (anchorReduceVal <= 0)
        {
            anchorReduceVal = 0;
        }

        if (sizeDeltaReduceVal <= 0)
        {
            sizeDeltaReduceVal = 0;
        }
     

        bossHpRect.anchoredPosition = new Vector2(bossHpRect.anchoredPosition.x - anchorReduceVal, bossHpRect.anchoredPosition.y);
        bossHpRect.sizeDelta = new Vector2(bossHpRect.sizeDelta.x - sizeDeltaReduceVal, bossHpRect.sizeDelta.y);
    }

    public void TimeReduction()
    {
        gameTimerRect.anchoredPosition = new Vector2(gameTimerRect.anchoredPosition.x - 14f, gameTimerRect.anchoredPosition.y);
        gameTimerRect.sizeDelta = new Vector2(gameTimerRect.sizeDelta.x - 28f, gameTimerRect.sizeDelta.y);
    }
 
    public void BackupTimeData()
    {
        gameTimerRect.anchoredPosition = timeAnchoredPositionBkData;
        gameTimerRect.sizeDelta = timeSizeDeltaPositionBkData;

    }

    public void BackUpBossHpData()
    {
        bossHpRect.anchoredPosition = bossHpAnchoredPositionBkData;
        bossHpRect.sizeDelta = bossSizeDeltaPostionBkData;
    }

    public bool IsBossHpOver()
    {
        if (bossHpRect.sizeDelta.x < 0f)
        {
            return true;
        }

        else
        {
            return false;
        }

    }

    public bool IsTimeOver()
    {
        if (gameTimerRect.sizeDelta.x < 0f)
        {
            return true;
        }

        else
        {
            return false;
        }

    }
    public async UniTask BattleBossUISetOn()
    {
        if(BossBattlePanel.activeSelf||IsBossHpOver()||IsTimeOver())
        {
            return; 
        }
        await UniTask.Delay(500);
        BossBattlePanel.SetActive(true);

    }


    public void BattleBossUISetOff()
    {
        BossBattlePanel.SetActive(false);
    }

    // Update is called once per frame

}
