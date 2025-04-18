using Cysharp.Threading.Tasks;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BossGenerateController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI bossApearTxt;
    public GameObject[] bossMobPrfab;
    public GameObject gamePlayer;
    public Transform bossMobTransform;

    private Vector3 bossVectorOffset;

    StageController stageController;
    GameUIController gameUIController;

    string bossAppearString = "��������!";

    public bool isBossTime = false;
    public bool isBossSpawn = false;

    int bossDefenceStatus = 20;


    public void Start()
    {
        // gameObject.SetActive(true);
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
        bossDefenceStatus = bossDefenceStatus + 20;
    }

    public int GetBossDefenceStatusValue()
    {
        return bossDefenceStatus;
    }

}
