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
            Debug.Log("���� ���۷���Ʈ �� ����ó�� ");
            monsterSpawnController.isMonsterSpawnerOff = false;
            playerManager.isPlayerSequenceOn =true;
        }

        else if(playerManager.isPlayerSequenceOff)
        {
            Debug.Log("���� ���۷���Ʈ �� ����ó�� ");
            playerManager.isPlayerSequenceOff =false ;
            monsterSpawnController.isMonsterSpawnerOn =true;
        }
                
       

    }
    
}
