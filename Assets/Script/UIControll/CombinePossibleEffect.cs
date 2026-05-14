using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.PublicData;

/// <summary>
/// 조합 가능 이펙트 설정 함수
/// </summary>
public class CombinePossibleEffect : MonoBehaviour
{

    private readonly Color DEFAULT = new(0, 0, 0, 0f);
    private Color ON;

    public void OnImageSetting(bool val, string grade) {
        ON = grade switch {
            "E" => color_e,
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            "IC" => core_color,
            "IB" => unicore_color,
            "IA" => crystal_color,
            _ => core_color
        };

        // 이미지 alpha on
        if (val) {
            gameObject.GetComponent<Image>().color = ON;
        }
        else {
            gameObject.GetComponent<Image>().color = DEFAULT;
        }

    }

}
