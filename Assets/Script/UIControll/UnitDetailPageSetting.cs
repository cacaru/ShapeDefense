using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailPageSetting : MonoBehaviour
{
    // ������ ����
    private DataHub datahub;

    // ������ �������� �ְ��� ������Ʈ��
    [SerializeField]
    private Image Image;                     // �̹���
    [SerializeField]
    private TMP_Text NickName;                // �̸�
    [SerializeField]
    private TMP_Text Level;                   // ���� ��ȭ�� (0���� ����)
    [SerializeField]
    private TMP_Text Attack;                  // ���ݷ�
    [SerializeField]
    private TMP_Text AttackSpeed;             // ���ݼӵ�
    [SerializeField]
    private TMP_Text Piece;                   // �������� / �ʿ����� ǥ��

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // ���� �������� �� ���� Unit
        Unit unit = (Unit)datahub.Unit[datahub.Unitid];

        // �������� ������ ���� ����
        NickName.text = unit.NickName;
        Level.text = unit.UpgradeValue.ToString();
        Attack.text = unit.Attack.ToString();
        AttackSpeed.text = unit.AttackSpeed.ToString();
        Piece.text = unit.Piece.ToString() + " / " + unit.NeedPiece.ToString();

        // �̹��� ����
        string[] part = unit.Name.Split("_");
        switch (part[0]) {
            case "n":
                if (part[1] == "circle") 
                    Image.sprite = Resources.Load<Sprite>("Sprite/Unit/normal_circle");
                else if (part[1] == "triangle")
                    Image.sprite = Resources.Load<Sprite>("Sprite/Unit/normal_triangle");
                else if (part[1] == "square")
                    Image.sprite = Resources.Load<Sprite>("Sprite/Unit/normal_square");
                break;

            case "ad":
                if (part[1] == "circle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_circle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_circle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_circle_3");
                    }
                }
                else if (part[1] == "triangle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_triangle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_triangle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_triangle_3");
                    }
                }
                else if (part[1] == "square") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_square_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_square_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/ad_square_3");
                    }
                }
                break;

            case "lu":
                if (part[1] == "circle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_circle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_circle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_circle_3");
                    }
                }
                else if (part[1] == "triangle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_triangle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_triangle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_triangle_3");
                    }
                }
                else if (part[1] == "square") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_square_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_square_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_square_3");
                    }
                }
                else if (part[1] == "star") {
                    Image.sprite = Resources.Load<Sprite>("Sprite/Unit/lu_star");
                }
                break;

            case "li":
                if (part[1] == "circle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_circle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_circle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_circle_3");
                    }
                }
                else if (part[1] == "triangle") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_triangle_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_triangle_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_triangle_3");
                    }
                }
                else if (part[1] == "square") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_square_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_square_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_square_3");
                    }
                }
                else if (part[1] == "star") {
                    if (part[2] == "1") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_star_1");
                    }
                    else if (part[2] == "2") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_star_2");
                    }
                    else if (part[2] == "3") {
                        Image.sprite = Resources.Load<Sprite>("Sprite/Unit/li_star_3");
                    }
                }
                break;

            case "c":
                break;

            default: break;
        }
    }


}
