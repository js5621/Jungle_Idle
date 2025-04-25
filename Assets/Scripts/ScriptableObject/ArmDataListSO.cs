using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static ServerConnect;

[CreateAssetMenu(fileName = "ArmDataList", menuName = "Data/ArmDataList")]
public class ArmDataListSO : ScriptableObject
{
    public List<JsonArmItem> armItems = new List<JsonArmItem>();
}