using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static ShapeDefenseSpace.PublicData;

public class AttackAreaControll : SceneSingleton<AttackAreaControll>
{

    [SerializeField] private GameObject AttackArea;

    private Color color;
    private int side;
    private readonly int RATE = 230;
    private Vector3 pos = new(125,125,125);

    public void SettingArea(int cur_id) {
        int tmp_rate = cur_id switch {
            >= 1000 and <= 1999 => 1,
            >= 2000 and <= 2999 => 1,
            >= 3000 and <= 3999 => 2,
            >= 4000 and <= 4999 => 2,
            >= 5000 and <= 5999 => 3,
            >= 6000 and <= 6999 => 3,
            _ => 1
        };
        color = cur_id switch {
            >= 1000 and <= 1999 => color_e,
            >= 2000 and <= 2999 => color_d,
            >= 3000 and <= 3999 => color_c,
            >= 4000 and <= 4999 => color_b,
            >= 5000 and <= 5999 => color_a,
            >= 6000 and <= 6999 => color_s,
            >= 300 and <= 399 => core_color,
            >= 400 and <= 499 => unicore_color,
            >= 500 and <= 599 => crystal_color,
            _ => core_color
        };

        side = RATE * tmp_rate;
        AttackArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, side);
        AttackArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, side);
        color.a = 180 / 255f;
        AttackArea.GetComponent<Image>().color = color;
    }

    public void Show(Vector3 pos, int cur_id) {
        if(pos !=  Vector3.zero) {
            this.pos = pos;
        }
        SettingArea(cur_id);
        AttackArea.transform.position = Camera.main.WorldToScreenPoint(this.pos);
        AttackArea.SetActive(true);
    }

    public void Hide() {
        AttackArea.SetActive(false);
    }

}
