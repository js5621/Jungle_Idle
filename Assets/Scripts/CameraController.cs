using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    float CameraZ;
    float CameraSpeed = 0.5f;

    Vector2 bossPostion;
    public Vector3 CameraSdVel = Vector3.zero;

    public bool isBossCameraMode;
    public bool isBossCameraSequenceStart;

    BossBattleSquenceController bossBattleSquenceController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossBattleSquenceController = FindAnyObjectByType<BossBattleSquenceController>();

    }

    // Update is called once per frame
    async void LateUpdate()
    {
        if (isBossCameraMode)// 보스 좀 비춰주세요 
        {
            await BossCameraMode();
        }

        else
        {

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -10), ref CameraSdVel, CameraSpeed * Time.deltaTime);

        }
    }

    async UniTask BossCameraMode()
    {
        if (isBossCameraSequenceStart)
        {
            return;
        }
        isBossCameraSequenceStart = true;
        bossPostion = bossBattleSquenceController.GetBossPosition();
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(bossPostion.x, bossPostion.y, -10), ref CameraSdVel, CameraSpeed * Time.deltaTime);
        transform.DOMove(new Vector3(bossPostion.x,bossPostion.y,-10),0.7f);
        await UniTask.Delay(1000);
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -10), ref CameraSdVel, CameraSpeed * Time.deltaTime);
        transform.DOMove(new Vector3(Player.transform.position.x, Player.transform.position.y, -10), 0.7f);
        await UniTask.Delay(1000);
        bossBattleSquenceController.SequeneceCameraToPlayer();
        isBossCameraSequenceStart = false;

    }
}
