using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public GameObject bossToPlayerParticle;
    private GameObject tmpParticleObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("공격 연출 발사");
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            Vector2 damageTextPosition = (Vector2)collision.transform.position + new Vector2(0, 0.5f);
            tmpParticleObject = Instantiate(bossToPlayerParticle, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();
            Destroy(tmpParticleObject, 1f);
        }
    }
}
