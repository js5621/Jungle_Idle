using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    private RandomPointGenerator randomPointGenerator;
    PlayerManager playerManager;
    private BossGenerateController bossController;
    GameFlowController gameFlowController;
    public bool isSpawning = false;
    
    Vector3 randomVector;
    
    public Queue<GameObject> spawnedMonsterQueue;

    public GameObject[] monsterSpawnObject;
    
    
    void Start()
    {
        randomPointGenerator = FindAnyObjectByType<RandomPointGenerator>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        bossController = FindAnyObjectByType<BossGenerateController>();
        gameFlowController = FindAnyObjectByType<GameFlowController>();
        spawnedMonsterQueue = new Queue<GameObject>();
    }
    async void Update()
    {
        if (!bossController.isBossTime && gameFlowController.gameState == GameFlowState.Field)
        {
            await MonsterSpawn();
        }
    }
    async UniTask MonsterSpawn()
    {
        int waitTime = 500;
        
        if (isSpawning)
        {
            return;
        }
        
        Vector2 monsterSpawnPoint = Vector2.zero + Random.insideUnitCircle * 20;
        isSpawning = true;
        spawnedMonsterQueue.Enqueue(Instantiate(monsterSpawnObject[Random.Range(0, 2)], monsterSpawnPoint, Quaternion.identity));
        await UniTask.Delay(500);

        isSpawning = false;
    }
}
