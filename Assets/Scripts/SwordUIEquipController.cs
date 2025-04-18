using UnityEngine;

public class SwordUIEquipController : MonoBehaviour
{
    [SerializeField]
    GameObject swordUIPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SwordUIOn()
    {
        swordUIPanel.SetActive(true);
    }

    public void SwordUIOff()
    {
        swordUIPanel.SetActive(false);
    }

   


}
