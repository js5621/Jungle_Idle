using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageControl : MonoBehaviour
{

    [SerializeField]GameObject startUIPanel;
    
    GameFlowController gameFlowController;
    bool isStagePanelOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameFlowController = FindAnyObjectByType<GameFlowController>();
        
    }

    // Update is called once per frame
    async void Update()
    {
        if(gameFlowController.gameState ==GameFlowState.Ready)
        {
            await StartStagePanelOnOff();
        }
    }

    async UniTask StartStagePanelOnOff()
    {
        if (isStagePanelOn)
        {
            return;
        }
        isStagePanelOn =true;

        
        await UniTask.Delay(2000);
        startUIPanel.SetActive(true);
        await UniTask.Delay(2000);
        startUIPanel.SetActive(false);

        gameFlowController.gameState = GameFlowState.Field;
        await UniTask.Delay(1000);
        isStagePanelOn=false;


    }
}
