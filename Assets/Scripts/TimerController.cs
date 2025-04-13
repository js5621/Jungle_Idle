using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]GameObject bossbattleUI;
    GameUIController gameUIController;
    public int timerCount = 20;
    bool isTimerOn =false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (bossbattleUI.activeSelf)
        {
            await TimerSequence();
        }
    }

    async UniTask TimerSequence()
    {
        if (timerCount < 0 || isTimerOn)
        {
            return;
        }
        isTimerOn = true;
        while (timerCount > 0)
            {
                timerCount--;
                gameUIController.TimeReduction();
                await UniTask.Delay(1000);
            }

        isTimerOn= false;
    }
}
