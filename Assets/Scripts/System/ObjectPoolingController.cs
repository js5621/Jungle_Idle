using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolingController : MonoBehaviour
{
    public static ObjectPoolingController Instance;
    [SerializeField]
    private GameObject poolingObjectMonsterPrefab;
    private Queue<FiedMonsterController> monstePoolingQueue = new Queue<FiedMonsterController>();
    private void Awake()
    {
        Instance = this;
        Initalize(10);
    }

    private FiedMonsterController CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectMonsterPrefab, transform).GetComponent<FiedMonsterController>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    public static FiedMonsterController GetMonsterObject()
    {
        if (Instance.monstePoolingQueue.Count > 0)
        {
            var obj = Instance.monstePoolingQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }

        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;

        }
    }
    private void Initalize(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            monstePoolingQueue.Enqueue(CreateNewObject());
        }
    }

    public static void ReturnObject(FiedMonsterController monsterObj)
    {
        monsterObj.gameObject.SetActive(false);
        monsterObj.transform.SetParent(Instance.transform);
        Instance.monstePoolingQueue.Enqueue(monsterObj);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
