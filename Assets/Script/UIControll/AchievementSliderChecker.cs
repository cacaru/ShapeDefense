using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 업적 량을 받아 slider의 값을 결정하는 함수
/// </summary>
public class AchievementSliderChecker : MonoBehaviour
{
    // slider
    [SerializeField] private GameObject Slider;

    // 해당 업적 달성 필요값
    private int need_val = 0;
    // 현재 값
    private int now_val = 0;

    private float result_val;

    public void ValueSet() {
        Slider.GetComponent<Slider>().value = result_val;
    }

    public int NeedVal { 
        set { 
            need_val = value; 
            if (need_val > 0 && now_val > 0) {
                result_val = (float)now_val / need_val;
            }
            else if(need_val == 0 && now_val == 0) {
                result_val = 0;
            }
            ValueSet();
        } 
    }
    public int NowVal { 
        set { 
            now_val = value;
            if (need_val > 0 && now_val > 0) {
                result_val = (float)now_val / need_val;
            }
            else if (need_val == 0 && now_val == 0) {
                result_val = 0;
            }
            ValueSet();
        } 
    }
}
