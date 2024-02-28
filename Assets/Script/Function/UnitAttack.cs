using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// unit attack function
/// default : false
/// active : attack per attack speed from unit data
/// </summary>
public class UnitAttack : MonoBehaviour
{
    private DataHub datahub;
    
    // 공격 속도등 정보를 받아올 변수
    private int this_id = 0;
    private int damage;
    private int attack_speed;
    [SerializeField]
    private bool active = false;
    [SerializeField]
    private bool has_target = false;
    private bool already_run = false;

    private GameObject target;
    private List<GameObject> targets;

    // 공격 오브젝트
    private GameObject bullet;
    public int Id { 
        set { 
            // 아이디에 따라 공속과 공격력을 받아옴
            this_id = value;
            if (this_id > 0) {
                var tmp = (Unit)datahub.Unit[this_id];
                damage = tmp.Attack;
                // 스테이지 한정 강화도에 따른 강화 추가
                if (this_id >= 1 && this_id <= 3) {
                    // 노말 강화도에 영향을 받음
                    damage += datahub.UpgradeNoValue * 2;
                }
                else if (this_id >= 5 && this_id <= 13) {
                    // 고급 강화도에 영향을 받음
                    damage += datahub.UpgradeAdValue * 3;
                }
                else if (this_id >= 14 && this_id <= 23) {
                    // 명품 강화도에 영향을 받음
                    damage += datahub.UpgradeLuValue * 4;
                }
                else if (this_id >= 24 && this_id <= 35) {
                    // 한정품 강화도에 영향을 받음
                    damage += datahub.UpgradeLiValue * 5;
                }
                else if (this_id >= 36 && this_id <= 39) {
                    // 커스텀 강화도에 영향을 받음
                    damage += datahub.UpgradeCuValue * 10;
                }

                attack_speed = tmp.AttackSpeed;

                if (damage > 0 && attack_speed > 0) {
                    active = true;
                }
            }
            else {
                //do nothing
                active = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        // 공격 오브젝트 생성
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // 타깃 찾기
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (targets.Count > 0) {
            // 가장 앞의 적에게 공격하기 -- 범위 설정은 추후
            target = targets[0];
            has_target = true;
        }
        else {
            has_target = false;
        }
        // 공격 개시
        if ( active ) {   
            // 타깃이 있으면 공격
            if ( has_target) {
                // 공격 오브젝트 생성
                // 공격속도에 따라 생성 
                if (!already_run) {
                    already_run = true;
                    StartCoroutine(Shot());
                }
                    
            }
            // 타깃 없으면 공격 중지
            else {
                StopCoroutine(Shot());
//Debug.Log("no target");
                already_run = false;
            }
        }
    }

    IEnumerator Shot() {
        while (active && has_target) {
            GameObject tmp = Instantiate(bullet, transform.position, transform.rotation);
            tmp.transform.SetParent(transform, true);
            tmp.GetComponent<BulletMove>().target = target;
            tmp.GetComponent<BulletMove>().damage = damage;
            ImageSetting(this_id, tmp);
            yield return new WaitForSeconds(2f / attack_speed);
        }
    }    

    public void StopShot() {
        StopCoroutine(Shot());
    }

    private void ImageSetting(int id, GameObject bullet) {
        string path = id switch {
            1 => "Sprite/Bullet/normal_circle_bullet",
            2 => "Sprite/Bullet/normal_triangle_bullet",
            3 => "Sprite/Bullet/normal_square_bullet",
            4 => "Sprite/Unit/normal_star",
            5 => "Sprite/Bullet/bullet_ad_circle_1",
            6 => "Sprite/Bullet/bullet_ad_circle_2",
            7 => "Sprite/Bullet/bullet_ad_circle_3",
            8 => "Sprite/Bullet/bullet_ad_triangle_1",
            9 => "Sprite/Bullet/bullet_ad_triangle_2",
            10 => "Sprite/Bullet/bullet_ad_triangle_3",
            11 => "Sprite/Bullet/bullet_ad_square_1",
            12 => "Sprite/Bullet/bullet_ad_square_2",
            13 => "Sprite/Bullet/bullet_ad_square_3",
            14 => "Sprite/Bullet/bullet_lu_circle_1",
            15 => "Sprite/Bullet/bullet_lu_circle_2",
            16 => "Sprite/Bullet/bullet_lu_circle_",
            17 => "Sprite/Bullet/bullet_lu_triangle_1",
            18 => "Sprite/Bullet/bullet_lu_triangle_2",
            19 => "Sprite/Bullet/bullet_lu_triangle_3",
            20 => "Sprite/Bullet/bullet_lu_square_1",
            21 => "Sprite/Bullet/bullet_lu_square_2",
            22 => "Sprite/Bullet/bullet_lu_square_3",
            23 => "Sprite/Bullet/bullet_lu_star",
            24 => "Sprite/Bullet/bullet_li_circle_1",
            25 => "Sprite/Bullet/bullet_li_circle_2",
            26 => "Sprite/Bullet/bullet_li_circle_3",
            27 => "Sprite/Bullet/bullet_li_triangle_1",
            28 => "Sprite/Bullet/bullet_li_triangle_2",
            29 => "Sprite/Bullet/bullet_li_triangle_3",
            30 => "Sprite/Bullet/bullet_li_square_1",
            31 => "Sprite/Bullet/bullet_li_square_2",
            32 => "Sprite/Bullet/bullet_li_square_3",
            33 => "Sprite/Bullet/bullet_li_star_1",
            34 => "Sprite/Bullet/bullet_li_star_2",
            35 => "Sprite/Bullet/bullet_li_star_3",
            36 => "Sprite/Bullet/normal_circle_bullet",
            37 => "Sprite/Bullet/normal_triangle_bullet",
            38 => "Sprite/Bullet/normal_square_bullet",
            39 => "Sprite/Bullet/lu_star",
            _ => ""
        };

        bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        bullet.GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
    }
    
}
