using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] 
    private GameObject target;

    [SerializeField]
    private GameObject hpbar;

    private Camera camera;

    // 이 객체가 가질 hp -> default 100
    [SerializeField]
    private int hp = 100;
    private int maxhp = 100;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        this.transform.position = target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = camera.WorldToScreenPoint(target.transform.position + new Vector3(0, -0.28f, 0));
        float left = (float)hp / maxhp;
        hpbar.GetComponent<Slider>().value = left;
    }

    public int Hp { get { return hp; } set { hp = value; } }
    public int Maxhp { get {  return maxhp; } set {  maxhp = value; } }
}
