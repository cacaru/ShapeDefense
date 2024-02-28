using System;
using System.Collections;
using System.Collections.Generic;

// Unit 정보를 저장해둘 클래스
public class Unit
{
    #region 변수
    private int id;
    private string name;
    private string nickname;
    private int attack;                     // 공격력
    private int attack_speed;               // 공격속도
    private int upgrade_figures;            // 업그레이드 1회당 올라갈 공격력 값
    private int upgrade_value;              // 현재 강화 수치
    private int max_upgrade_value = 10;     // 최대 강화 수치
    private string grade;                   // 등급
    private int need_gold;                  // 강화시 필요 골드량
    private int piece;                      // 현재 보유 조각 갯수
    private int need_piece;                 // 강화시 필요 조각 갯수
    private List<CombineFunction> comb_function;    // 이 유닛으로부터 조합 할 수 있는 조합식

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
