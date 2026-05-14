using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using Unity.VisualScripting;
public class User
{
    #region VALUE
    private string nickname;            // 이름
    private int dot;                    // 골드(점)
    private int level;                  // 레벨
    private int experience;             // 경험치
    private int need_experience;        // 현 레벨 최대 경험치 량
    private int stamina;                // 번개
    private int max_stamina;            // 최대 번개량
    private int change_nickname_recode; // 이름 변경 횟수
    private int skill_point;            // 소지 스킬 포인트(안찍고 남은 포인트)
    private int max_skill_point;        // 최대 스킬 포인트
    private int status_attack_level;        // 추가 공격력 증가 레벨
    private int status_attackspeed_level;   // 추가 공격속도 증가 레벨
    private int status_start_dot_level;     // 시작 재화 추가 레벨
    private int status_gain_dot_level;      // 적 처치시 획득 재화 증가 레벨
    private int status_clear_dot_level;     // 게임 클리어시 획득 골드 증가 레벨

    #endregion

    #region property
    public string Nickname { get { return nickname; } set {  nickname = value; } }
    public int Dot { get { return dot; } set { dot = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int NeedExperience { get { return need_experience; } set { need_experience = value; } }
    public int Stamina { 
        get {  return stamina; } 
        set {  
            stamina = value;
            if (datahub.DBConnectEnd) {
                stamina_observer.StartCharge();
            }
        } 
    }
    public int MaxStamina { get { return max_stamina; } set {  max_stamina = value; } }
    public int ChangeNickNameRecode { get { return change_nickname_recode; } set { change_nickname_recode = value; } }
    public int SkillPoint { get { return skill_point; } set { skill_point = value; } }
    public int MaxSkillPoint { get { return max_skill_point; } set { max_skill_point = value; } }
    public int StatusAttackLevel { get { return status_attack_level; } set {  status_attack_level = value; } }
    public int StatusAttackSpeedLevel { get { return status_attackspeed_level; } set { status_attackspeed_level = value; } }
    public int StatusStartDotLevel { get { return status_start_dot_level; } set { status_start_dot_level = value; } }
    public int StatusGainDotLevel { get { return status_gain_dot_level; } set { status_gain_dot_level = value; } }
    public int StatusClearDotLevel { get { return status_clear_dot_level; } set { status_clear_dot_level = value; } }

    public int Experience {
        get { return experience; }
        set {
            experience = value;
            LevelUpCheck();
        }
    }
    #endregion

    private void LevelUpCheck() {
        string query;
        
        // 레벨 업 할 수 있는지 확인
        if (experience > 0 && experience >= need_experience && need_experience > 0) {
            level++;
            experience -= need_experience;
            need_experience += 100;

            // max stamina
            max_stamina += 2;

            // stamia check
            if ( stamina < max_stamina ) {
                stamina = max_stamina;
            }

            max_skill_point += 1;
            skill_point += 1;

            // db 업로드
            // level, stamina, exp, max_stamina, skillpoint, max_skill_point
            query = UtilityHub.query_builder.Append("UPDATE user SET level=")
                                            .Append(level)
                                            .Append(", stamina=")
                                            .Append(stamina)
                                            .Append(", max_stamina=")
                                            .Append(max_stamina)
                                            .Append(", experience=")
                                            .Append(experience)
                                            .Append(", max_exp=")
                                            .Append(need_experience)
                                            .Append(", skill_point=")
                                            .Append(skill_point)
                                            .Append(", max_skill_point=")
                                            .Append(max_skill_point)
                                            .ToString();
            UtilityHub.query_builder.Clear();
            modifyDB.ControllDB(query, "user");

            if (!datahub.Gaming) {
                // stat effect 수정
                StatEffectObserver.Instance.EffectObserve();
                // stamina 수정
                StaminaShow.Instance.ReShow();
            }
        }
    }


    public void AllSkillUpdate() {
        string query = UtilityHub.query_builder.Append("UPDATE user SET skill_point=")
                                               .Append(skill_point)
                                               .Append(", max_skill_point=")
                                               .Append(max_skill_point)
                                               .Append(", status_attack_level=")
                                               .Append(status_attack_level)
                                               .Append(", status_attackspeed_level=")
                                               .Append(status_attackspeed_level)
                                               .Append(", status_start_dot_level=")
                                               .Append(status_start_dot_level)
                                               .Append(", status_gain_dot_level=")
                                               .Append(status_gain_dot_level)
                                               .Append(", status_clear_dot_level=")
                                               .Append(status_clear_dot_level)
                                               .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");
    }

    public void SkillInit() {
        status_attackspeed_level = 0;
        status_attack_level = 0;
        status_clear_dot_level = 0;
        status_gain_dot_level = 0;
        status_start_dot_level = 0;
        skill_point = max_skill_point;

        AllSkillUpdate();
    }

    private void MaxStaminaInit() {
        string query = UtilityHub.query_builder.Append("UPDATE user SET max_stamina=")
                                               .Append(max_stamina)
                                               .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");
    }

    // 현재 레벨에 맞게 스킬포인트가 조정되어있는지 확인
    public void UserCorrectCheck() {
        //stamina check
        int max_stamina_val = 20 + ((level - 1) * 2);
        if(max_stamina != max_stamina_val) {
            max_stamina = max_stamina_val;
            MaxStaminaInit();
        }

        // skill check
        // 현재 레벨 -1만큼 max skill point가 없다면 생성
        int must_need_val = level - 1;
        if(must_need_val != max_skill_point) {
            max_skill_point = must_need_val;
            skill_point = max_skill_point;
            //Debug.Log(max_skill_point);
            SkillInit();
            return;
        }

        if (max_skill_point == 0 && level >= 2) {
            max_skill_point = level - 1;
            skill_point = max_skill_point;
            SkillInit();
            return;
        }

        datahub.UserConnectEnd = true;
    }
}
