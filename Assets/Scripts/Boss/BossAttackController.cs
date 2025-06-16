using UnityEngine;

public class BossAttackController : MonoBehaviour
{ 
    public GameObject bossToPlayerParticle;
    private GameObject tmpParticleObject;
    /// <summary>
    /// ?? ?? ?? ?? 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Vector2 emissionPosition = (Vector2)collision.transform.position;
            
            tmpParticleObject = Instantiate(bossToPlayerParticle, emissionPosition, Quaternion.identity);
            tmpParticleObject.GetComponent<ParticleSystem>().Play();

            Destroy(tmpParticleObject, 1f);
        }
    }
}
