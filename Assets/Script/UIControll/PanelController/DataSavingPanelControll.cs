using System.Collections;
using TMPro;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class DataSavingPanelControll : SceneSingleton<DataSavingPanelControll>
{
    [SerializeField] private GameObject ConfirmField;
    [SerializeField] private GameObject CheckField;
    [SerializeField] private GameObject EndField;
    [SerializeField] private GameObject LoadingField;
    [SerializeField] private Transform LoadingIcon;
    [SerializeField] private TMP_Text CheckAnnounce;
    [SerializeField] private TMP_Text EndAnnounce;
    [SerializeField] private GameObject SaveBtn;
    [SerializeField] private GameObject LoadBtn;
    [SerializeField] private GameObject FailText;


    private readonly string save_announce = "데이터를 저장하시겠습니까?\n경로는 다운로드 폴더 입니다.";
    private readonly string load_announce = "다운로드 폴더에 저장된 데이터를 불러오시겠습니까?";

    private readonly string save_success_announce = "저장을 완료하였습니다.";
    private readonly string load_success_announce = "불러오기를 완료하였습니다.";


    public void SetSaveField() {
        CheckAnnounce.text = save_announce;
        SaveBtn.SetActive(true);
        ConfirmField.SetActive(true);
    }

    public void SetLoadField() {
        CheckAnnounce.text = load_announce;
        LoadBtn.SetActive(true);
        ConfirmField.SetActive(true);
    }

    public void SaveEndField() {
        EndAnnounce.text = save_success_announce;
        LoadingField.SetActive(false);
        CheckField.SetActive(false);
        EndField.SetActive(true);
    }

    public void LoadEndField() {
        EndAnnounce.text = load_success_announce;
        LoadingField.SetActive(false);
        CheckField.SetActive(false);
        EndField.SetActive(true);
    }

    public void OnLoadingField() {
        LoadingField.SetActive(true);
        CheckField.SetActive(false);
        EndField.SetActive(false);
        StartCoroutine(Loading_icon_spinning());
    }

    IEnumerator Loading_icon_spinning() {
        var trans = LoadingIcon.rotation;
        while (true) {
            yield return wff;
            trans = LoadingIcon.rotation;
            trans.z += 30;
            LoadingIcon.rotation = trans;
        }
    }

    public void CloseField() {
        StopAllCoroutines();
        SaveBtn.SetActive(false);
        LoadBtn.SetActive(false);

        FailText.SetActive(false);

        CheckField.SetActive(true);
        LoadingField.SetActive(false);
        EndField.SetActive(false);

        ConfirmField.SetActive(false);
    }

    public void OnFailText() {
        FailText.SetActive(true);
    }
}
