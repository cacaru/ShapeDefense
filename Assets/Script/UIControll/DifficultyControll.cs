using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyControll : MonoBehaviour
{
    [SerializeField] private GameObject PreBtn;
    [SerializeField] private GameObject NextBtn;
    [SerializeField] private TMP_Text DifficultyText;

    private DataHub datahub;

    private Color pre_color;
    private Color next_color;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        pre_color = PreBtn.GetComponent<Image>().color;
        next_color = NextBtn.GetComponent<Image>().color;
    }
    // ���̵� ���� ��ư Ŭ��
    public void DifficultyNextBtnClick() {
        datahub.Difficulty++;
        // 8�� �ִ�� ����
        if (datahub.Difficulty > 8) { 
            datahub.Difficulty = 8; 
        }
        // 8�̸� ���� ��ư�� ������ �ʵ��� ����
        else if (datahub.Difficulty == 8) {
            next_color.a = 0;
            NextBtn.GetComponent<Image>().color = next_color;
        }
        // 2�̻���ʹ� ���� ��ư�� ���̵��� ����
        else if (datahub.Difficulty < 8 && datahub.Difficulty >= 2) {
            pre_color.a = 1;
            PreBtn.GetComponent<Image>().color = pre_color;
        }

        DifficultyText.text = datahub.Difficulty.ToString();
    }
    public void DifficultyPreBtnClick() {
        datahub.Difficulty--;
        // 1�� �ּڰ����� ����
        if (datahub.Difficulty <= 0) datahub.Difficulty = 1;
        // 1�̸� ���� ��ư�� ������ �ʵ��� ����
        else if (datahub.Difficulty == 1) {
            pre_color.a = 0;
            PreBtn.GetComponent<Image>().color = pre_color;
        }
        // 2 �̻��̸� ���� ��ư�� ���̵��� ����
        else if (datahub.Difficulty > 1) {
            next_color.a = 1;
            NextBtn.GetComponent<Image>().color = next_color;
        }
        DifficultyText.text = datahub.Difficulty.ToString();
    }
}
