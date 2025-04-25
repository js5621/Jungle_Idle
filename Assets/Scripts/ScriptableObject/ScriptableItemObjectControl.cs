using System;
using UnityEngine;

public class ScriptableItemObjectControl : MonoBehaviour
{
    [SerializeField] ArmDataListSO armDataListSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int getArm_atk_bonus_val(int index)
    {
        return armDataListSO.armItems[index].arm_atk_bonus_val;
    }

    public int getArm_atk_atkSpeed_bonus_val(int index)
    {
        return armDataListSO.armItems[index].arm_atkSpeed_bonus_val;
    }

    public string getItemStatInfoString(int index)
    {
        string msgString = "";
        //장비명 설정
        msgString += "장비명\n";
        msgString += armDataListSO.armItems[index].arm_name + "\n";
        msgString += "\n";
        //장비타입 설정
        msgString += "장비 타입\n";
        msgString += armDataListSO.armItems[index].arm_type + "\n";
        msgString += "\n";
        // 장비효과 설정
        msgString += "장비 효과 설정\n";

        if (armDataListSO.armItems[index].arm_atk_bonus_val != 0)
        {
            msgString += "공격력 보정:" + armDataListSO.armItems[index].arm_atk_bonus_val + "\n";
        }

        if (armDataListSO.armItems[index].arm_atkSpeed_bonus_val != 0)
        {
            msgString += "공격속도 보정:" + armDataListSO.armItems[index].arm_atkSpeed_bonus_val + "\n";
        }

        return msgString;

    }
}
