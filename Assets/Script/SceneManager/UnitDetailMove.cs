using UnityEngine;
using UnityEngine.EventSystems;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 유닛 상세 보기 페이지로 이동시키는 함수
/// </summary>
/// 
public class UnitDetailMove : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        
        if (now != null) {
            string name = now.name;
            if ( name.Contains("_") ) {
                datahub.UnitidWithPage = int.Parse(name.Split("_")[0]);
                
                if ( datahub.UnitidWithPage > 1000 && datahub.UnitidWithPage < 6999) {
                    // 유닛 디테일 표시 페이지로 이동
                    //SceneManager.LoadScene("UnitDetailScene");
                    // 유닛 디테일 창 설정
                    UnitDetailPageSetting.Instance.SettingPage();

                    // animate activate
                    UnitDetailPageSetting.Instance.gameObject.GetComponent<Animator>().SetBool(ANI_ACTIVATE, true);
                    gameObject.GetComponent<Animator>().SetBool("On", true);
                }
            }

        }
    }
}
