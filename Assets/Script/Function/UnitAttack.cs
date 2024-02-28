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
    
    // ���� �ӵ��� ������ �޾ƿ� ����
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

    // ���� ������Ʈ
    private GameObject bullet;
    public int Id { 
        set { 
            // ���̵� ���� ���Ӱ� ���ݷ��� �޾ƿ�
            this_id = value;
            if (this_id > 0) {
                var tmp = (Unit)datahub.Unit[this_id];
                damage = tmp.Attack;
                // �������� ���� ��ȭ���� ���� ��ȭ �߰�
                if (this_id >= 1 && this_id <= 3) {
                    // �븻 ��ȭ���� ������ ����
                    damage += datahub.UpgradeNoValue * 2;
                }
                else if (this_id >= 5 && this_id <= 13) {
                    // ��� ��ȭ���� ������ ����
                    damage += datahub.UpgradeAdValue * 3;
                }
                else if (this_id >= 14 && this_id <= 23) {
                    // ��ǰ ��ȭ���� ������ ����
                    damage += datahub.UpgradeLuValue * 4;
                }
                else if (this_id >= 24 && this_id <= 35) {
                    // ����ǰ ��ȭ���� ������ ����
                    damage += datahub.UpgradeLiValue * 5;
                }
                else if (this_id >= 36 && this_id <= 39) {
                    // Ŀ���� ��ȭ���� ������ ����
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
        // ���� ������Ʈ ����
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ�� ã��
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (targets.Count > 0) {
            // ���� ���� ������ �����ϱ� -- ���� ������ ����
            target = targets[0];
            has_target = true;
        }
        else {
            has_target = false;
        }
        // ���� ����
        if ( active ) {   
            // Ÿ���� ������ ����
            if ( has_target) {
                // ���� ������Ʈ ����
                // ���ݼӵ��� ���� ���� 
                if (!already_run) {
                    already_run = true;
                    StartCoroutine(Shot());
                }
                    
            }
            // Ÿ�� ������ ���� ����
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
