using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneChanger : MonoBehaviour
{
    // �ΰ��� ���� ȭ�� ������Ʈ ����
    [SerializeField]
    public GameObject settingCanvas;

    /// <summary>
    /// ���� ���� �ʱ�ȭ�� ���� datahub
    /// </summary>
    private DataHub datahub;


    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }
    // ���� ȭ�� �̵�
    public void SettingOnClick() {
        settingCanvas.SetActive(true);
    }

    // ���� ������
    public void SettingExitClick() {
        settingCanvas.SetActive(false);
    }
    // ���� ������ == �κ�� �̵� == gameStartPage�� �̵�
    public void GameExitClick() {
        // �� ���� ������ �ʱ�ȭ
        ResetData();
        SceneManager.LoadScene("GameStartScene");
    }

    private void ResetData() {
        Debug.Log("data reset");
        
        // ��ȭ�� �ʱ�ȭ
        datahub.UpgradeNoValue = 0;
        datahub.UpgradeAdValue = 0;
        datahub.UpgradeLuValue = 0;
        datahub.UpgradeLiValue = 0;
        datahub.UpgradeCuValue = 0;

        // ���� �� ��ȭ �ʱ�ȭ
        datahub.Dot = 80;

        // ���� �ʱ�ȭ
        datahub.RoundNumber = 0;
        // �������� �ʱ�ȭ
        datahub.StageNumber = 1;
        // ���� ���� �� �ʱ�ȭ
        for (int i = 0; i < datahub.UnitCounter.Count(); i++) {
            datahub.UnitCounter[i] = 0;
        }
        // ���� �ʱ�ȭ 
        datahub.CombineWaitingPos = 0;
        datahub.CombineTargetId = 0;
        // ���̵� �ʱ�ȭ
        datahub.Difficulty = 1;
    }
}
