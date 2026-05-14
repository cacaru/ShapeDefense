using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefabs;
    [SerializeField] private int counter;

    [SerializeField] private DieEffectPool dieeffectpool;

    private readonly Queue<GameObject> bulletqueue = new();

    void OnEnable() {
        Initialize(100);
    }

    private GameObject CreateBullet() {
        var bullet = Instantiate(BulletPrefabs, transform);
        // 부모 설정은 bullet을 받을 때 설정
        bullet.SetActive(false);
        counter++;
        return bullet;
    }

    private void Initialize(int count) {
        for (int i = 0; i < count; i++) {
            bulletqueue.Enqueue(CreateBullet());
        }
    }

    /// <summary>
    /// 외부에서 pool에 들어있는 오브젝트에 접근
    /// </summary>
    /// <returns>bullet 오브젝트</returns>
    public GameObject GetObject(GameObject parent_field, GameObject target, Damage damage) {
        GameObject bullet;
        if (bulletqueue.Count > 0) {
            bullet = bulletqueue.Dequeue();   
        }
        else {
            bullet = CreateBullet();
        }

        bullet.transform.SetParent(parent_field.transform, false);
        bullet.GetComponent<BulletMove>().SetPos(parent_field.transform.position);
        bullet.GetComponent<BulletMove>().damage = damage;
        bullet.GetComponent<BulletMove>().SetPool(this, dieeffectpool);
        bullet.SetActive(true);
        bullet.GetComponent<BulletMove>().Target = target;
        return bullet;
    }

    /// <summary>
    /// 사용 완료된 오브젝트 반환
    /// </summary>
    /// <param name="bullet"></param>
    public void BackObject(GameObject bullet) {
        bullet.transform.SetParent(gameObject.transform, false);
        bullet.GetComponent<BulletMove>().InitMove();
        bullet.SetActive(false);
        bulletqueue.Enqueue(bullet);

        // bulletqueue의 사이즈가 512가 넘어가면 리셋
        if(bulletqueue.Count >= 512) {
            Resizing();
        }
    }

    /// <summary>
    /// 탄알의 갯수 재설정
    /// </summary>
    public void Resizing() {
        bulletqueue.Clear();
        Initialize(100);
    }
}
