using UnityEngine;

public class FieldGameOperator : MonoBehaviour
{
    PlayerManager playerManager;    
    MonsterSpawnController monsterSpawnController;
    public bool isOperateSequnce = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        monsterSpawnController= FindAnyObjectByType<MonsterSpawnController>();
        monsterSpawnController.isMonsterSpawnerOn = true;
    }

    // Update is called once per frame
    void Update()
    {   
       
        
    }

    public void OperateProcess()
    {
        
        if(monsterSpawnController.isMonsterSpawnerOff)
        {
            Debug.Log("스폰 오퍼레이트 후 진행처리 ");
            monsterSpawnController.isMonsterSpawnerOff = false;
            playerManager.isPlayerSequenceOn =true;
        }

        else if(playerManager.isPlayerSequenceOff)
        {
            Debug.Log("스폰 오퍼레이트 후 진행처리 ");
            playerManager.isPlayerSequenceOff =false ;
            monsterSpawnController.isMonsterSpawnerOn =true;
        }
                
       

    }
    
}
