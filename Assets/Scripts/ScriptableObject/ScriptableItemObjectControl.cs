using System;
using UnityEngine;

public class ScriptableItemObjectControl : MonoBehaviour
{
    [SerializeField] ArmDataListSO armDataListSO;
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
        msgString += "무기명:\n";
        msgString += armDataListSO.armItems[index].arm_name + "\n";
        msgString += "\n";
      
        msgString += "무기 타입 :\n";
        msgString += armDataListSO.armItems[index].arm_type + "\n";
        msgString += "\n";
       
        msgString += "무기 부가 효과\n";

        if (armDataListSO.armItems[index].arm_atk_bonus_val != 0)
        {
            msgString += "공격력 보정 : " + armDataListSO.armItems[index].arm_atk_bonus_val + "\n";
        }
        if (armDataListSO.armItems[index].arm_atkSpeed_bonus_val != 0)
        {
            msgString += "공격 속도 보정 :" + armDataListSO.armItems[index].arm_atkSpeed_bonus_val + "\n";
        }
        
        return msgString;
    }
}
