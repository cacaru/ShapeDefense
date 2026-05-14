using UnityEngine;

/// <summary>
/// 위 유닛 화면 클릭 이벤트
/// </summary>
public class UnitClick : MonoBehaviour 
{
    [SerializeField] private GameObject Observer;

    public void ClickReturn() {        
        int cur_id = gameObject.GetComponent<Field>().UnitId;
        int waiting_pos = int.Parse(gameObject.name.Split("_")[1]);
//Debug.Log(cur_id + " // " + waiting_pos);
        Observer.GetComponent<UnitClickObserver>().UnitCombineFieldSetting(cur_id, waiting_pos, transform.position);
    }
}
