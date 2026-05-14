using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 상자 사는 함수
/// </summary>
public class BuyChest : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject Popup;

    public void OnPointerClick(PointerEventData eventData) {
        if (!eventData.pointerCurrentRaycast.gameObject.name.Contains("_")) {
            return;
        }
        string key = eventData.pointerCurrentRaycast.gameObject.name.Split("_")[1];
   //     Debug.Log(key);

        string piece = "";
        string percent_announce = "";
        int chest_type = 0;
        switch (key) {
            case "no":
                piece = "E급 상자 : 조각 10개 지급";
                percent_announce = "확률\nE 등급 80%\nD 등급 15%\nC 등급 5%";
                Popup.GetComponent<Chest>().e = 80;
                Popup.GetComponent<Chest>().d = 15;
                Popup.GetComponent<Chest>().c = 15;
                break;
            case "ad":
                piece = "D급 상자 : 조각 20개 지급";
                percent_announce = "확률\nE 등급 55%\nD 등급 25%\nC 등급 15%\nB 등급 5%";
                Popup.GetComponent<Chest>().e = 55;
                Popup.GetComponent<Chest>().d = 25;
                Popup.GetComponent<Chest>().c = 15;
                Popup.GetComponent<Chest>().b = 5;
                break;
            case "lu":
                piece = "C급 상자 : 조각 30개 지급";
                percent_announce = "확률\nE 등급 40%\nD 등급 30%\nC 등급 20%\nB 등급 9%\nA 등급 1%";
                Popup.GetComponent<Chest>().e = 40;
                Popup.GetComponent<Chest>().d = 30;
                Popup.GetComponent<Chest>().c = 20;
                Popup.GetComponent<Chest>().b = 9;
                Popup.GetComponent<Chest>().a = 1;
                break;
            case "li":
                piece = "B급 상자 : 조각 50개 지급";
                percent_announce = "확률\nD 등급 54%\nC 등급 25%\nB 등급 15%\nA 등급 5%\nS 등급 1%";
                Popup.GetComponent<Chest>().d = 54;
                Popup.GetComponent<Chest>().c = 25;
                Popup.GetComponent<Chest>().b = 15;
                Popup.GetComponent<Chest>().a = 5;
                Popup.GetComponent<Chest>().s = 1;
                break;
            case "cu":
                piece = "A급 상자 : 조각 100개 지급";
                percent_announce = "확률\nC 등급 55%\nB 등급 25%\nA 등급 15%\nS 등급 5%";
                Popup.GetComponent<Chest>().c = 55;
                Popup.GetComponent<Chest>().b = 25;
                Popup.GetComponent<Chest>().a = 15;
                Popup.GetComponent<Chest>().s = 5;
                break;
            case "adv":
                piece = "광고 상자 : 조각 30개 지급";
                percent_announce = "확률\nE 등급 40%\nD 등급 30%\nC 등급 20%\nB 등급 9%\nA 등급 1%";
                Popup.GetComponent<Chest>().e = 40;
                Popup.GetComponent<Chest>().d = 30;
                Popup.GetComponent<Chest>().c = 20;
                Popup.GetComponent<Chest>().b = 9;
                Popup.GetComponent<Chest>().a = 1;
                chest_type = 1;
                break;
            case "sta":
                piece = "광고 상자 : 스태미나 8 ~ 16개지급";
                percent_announce = "8 ~ 16개 사이 랜덤 지급\n확률 동일";
                chest_type = 2;
                break;
        }

        // 확인 ui 호출
        Popup.GetComponent<Chest>().Id = key;
        Popup.transform.Find("Item").GetComponent<TMP_Text>().text = piece;
        Popup.transform.Find("Percent").GetComponent<TMP_Text>().text = percent_announce;

        if (chest_type != 0) {
            // 광고 구매
            //Popup.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => GoogleAdvManager.Instance.ShowAds(chest_type));
            Popup.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => BuyConfirm.Instance.FreeChestCheck(chest_type));
        }
        else {
            // 일반 구매
            Popup.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => BuyConfirm.Instance.ChestBuy());
        }

        Popup.SetActive(true);
    }


}
