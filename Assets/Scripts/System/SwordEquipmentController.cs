using TMPro;
using UnityEngine;

public class SwordEquipController : MonoBehaviour
{
    [SerializeField] GameObject ArmExplainPanelGroup;
    
    ScriptableItemObjectControl scriptableItemObjectControl; 
    void Start()
    {
        scriptableItemObjectControl = FindAnyObjectByType<ScriptableItemObjectControl>();

        for (int i = 0; i < ArmExplainPanelGroup.transform.childCount; i++)
        {
            ArmExplainPanelGroup.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scriptableItemObjectControl.getItemStatInfoString(i);
        }
    }
}
