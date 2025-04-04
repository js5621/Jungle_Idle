using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    private RandomPointGenerator randomPointGenerator;
    private FieldGameOperator fieldGameOperator;
    PlayerManager playerManager;
    public GameObject spawnGoblinPrefab;
    public GameObject spawnSkeletonPrefab;

    public bool isMonsterSpawnerOn = false;
    public bool isMonsterSpawnerOff=false;
    Vector3 randomVector;
    private GameObject tempSpawnObject;
    void Start()
    {
        randomPointGenerator = FindAnyObjectByType<RandomPointGenerator>();
        fieldGameOperator = FindAnyObjectByType<FieldGameOperator>();
        playerManager = FindAnyObjectByType<PlayerManager>();

    }

    // Update is called once per frame
    async void Update()
    {
        if(isMonsterSpawnerOn)
        {
            await MonsterSpawn();
        }
        
    }

    async UniTask MonsterSpawn()
    {

        Vector2 playerMoveVector;
        isMonsterSpawnerOn =false;
        randomVector = randomPointGenerator.RandomVectorArray[Random.Range(0, randomPointGenerator.RandomVectorArray.Length)];
        await UniTask.Delay(1000);
        tempSpawnObject = Instantiate(spawnSkeletonPrefab, randomVector, Quaternion.identity);
        playerMoveVector = new Vector2(tempSpawnObject.transform.position.x + 1.0f, tempSpawnObject.transform.position.y);
        await UniTask.Delay(500);
        playerManager.destinationVector = playerMoveVector;
        await UniTask.Delay(500);
        isMonsterSpawnerOff = true;
        fieldGameOperator.OperateProcess();

        
       
        
        
       
        

    }
}
