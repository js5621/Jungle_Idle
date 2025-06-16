using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameObject bossbattleUI;
   
    GameUIController gameUIController;
   
    public int timerCount = 22;
    public int initialTimeSeconds = 0;
   
    bool isTimerOn = false;
    void Start()
    {
        initialTimeSeconds = timerCount;
        gameUIController = FindAnyObjectByType<GameUIController>();
    }
    async void Update()
    {
        if (bossbattleUI.activeSelf)
        {
            await TimerSequence();
        }
    }
    public void BackUpTimeCount()
    {
        timerCount = initialTimeSeconds;
    }
    async UniTask TimerSequence()
    {
        if (timerCount <= 0 || isTimerOn)
        {
            return;
        }
        
        isTimerOn = true;

        int timerIntervalSec = 1000;

        while (timerCount > 0)
        {
            if (gameUIController.IsBossHpOver())
            {
                break;
            }
            await UniTask.Delay(timerIntervalSec);
            gameUIController.TimeReduction();
            timerCount--;
        }
        
        await UniTask.Delay(500);
        
        isTimerOn = false;
    }
}
