using TMPro;
using UnityEngine;

public class SwordEquipController : MonoBehaviour
{
    [SerializeField] GameObject ArmExplainPanelGroup;
    ScriptableItemObjectControl scriptableItemObjectControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scriptableItemObjectControl = FindAnyObjectByType<ScriptableItemObjectControl>();

        for (int i = 0; i < ArmExplainPanelGroup.transform.childCount; i++)
        {
            ArmExplainPanelGroup.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scriptableItemObjectControl.getItemStatInfoString(i);
        }
    }

    // Update is called once per frame.
    void Update()
    {

    }
}
