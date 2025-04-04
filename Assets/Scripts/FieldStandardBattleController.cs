using UnityEngine;

public class FieldStandardBattleController : MonoBehaviour
{
    GameObject tempConnectedMonster;
    

    public void MonsterConnect(GameObject tempMonSter)
    {
        tempConnectedMonster =tempMonSter;
   
    }

    public void MonsterDisconnect()
    {
        tempConnectedMonster = null;
    }


    
}
