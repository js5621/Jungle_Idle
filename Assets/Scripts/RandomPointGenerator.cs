using UnityEngine;

public class RandomPointGenerator : MonoBehaviour
{
    public Vector3[] RandomVectorArray;
    int RandomArraySize = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomVectorArray = new Vector3[RandomArraySize];
        for (int i = 0; i < RandomArraySize; i++)
        {
            RandomVectorArray[i] = Random.insideUnitCircle * 10;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
