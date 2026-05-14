using UnityEngine;
using UnityEngine.EventSystems;


public class UserUpgradePanelControll : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject Panel;

    // 다른곳을 클릭하면 패널이 꺼지도록 설정 
    public void OnPointerClick(PointerEventData eventData) {
        //-> 클릭된 오브젝트가 Out 이면 panel down
        var target = eventData.pointerCurrentRaycast.gameObject.name;

        if (target.Equals("Out")) {
            PageControll.Instance.PanelDown("stat");
        }
    }

}
