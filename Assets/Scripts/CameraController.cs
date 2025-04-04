using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    Vector3 cameraPosition = new Vector3(0, 0, -10);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.DOMove(Player.transform.position + cameraPosition, 1f);
    }
}
