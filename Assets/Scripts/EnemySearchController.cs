using UnityEngine;

public class EnemySearchController : MonoBehaviour
{
    FiedMonsterController enemyContoller;
    BossMoveController bossController;

   
    public bool isEnemyNull(GameObject enemy)
    {
        if (enemy == null)
        {
            return true;
        }

        else
        {
            return false;
        }

    }
    public GameObject FoundEnemy()
    { 
        
        enemyContoller = FindAnyObjectByType<FiedMonsterController>();
        if (enemyContoller == null)
        {

            bossController = FindAnyObjectByType<BossMoveController>();
            if(bossController == null)
            {
                
                return FindAnyObjectByType<BossMoveController>().gameObject;
            }
            else
            {
                return  bossController.gameObject;
            }
        }
        else
        {
            return enemyContoller.gameObject;
        }
            
        
    }

   

}
