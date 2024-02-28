using System;
using System.Collections;
using System.Collections.Generic;

// Unit ������ �����ص� Ŭ����
public class Unit
{
    #region ����
    private int id;
    private string name;
    private string nickname;
    private int attack;                     // ���ݷ�
    private int attack_speed;               // ���ݼӵ�
    private int upgrade_figures;            // ���׷��̵� 1ȸ�� �ö� ���ݷ� ��
    private int upgrade_value;              // ���� ��ȭ ��ġ
    private int max_upgrade_value = 10;     // �ִ� ��ȭ ��ġ
    private string grade;                   // ���
    private int need_gold;                  // ��ȭ�� �ʿ� ��差
    private int piece;                      // ���� ���� ���� ����
    private int need_piece;                 // ��ȭ�� �ʿ� ���� ����
    private List<CombineFunction> comb_function;    // �� �������κ��� ���� �� �� �ִ� ���ս�

    #endregion

    #region getter setter
    public int Id { get { return id; } set {  id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string NickName { get { return nickname; } set {  nickname = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int AttackSpeed { get { return attack_speed; } set { attack_speed = value; } }
    public int UpgradeFigures { get { return upgrade_figures; } set {  upgrade_figures = value; } }
    public int UpgradeValue { get { return upgrade_value; } set {  upgrade_value = value; } }
    public int MaxUpgradeValue { 
        get { return max_upgrade_value; } 
        set {
            if (value >= 10)
                max_upgrade_value = 10;
            else
                max_upgrade_value = value; 
        } 
    }
    public string Grade { get {  return grade; } set {  grade = value; } }
    public int NeedGold { get { return need_gold; } set { need_gold = value; } } 
    public int Piece { get { return piece; } set { piece = value; } }
    public int NeedPiece { get { return need_piece; } set { need_piece = value; } }
    public List<CombineFunction> CombFucntion { get { return comb_function; } set { comb_function = value; } }

    #endregion
}
