using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    private RandomPointGenerator randomPointGenerator;
    private FieldGameOperator fieldGameOperator;
    PlayerManager playerManager;
    private BossGenerateController bossController;
    public GameObject spawnGoblinPrefab;
    public GameObject spawnSkeletonPrefab;

    public bool isMonsterSpawnerOn = false;
    public bool isMonsterSpawnerOff=false;
    public bool isSpawning =false;
    Vector3 randomVector;
    public GameObject[] monsterSpawnObject;

    void Start()
    {
        randomPointGenerator = FindAnyObjectByType<RandomPointGenerator>();
        fieldGameOperator = FindAnyObjectByType<FieldGameOperator>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        bossController = FindAnyObjectByType<BossGenerateController>();

    }

    // Update is called once per frame
    async void Update()
    {
        if(!bossController.isBossTime)
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

        Vector2 monsterSpawnPoint = Vector2.zero + Random.insideUnitCircle*10;
        isSpawning =true;
        Instantiate(monsterSpawnObject[Random.Range(0,2)], monsterSpawnPoint,Quaternion.identity);
        await UniTask.Delay(1700);

        isSpawning=false;
        
       
        
        
       
        

    }
}
