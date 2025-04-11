using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GameObject bossHpPanel;
    public GameObject BossBattlePanel;
    RectTransform bossHpRect;
    private bool isBossUITurnOn =false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHpRect = bossHpPanel.GetComponent<RectTransform>();
    }

    public void BossHPUIDamgage()
    {
        
        bossHpRect.anchoredPosition = new Vector2(bossHpRect.anchoredPosition.x - 27f, bossHpRect.anchoredPosition.y);
        bossHpRect.sizeDelta = new Vector2(bossHpRect.sizeDelta.x - 55f, bossHpRect.sizeDelta.y);
    }
    public bool isBossHpOver()
    {
        if(bossHpRect.sizeDelta.x<0f)
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
        if (isBossUITurnOn)
        {
            return;
        }
        isBossUITurnOn=true;
        await UniTask.Delay(1200);
        BossBattlePanel.SetActive(true);

    }


    public void BattleBossUISetOff()
    {
        BossBattlePanel.SetActive(false);
    }

    // Update is called once per frame

}
