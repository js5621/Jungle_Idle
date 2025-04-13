using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    GameUIController gameUIController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameUIController.isTimeOver())
        {
            Time.timeScale = 0f;
            Debug.Log("���� ����");
        }

        else
        {

            if (gameUIController.isBossHpOver())
            {
                Time.timeScale = 0f;
                Debug.Log("�����¸�");
            }
        }
    }
}
