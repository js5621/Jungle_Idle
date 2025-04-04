using Cysharp.Threading.Tasks;
using UnityEngine;

public class FiedMonsterController : MonoBehaviour
{
    bool isDamgeSquenceOn;
    bool isDamgeSquenceOff;
    Animator monsterAnimator;
    int monsterHp = 3;
    FieldStandardBattleController fieldStandardBattleController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        //fieldStandardBattleController = GetComponent<FieldStandardBattleController>();
        //fieldStandardBattleController.MonsterConnect(this.gameObject);
    }

    // Update is called once per frame
    async void Update()
    {


    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌체 판정 : " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("PlayerAttack"))
        {
            Debug.Log("피격 판정");
            monsterAnimator.SetTrigger("Damage");
        }
    }
}

    //async UniTask GotDamage()
    //{
    //    isDamgeSquenceOn = false;
    //    monsterHp -= 1;
    //    if (monsterHp <= 0)
    //    {
    //        await UniTask.Delay(100);
    //        monsterAnimator.SetTrigger("Die");
    //        await UniTask.Delay(500);
    //        Destroy(this);
    //    }

    //    else
    //    {
    //        monsterAnimator.SetTrigger("Damage");
    //        await UniTask.Delay(500);
    //        isDamgeSquenceOff = true;
    //    }




    //}

