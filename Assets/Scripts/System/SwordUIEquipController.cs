using UnityEngine;

public class SwordUIEquipController : MonoBehaviour
{
    [SerializeField] GameObject swordUIPanel;
    public void SwordUIOn()
    {
        swordUIPanel.SetActive(true);
    }
    public void SwordUIOff()
    {
        swordUIPanel.SetActive(false);
    }
}
