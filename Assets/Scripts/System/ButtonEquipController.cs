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
        Debug.Log("cdstate" + childState);
        playerManager = FindAnyObjectByType<PlayerManager>();
        scriptableItemObjectControl = FindAnyObjectByType<ScriptableItemObjectControl>();
    }

    public void ProcessEquipByButton()
    {
        if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("장착"))
        {

            for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
            {

                if (parentBtnGoup.transform.GetChild(i) == transform)
                {
                    continue;
                }

                else
                {
                    if (parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("해제"))
                    {
                        playerManager.RollBackAtk(scriptableItemObjectControl.getArm_atk_bonus_val(i));
                        playerManager.RollBackAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(i));
                        parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "장착";
                    }

                    parentBtnGoup.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
            }

            Debug.Log(" 장착시도중 ");
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "해제";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            Debug.Log("보정값 확인" + childState + "번째버튼 보정값" + scriptableItemObjectControl.getArm_atk_bonus_val(childState).ToString());
            playerManager.SetplayerAtk(scriptableItemObjectControl.getArm_atk_bonus_val(childState));

            playerManager.SetPlayerAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(childState));

        }

        else if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("해제"))
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "장착";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            playerManager.RollBackAtk(scriptableItemObjectControl.getArm_atk_bonus_val(childState));
            playerManager.RollBackAtkSpeed(scriptableItemObjectControl.getArm_atk_atkSpeed_bonus_val(childState));
        }
    }

    public void SetChildState()
    {
        for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
        {
            if (parentBtnGoup.transform.GetChild(i) == transform)
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
