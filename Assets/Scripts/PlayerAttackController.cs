using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject monsterAtkParticlePrefab;
    public GameObject bossAtkParticlePrefab;
    public GameObject attackDamageTmPro;
    public GameUIController gameUIController;
    private GameObject tmpParticleObject;
    private GameObject tmpTmProPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            Debug.Log("공격 연출 발사");
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
            Debug.Log("공격 연출 발사");
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position + new Vector2(0, 0.5f);

            tmpParticleObject = Instantiate(bossAtkParticlePrefab, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            tmpTmProPrefab = Instantiate(attackDamageTmPro, damageTextPosition + new Vector2(0, 2f), Quaternion.identity);
            gameUIController.BossHPUIDamgage();
            Destroy(tmpParticleObject, 1f);
            Destroy(tmpTmProPrefab, 1f);

        }
    }
}
