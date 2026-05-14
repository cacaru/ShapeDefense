using System.Collections;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private GameObject DieEffect;

    private Color alpha_0 = new(0, 0, 0, 0);

    private DieEffectPool die_effect_pool;
    private EnemyPool enemy_pool;

    public void SetPool(DieEffectPool die_effect_pool, EnemyPool enemy_pool) {
        this.die_effect_pool = die_effect_pool;
        this.enemy_pool = enemy_pool;
    }

    public void Die() {
        gameObject.tag = "Dead";
        // 불필요한 오브젝트 cancle
        GetComponent<EnemyMove>().speed = 0;
        transform.Find("Canvas").gameObject.SetActive(false);

        //하위 이펙트 종료
        transform.Find("Poison").gameObject.SetActive(false);
        transform.Find("Blust").gameObject.SetActive(false);

        // 이펙트 켜기
        var effect = die_effect_pool.GetEffect(gameObject.transform.position);
        
        // Enemy Sprite alpha를 0으로 조절
        gameObject.GetComponent<SpriteRenderer>().color = alpha_0;

        StartCoroutine(DestroyObject(effect));
    }

    
    IEnumerator DestroyObject(GameObject effect) {
        yield return wfs_1_5;
        // 이펙트 끄기
        effect.tag = "Wait";
        die_effect_pool.BackEffect(effect);
        //Destroy(gameObject);

        // enemy 끄기
        enemy_pool.BackEnemy(gameObject);
    }
    
}
