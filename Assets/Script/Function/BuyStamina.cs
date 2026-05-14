using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
public class BuyStamina : MonoBehaviour 
{ 
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private GameObject StaminaBuyField;


    public void BuyStamianClick() {
        string val = InputField.GetComponent<TMP_InputField>().text;
        int value = int.Parse(val) * 50;

        string announce = UtilityHub.query_builder.Append("구매를 원하신 스태미나\n")
                                                  .Append(val)
                                                  .Append("\n\n필요한 골드\n")
                                                  .Append(value)
                                                  .ToString();
        UtilityHub.query_builder.Clear();
        // 구매 안내창 띄우기
        StaminaBuyField.GetComponent<BuyStaminaData>().StaminaValue = int.Parse(val);
        StaminaBuyField.transform.Find("Text").GetComponent<TMP_Text>().text = announce;
        StaminaBuyField.SetActive(true);
        
    }
}
