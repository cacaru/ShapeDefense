using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
public class NickNameSetting : MonoBehaviour
{
    [SerializeField] private TMP_Text Nick_Name;
    [SerializeField] private GameObject Change_Field;
    [SerializeField] private TMP_InputField Nick_Name_Input;

    [SerializeField] private TMP_Text Need_Gold_Text;

    [SerializeField] private TMP_Text Change_Field_Now_NickName_Text;
    [SerializeField] private TMP_Text Modal_Change_NickName_Text;

    [SerializeField] private GameObject Announce_Modal;
    [SerializeField] private TMP_Text Announce_Text;
    [SerializeField] private GameObject Check_Modal;

    private string query;

    private readonly Regex nick_checker_regex = new Regex(@"[^a-zA-Z0-9§°-§æ∞°-∆R]+");
    private readonly string STRING_UNCHECK = "«—±€, øµπÆ, º˝¿⁄∏∏\r\n¿‘∑¬ ∞°¥…«’¥œ¥Ÿ.";
    private readonly string STRING_LENGTH = "12¿⁄ ¿Ã«œ∑Œ∏∏ º≥¡§ ∞°¥…«’¥œ¥Ÿ.";
    private readonly string STRING_GOLDLESS = "∞ÒµÂ∞° ∫Œ¡∑«’¥œ¥Ÿ.";


    private void ShowNickName() {
        Nick_Name.text = datahub.User.Nickname;
        // headerµµ √ﬂ∞° ºº∆√
        UtilityHub.PageHeaderSetting();
    }

    /// <summary>
    /// ∏ﬁ¿Œ ∆‰¿Ã¡ˆ¿« ∫Ø∞Ê«œ±‚
    /// </summary>
    public void ChangeNickNameClick() {
        // «ˆ¿Á ¿Ã∏ß πﬁæ∆ø¿±‚
        Change_Field_Now_NickName_Text.text = datahub.User.Nickname;

        // « ø‰ ∞ÒµÂ ∫–ºÆ
        int need_gold = datahub.User.ChangeNickNameRecode * 500;
        //Debug.Log("need-Gold  > " + need_gold);
        Need_Gold_Text.text = need_gold.ToString();

        Change_Field.SetActive(true);
    }

    /// <summary>
    /// ∫Ø∞Ê»Æ¿Œ æ»≥ª√¢ ∂ÁøÏ±‚
    /// </summary>
    public void CheckNickModal() {
        // ¿Ã∏ß ∞ÀªÁ
        // ¥–≥◊¿” ∞ÀªÁ
        // ∆ØºˆπÆ¿⁄ æ»µ«∞Ì øµπÆ, «—±€, º˝¿⁄∏∏, 12±€¿⁄ ¿Ã≥ª∑Œ ∞ÀªÁ
        var check_nick_result = nick_checker_regex.IsMatch(Nick_Name_Input.text);
        //Debug.Log("check_nick_isnt_possible > " + check_nick_result);
        if (check_nick_result) {
           // Debug.Log("Dont Match");
            // announce
            Announce_Text.text = STRING_UNCHECK;
            Announce_Modal.SetActive(true);
            return;
        }
        //Debug.Log("nickname string length > " + Nick_Name_Input.text.Length);
        // ¿Ã∏ß ±Ê¿Ã ∞ÀªÁ
        if (Nick_Name_Input.text.Length > 12 || Nick_Name_Input.text.Length <= 0) {
           // Debug.Log("±Ê¿Ã √ ∞˙");
            Announce_Text.text = STRING_LENGTH;
            Announce_Modal.SetActive(true);
            return;
        }

        // ¿Ã∏ß º≥¡§
        Modal_Change_NickName_Text.text = Nick_Name_Input.text;
        Check_Modal.SetActive(true);
    }

    /// <summary>
    /// ∫Ø∞Êµ» ¿Ã∏ß º≥¡§
    /// </summary>
    public void ChangeNickName() {
        bool use_gold = false;
        // ¡∂∞« º≥¡§ 
        // √÷√  1»∏ π´∑·
        // ¿Ã»ƒ ∫Ø∞ÊΩ√∏∂¥Ÿ 500 ∞ÒµÂ « ø‰
        // π´∑·
        if(datahub.User.ChangeNickNameRecode != 0 && datahub.User.Dot < 500) {
            // announce
            // ∞ÒµÂ ∫Œ¡∑ æ»≥ª
            Announce_Text.text = STRING_GOLDLESS;
            Announce_Modal.SetActive(true);
            return;
        }
        else if(datahub.User.ChangeNickNameRecode != 0 && datahub.User.Dot >= 500) {
            use_gold = true;
        }

        // ≈Î∞˙µ«æ˙¿∏∏È ∫Ø∞Ê
        if(use_gold) {
            datahub.User.Dot -= 500;  
        }
        datahub.User.Nickname = Nick_Name_Input.text;
        datahub.User.ChangeNickNameRecode += 1;
        
        // dot, nickname, changeNickNameRecode æ˜∑ŒµÂ
        query = UtilityHub.query_builder.Append("UPDATE user SET dot=")
                                        .Append(datahub.User.Dot)
                                        .Append(", nickname='")
                                        .Append(datahub.User.Nickname)
                                        .Append("', nickname_change_recode=")
                                        .Append(datahub.User.ChangeNickNameRecode)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");

        /*
        query = UtilityHub.query_builder.Append("UPDATE user SET nickname='")
                                        .Append(datahub.User.Nickname)
                                        .Append("'")
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");

        query = UtilityHub.query_builder.Append("UPDATE user SET nickname_change_recode=")
                                        .Append(datahub.User.ChangeNickNameRecode)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        //Debug.Log(query);
        modifyDB.ControllDB(query, "user");
        */

        Check_Modal.SetActive(false);
        Change_Field.SetActive(false);
        ShowNickName();
    }

    /// <summary>
    /// √÷¡æ »Æ¿Œø°º≠ √Îº“ -> modal∏∏ ¥›±‚
    /// </summary>
    public void CheckCancel() {
        Check_Modal.SetActive(false);
    }

    /// <summary>
    /// ∫“∞° æ»≥ª√¢ ¥›±‚
    /// </summary>
    public void AnnounceCancel() {
        Announce_Modal.SetActive(false);
    }
    
    /// <summary>
    /// ¿Ã∏ß ∫Ø∞Ê √Îº“«œ±‚
    /// </summary>
    public void ChangeCancel() {
        Check_Modal.SetActive(false);
        Nick_Name_Input.text = "";
        Change_Field.SetActive(false);
    }
}
