using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffectPool : MonoBehaviour
{
    [SerializeField] private GameObject EnemyEffectParent;
    [SerializeField] private GameObject BulletEffectParent;
    [SerializeField] private GameObject DieEffect;
    [SerializeField] private GameObject BulletDieEffect;

    private readonly Queue<GameObject> e_queue = new();
    private readonly Queue<GameObject> e_back_wait_queue = new();

    private readonly Queue<GameObject> b_queue = new();
    private readonly Queue<GameObject> b_back_wait_queue = new();

    private Quaternion rotate;

    private void Update() {
        // enemy die effect 
        if (e_back_wait_queue.Count > 0) {
            var obj = e_back_wait_queue.Dequeue();
            obj.transform.position = EnemyEffectParent.transform.position;
            obj.tag = "On";
            e_queue.Enqueue(obj);
            //Debug.Log("update_back_queue");
            obj.SetActive(false);
        }
        // bullet die effect
        if (b_back_wait_queue.Count > 0) {
            var obj = b_back_wait_queue.Dequeue();
            obj.transform.position = BulletEffectParent.transform.position;
            obj.tag = "On";
            b_queue.Enqueue(obj);
            //Debug.Log("update_back_queue");
            obj.SetActive(false);
        }

        // die effect가 종료된 후 꺼져 있는 것을 찾으면 queue로 돌리고 끄기
        var off_effect = GameObject.FindGameObjectsWithTag("Wait");
        if( off_effect.Length > 0 ) {
            int size = off_effect.Length;
            for(int i = 0; i < size; i++) {
                off_effect[i].tag = "On";
                off_effect[i].SetActive(false);
                // die 구분
                if (off_effect[i].name.Contains("DieEffect")) {
                    off_effect[i].transform.position = EnemyEffectParent.transform.position;
                    e_queue.Enqueue(off_effect[i]);
                }
                else {
                    off_effect[i].transform.position = BulletEffectParent.transform.position;
                    b_queue.Enqueue(off_effect[i]);
                }
            }
        }


        // on 상태지만 종료되지 않는 오브젝트를 찾아서 종료시켜 사용할 수 있게 변경
        var effects = GameObject.FindGameObjectsWithTag("On");
        if (effects.Length > 0) {
            int size = effects.Length;
            for (int j = 0; j < size; j++) {
                if (effects[j].GetComponent<ParticleSystem>().isStopped) {
                    effects[j].SetActive(false);
                    // die 구분
                    if (effects[j].name.Contains("DieEffect")) {
                        effects[j].transform.position = EnemyEffectParent.transform.position;
                        e_queue.Enqueue(effects[j]);
                    }
                    else {
                        effects[j].transform.position = BulletEffectParent.transform.position;
                        b_queue.Enqueue(effects[j]);
                    }
                }
            }
        }
    }

    private void Start() {
        rotate = Quaternion.Euler(0, 180, 0);
        Initialize(10);
        InitBulletEffect(100);
    }
    

    private GameObject CreateEffect() {
        var effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
        effect.transform.SetParent(EnemyEffectParent.transform);
        effect.transform.localRotation = rotate;
        // 부모 설정은 effect를 받는 곳에서 설정
        effect.SetActive(false);

        return effect;
    }

    private GameObject CreateBulletEffect() {
        var effect = Instantiate(BulletDieEffect, transform.position, Quaternion.identity);
        effect.transform.SetParent(BulletEffectParent.transform);
        //effect.transform.localRotation = rotate;
        // 부모 설정은 bullet을 받을 때 설정
        effect.SetActive(false);

        return effect;
    }

    private void Initialize(int count) {
        for (int i = 0; i < count; i++) {
            e_queue.Enqueue(CreateEffect());
        }
    }

    private void InitBulletEffect(int count) {
        for(int i = 0; i < count; i++) {
            b_queue.Enqueue(CreateBulletEffect());
        }
    }

    public void BulletEffectActive(Vector3 pos) {
        GameObject effect;
        if(b_queue.Count > 0) {
            effect = b_queue.Dequeue();
        }
        else {
            effect = CreateBulletEffect();
        }
        effect.transform.position = pos;
        effect.SetActive(true);
    }

    public void BackBulletEffect(GameObject effect) {
        b_back_wait_queue.Enqueue(effect);
    }

    public GameObject GetEffect(Vector3 pos) {
        GameObject effect;
        if(e_queue.Count > 0) {
            effect = e_queue.Dequeue();
        }
        else {
            effect = CreateEffect();
        }

        effect.transform.position = pos;

        effect.SetActive(true);

        return effect;
    }

    public void BackEffect(GameObject effect) {
        e_back_wait_queue.Enqueue(effect);
    }
}
