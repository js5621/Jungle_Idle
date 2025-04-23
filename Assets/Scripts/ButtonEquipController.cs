using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ButtonEquipController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject parentBtnGoup;

    int childState = 0;

    PlayerManager playerManager;
    ScriptableItemObjectControl scriptableItemObjectControl;
    public void Start()
    {
        SetChildState();
        playerManager = FindAnyObjectByType<PlayerManager>();
        scriptableItemObjectControl = FindAnyObjectByType<ScriptableItemObjectControl>();
    }

    public void ProcessEquipByButton()
    {
        if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÀåÂø"))
        {
            
            for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
            {

                if (parentBtnGoup.transform.GetChild(i) == transform)
                {
                    continue;
                }

                else
                {
                    if (parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÇØÁ¦"))
                    {
                        playerManager.RollBackAtk(scriptableItemObjectControl.getArm_atk_bonus_val(i));
                        playerManager.RollBackAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(i));
                        parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÀåÂø";

                        
                    }

                    parentBtnGoup.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
            }

            Debug.Log(" ÀåÂø½ÃµµÁß ");
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÇØÁ¦";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            playerManager.SetplayerAtk(scriptableItemObjectControl.getArm_atk_bonus_val(childState));
            playerManager.RollBackAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(childState));


        }

        else if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÇØÁ¦"))
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÀåÂø";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            playerManager.RollBackAtk(scriptableItemObjectControl.getArm_atk_bonus_val(childState));
            playerManager.RollBackAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(childState));


        }
    }

    public void SetChildState()
    {
        for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
        {
            if (parentBtnGoup.transform.GetChild(i).gameObject == transform)
            {
                childState = i;
                break;
            }
        }
    }
        // Update is called once per frame
    void Update()
    {

    }
}
