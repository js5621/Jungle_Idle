using Cysharp.Threading.Tasks;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BossGenerateController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI bossApearTxt;
    public GameObject bossMobPrfab;
    public GameObject gamePlayer;
    public Transform bossMobTransform;
    private Vector3 bossVectorOffset;

    GameUIController gameUIController;
    string bossAppearString = "보스등장!";
    public bool isBossTime = false;
    public bool isBossSpawn = false;

    public void Start()
    {
        // gameObject.SetActive(true);
        bossVectorOffset = new Vector3(5f, 2f, 0);
        gameUIController = FindAnyObjectByType<GameUIController>();
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
        Instantiate(bossMobPrfab,gamePlayer.transform.position+bossVectorOffset, Quaternion.identity);
        
        




    }



    // Update is called once per frame
    void Update()
    {

    }

    async UniTask ShowBossAppearText()
    {
        for (int i = 0; i < bossAppearString.Length; i++)
        {
            bossApearTxt.text += bossAppearString[i];
            await UniTask.Delay(200);
        }
        bossApearTxt.gameObject.SetActive(false);



    }

    
}
