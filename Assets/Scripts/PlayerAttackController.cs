using TMPro;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject monsterAtkParticlePrefab;
    public GameObject bossAtkParticlePrefab;
    public GameObject attackDamageTmPro;
    
    private GameObject tmpParticleObject;
    private GameObject tmpTmProPrefab;

    private GameUIController gameUIController;
    private SFxController sFxController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
        sFxController = FindAnyObjectByType<SFxController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            Debug.Log("���� ���� �߻�");
            sFxController.Sfxplay(0);
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position + new Vector2(0, 0.5f);

            tmpParticleObject = Instantiate(monsterAtkParticlePrefab, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            tmpTmProPrefab = Instantiate(attackDamageTmPro, damageTextPosition, Quaternion.identity);

            Destroy(tmpParticleObject, 1f);
            Destroy(tmpTmProPrefab, 1f);
        }

        if (collision.tag.Equals("Boss"))
        {
            Debug.Log("���� ���� �߻�");
            sFxController.Sfxplay(0);// Ÿ�� ȿ���� ��� 
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position;

            tmpParticleObject = Instantiate(bossAtkParticlePrefab, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            tmpTmProPrefab = Instantiate(attackDamageTmPro, damageTextPosition + new Vector2(-1, 1f), Quaternion.identity);
            tmpTmProPrefab.GetComponent<TextMeshPro>().autoSizeTextContainer = true;
            tmpTmProPrefab.GetComponent<TextMeshPro>().fontSize = 5f;
            gameUIController.BossHPUIDamgage();
            Destroy(tmpParticleObject, 1f);
            Destroy(tmpTmProPrefab, 1f);

        }
    }
}
