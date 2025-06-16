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
    public void Start()
    {
        gameUIController = FindAnyObjectByType<GameUIController>();
        sFxController = FindAnyObjectByType<SFxController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            sFxController.Sfxplay(0);
            
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position + new Vector2(0, 0.5f);

            tmpParticleObject = Instantiate(monsterAtkParticlePrefab, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            tmpTmProPrefab = Instantiate(attackDamageTmPro, damageTextPosition, Quaternion.identity);

            int playerAtk = transform.parent.GetComponent<PlayerManager>().GetPlayerAtk();
            tmpTmProPrefab.GetComponent<TextMeshPro>().text = MakeUkCheonValue(playerAtk);

            Destroy(tmpParticleObject, 1f);
            Destroy(tmpTmProPrefab, 1f);
        }

        if (collision.tag.Equals("Boss"))
        {
            sFxController.Sfxplay(0);
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position;
            
            tmpParticleObject = Instantiate(bossAtkParticlePrefab, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            tmpTmProPrefab = Instantiate(attackDamageTmPro, damageTextPosition + new Vector2(-1, 1f), Quaternion.identity);

            int playerAtk = transform.parent.GetComponent<PlayerManager>().GetPlayerAtk();
            
            tmpTmProPrefab.GetComponent<TextMeshPro>().autoSizeTextContainer = true;
            tmpTmProPrefab.GetComponent<TextMeshPro>().fontSize = 5f;
            tmpTmProPrefab.GetComponent<TextMeshPro>().text = MakeUkCheonValue(playerAtk);
            
            gameUIController.BossHPUIDamgage(playerAtk);
            Destroy(tmpParticleObject, 1f);
            Destroy(tmpTmProPrefab, 1f);
        }
    }

    string MakeUkCheonValue(int playerAtk)
    {
        int uk = playerAtk / 10;
        int cheon = playerAtk % 10;
        string ukCheon = uk.ToString() + "¾ï";
        
        if (cheon != 0)
        {
            ukCheon += cheon + "000¸¸";
        }
        
        return ukCheon;
    }
}
