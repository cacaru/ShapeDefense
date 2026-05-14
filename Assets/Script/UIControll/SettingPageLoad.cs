using TMPro;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class SettingPageLoad : MonoBehaviour
{
    [SerializeField] private TMP_Text Nick_Name;

    // Start is called before the first frame update
    void Start()
    {
        LoadPage();
    }

    public void LoadPage() {
        // 이름 변경
        Nick_Name.text = datahub.User.Nickname;
    }
}
