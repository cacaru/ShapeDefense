using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 10���� ���� �����ϴ� ������ ���� �� ���� ������ ����� �Լ�
/// </summary>
public class BossDrop : MonoBehaviour
{
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    public void Drop() {
        // ���� ���忡 ���� ������ ��ȭ�� ���
        // 40������� ����
        // ���� ���
        if (datahub.RoundNumber <= 40) {
            // ������ ����
            UtilityHub.UnitCreateRandomField(40, 41, 42, 43);
        }

        // 50������� ��ȭ
        else {
            UtilityHub.UnitCreateRandomField(44, 45, 46, 47);
        }
        
    }
}
