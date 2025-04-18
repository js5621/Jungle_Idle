using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GameUIController gameUIController;
    BossMoveController bossMoveController;
    BossGenerateController bossGenerateController;
    GameFlowController gameFlowController;

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

    }





}
