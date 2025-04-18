using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{

    [SerializeField] GameObject stageUIPanel;
    [SerializeField] TextMeshProUGUI stageUIText;

    GameFlowController gameFlowController;
    bool isStagePanelOn = false;

    int subStageValue = 1;
    int mainStageValue = 1;

    int tmpDebugIdx = 0;// ���� �׽�Ʈ��
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameFlowController = FindAnyObjectByType<GameFlowController>();

    }

    // Update is called once per frame
    async void Update()
    {
        if (gameFlowController.gameState == GameFlowState.Ready)
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
        isStagePanelOn = true;

    
        if (stageUIText.text.Equals("�̼� Ŭ����"))
        {
            stageUIText.text = $"�������� {mainStageValue}.{subStageValue}\n �̼� ����!";
        }

        if (stageUIText.text.Equals("�̼ǽ���..."))
        {
            Debug.Log("���� �޼��� Ȯ�� ");
            stageUIText.text = $"�������� {mainStageValue}.{subStageValue}\n �̼� ����";
        }


        await UniTask.Delay(2000);
        stageUIPanel.SetActive(true);
        await UniTask.Delay(2000);
        stageUIPanel.SetActive(false);

        gameFlowController.gameState = GameFlowState.Field;
        await UniTask.Delay(1000);
        isStagePanelOn = false;


    }

    public void ResultStageUIOn(string resultText)
    {
        stageUIPanel.SetActive(true);
        stageUIText.text = resultText;
    }

    public void ResultStageUIOff()
    {
        stageUIPanel.SetActive(false);
    }



    public void goToNextSubStage()
    {
        if (subStageValue == 2)
        {
            goToNextMaingStge();
            subStageValue = 1;
        }

        else
        {
            subStageValue += 1;

        }
    }

    public void goToNextMaingStge()
    {
        mainStageValue += 1;
    }

    public int getSubstageValue()
    {
        return subStageValue;

    }

    public int getMainStageValue()
    {
        return mainStageValue;
    }

}
