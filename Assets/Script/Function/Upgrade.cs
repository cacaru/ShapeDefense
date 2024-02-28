using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject Announce;
    [SerializeField]
    private TMP_Text AnnounceText;

    // ���� ����� ����
    private DataHub datahub;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    public void UpgradeUnit() {
        // Unit ������ ��� ���� ��ȭ ���׷��̵� ����
        // �ʿ� ���� :: Unit id
        // ���� ������ ������ ������� Ȯ���ϱ� ���� dot / piece/ need_piece �� 
        //�� �Լ��� ������ ���ִ� �������� null�� �ƴ��� ����
        Unit select_unit = (Unit)datahub.Unit[datahub.Unitid];
        // ��尡 ������� ������ �ȳ������� �����ְ� ����
        if ( select_unit.NeedGold > datahub.User.Dot) {
            // �ȳ� ������ �����ְ�
            Announce.SetActive(true);
            AnnounceText.text = "��尡 �����մϴ�.";
            return;
        }
        
        // ��尡 ����ϸ�
        // ������ ������ ������� ������ �ȳ������� �����ְ� ����
        if ( select_unit.NeedPiece > select_unit.Piece) {
            // �ȳ� ���� �����ְ�
            Announce.SetActive(true);
            AnnounceText.text = "�ʿ��� ������ �����մϴ�.";
            return;
        }
        // ������ ����ϸ�

        // ��ȭ
        Debug.Log("��ȭ ����");            

    }

    public void NotEnoughConfirm() {
        Announce.SetActive(false);
    }
}
