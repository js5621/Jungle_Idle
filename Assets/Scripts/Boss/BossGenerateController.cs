using Cysharp.Threading.Tasks;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BossGenerateController : MonoBehaviour
{
    public TextMeshProUGUI bossApearTxt;
    public GameObject[] bossMobPrfab;
    public GameObject gamePlayer;
    public Transform bossMobTransform;

    private Vector3 bossVectorOffset;

    StageController stageController;
    GameUIController gameUIController;

    string bossAppearString = "보스 등장!";

    public bool isBossTime = false;
    public bool isBossSpawn = false;

    int bossDefenceStatus = 20;
    public void Start()
    {
        bossVectorOffset = new Vector3(6f, 0f, 0);
        gameUIController = FindAnyObjectByType<GameUIController>();
        stageController = FindAnyObjectByType<StageController>();
    }
    public async void CallBoss()
    {
        if (isBossSpawn)
        {
            return;
        }

        isBossSpawn = true;

        await ShowBossAppearText();

        isBossTime = true;

        int spawnBossIndex = stageController.getMainStageValue() - 1;
        Instantiate(bossMobPrfab[spawnBossIndex], gamePlayer.transform.position + bossVectorOffset, Quaternion.identity);
    }
    async UniTask ShowBossAppearText()
    {
        bossApearTxt.text = "";
        bossApearTxt.gameObject.SetActive(true);

        for (int i = 0; i < bossAppearString.Length; i++)
        {
            bossApearTxt.text += bossAppearString[i];
            await UniTask.Delay(200);
        }

        bossApearTxt.gameObject.SetActive(false);
    }
    public void UpdgradeBossDefenceStatus()
    {
        bossDefenceStatus = bossDefenceStatus + 10;
    }
    public int GetBossDefenceStatusValue()
    {
        return bossDefenceStatus;
    }

}
