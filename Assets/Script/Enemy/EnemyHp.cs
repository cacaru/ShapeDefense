using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] 
    private GameObject target;

    [SerializeField]
    private GameObject hpbar;

    private int name_tag = 0;
    public int NameTag {  get { return name_tag; }  set { name_tag = value; } }

    // 이 객체가 가질 hp -> default 100
    [SerializeField]
    private float hp = 100;
    private float maxhp = 100;
    [SerializeField]
    private float hid_hp = 100;

    public Image BossHpImg;

    public int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        hpbar.transform.position = target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        hpbar.transform.position = Camera.main.WorldToScreenPoint(target.transform.position + new Vector3(0, -0.28f, 0));
        float left = (float)hp / maxhp;
        hpbar.GetComponent<Slider>().value = left;
    }

    public void SetAllHP(int hp, int maxhp, int hid_hp) {
        this.hp = hp;
        this.maxhp = maxhp;
        this.hid_hp = hid_hp;
    }

    public float Hp { 
        get { return hp; } 
        set { 
            hp = value;
            // boss라면
            if (id > 100) {
                BossHpImg.fillAmount = (float)hp / maxhp ;
            }
        } 
    }
    public float Maxhp { get {  return maxhp; } set {  maxhp = value; } }
    public float Hid_HP { get { return hid_hp; } set { hid_hp = value; } }
}
