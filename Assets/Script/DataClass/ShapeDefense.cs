
using UnityEngine;

namespace ShapeDefenseSpace {

    /// 공용으로 사용하게 될 변수 지정(공용변수)
    public class GameData {

        public static DataHub datahub = DataHub.Instance;
        public static ModifyDB modifyDB = ModifyDB.Instance;
        public static AchievementObserver achieve_observer = AchievementObserver.Instance;
        public static GamePause pause = GamePause.Instance;
        public static StaminaObserver stamina_observer = StaminaObserver.Instance;

        // db connector
        public static IQuestConnector Achieve_Connector = AchievementConnector.Instance;
        public static IQuestConnector Daily_Connector = DailyQuestConnector.Instance;
        public static IQuestConnector Weekly_Connector = WeeklyQuestConnector.Instance;

        // prefabs
        public static GameObject _game_clear_reward_obj = Resources.Load<GameObject>("Prefabs/GameClearRewardObj");
        public static GameObject _title = Resources.Load<GameObject>("Prefabs/CombineTitle");
        public static GameObject _function_2 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_2");
        public static GameObject _function_3 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_3");
        public static GameObject _function_4 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_4");
        public static GameObject _function_5 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_5");
        public static GameObject _quest_obj = Resources.Load<GameObject>("Prefabs/Quest");
        public static GameObject _bullet_obj = Resources.Load<GameObject>("Prefabs/Bullet");
        public static GameObject _bullet_die_obj = Resources.Load<GameObject>("Prefabs/BulletDie");
        public static GameObject _enemy_obj = Resources.Load<GameObject>("Prefabs/Enemy");
        public static GameObject _die_effect_obj = Resources.Load<GameObject>("Prefabs/DieEffect");
        public static GameObject _unit_piece_obj = Resources.Load<GameObject>("Prefabs/PieceUnit");

        public static readonly WaitForEndOfFrame attack_frame = new();

        public static readonly WaitForEndOfFrame wff = new();

        public static readonly WaitForSeconds wfs_0_1 = new(.01f);
        public static readonly WaitForSeconds wfs_0_5 = new(.5f);
        public static readonly WaitForSeconds wfs_1 = new(1);
        public static readonly WaitForSeconds wfs_1_5 = new(1.5f);
        public static readonly WaitForSeconds wfs_2 = new(2);
        public static readonly WaitForSeconds wfs_3 = new(3);
        public static readonly WaitForSeconds wfs_4 = new(4);
        public static readonly WaitForSeconds wfs_5 = new(5);
        public static readonly WaitForSeconds wfs_10 = new(10);
        public static readonly WaitForSeconds wfs_60 = new(60);

        #region Attack speed
        public static readonly WaitForSeconds attack_speed_1 = new(1f);
        public static readonly WaitForSeconds attack_speed_1_1 = new(.952f);
        public static readonly WaitForSeconds attack_speed_1_2 = new(.909f);
        public static readonly WaitForSeconds attack_speed_1_3 = new(.869f);
        public static readonly WaitForSeconds attack_speed_1_4 = new(.833f);
        public static readonly WaitForSeconds attack_speed_1_5 = new(.800f);
        public static readonly WaitForSeconds attack_speed_1_6 = new(.769f);
        public static readonly WaitForSeconds attack_speed_1_7 = new(.740f);
        public static readonly WaitForSeconds attack_speed_1_8 = new(.714f);
        public static readonly WaitForSeconds attack_speed_1_9 = new(.689f);
        public static readonly WaitForSeconds attack_speed_1_10 = new(0.666f);
        public static readonly WaitForSeconds attack_speed_2 = new(.5f);
        public static readonly WaitForSeconds attack_speed_2_1 = new(.487f);
        public static readonly WaitForSeconds attack_speed_2_2 = new(.476f);
        public static readonly WaitForSeconds attack_speed_2_3 = new(.465f);
        public static readonly WaitForSeconds attack_speed_2_4 = new(.454f);
        public static readonly WaitForSeconds attack_speed_2_5 = new(.444f);
        public static readonly WaitForSeconds attack_speed_2_6 = new(.437f);
        public static readonly WaitForSeconds attack_speed_2_7 = new(.425f);
        public static readonly WaitForSeconds attack_speed_2_8 = new(.416f);
        public static readonly WaitForSeconds attack_speed_2_9 = new(.408f);
        public static readonly WaitForSeconds attack_speed_2_10 = new(.4f);
        public static readonly WaitForSeconds attack_speed_3 = new(.333f);
        public static readonly WaitForSeconds attack_speed_3_1 = new(.327f);
        public static readonly WaitForSeconds attack_speed_3_2 = new(.322f);
        public static readonly WaitForSeconds attack_speed_3_3 = new(.317f);
        public static readonly WaitForSeconds attack_speed_3_4 = new(.3125f);
        public static readonly WaitForSeconds attack_speed_3_5 = new(.307f);
        public static readonly WaitForSeconds attack_speed_3_6 = new(.303f);
        public static readonly WaitForSeconds attack_speed_3_7 = new(.298f);
        public static readonly WaitForSeconds attack_speed_3_8 = new(.294f);
        public static readonly WaitForSeconds attack_speed_3_9 = new(.289f);
        public static readonly WaitForSeconds attack_speed_3_10 = new(.285f);
        public static readonly WaitForSeconds attack_speed_4 = new(.25f);
        public static readonly WaitForSeconds attack_speed_4_1 = new(.246f);
        public static readonly WaitForSeconds attack_speed_4_2 = new(.243f);
        public static readonly WaitForSeconds attack_speed_4_3 = new(.240f);
        public static readonly WaitForSeconds attack_speed_4_4 = new(.238f);
        public static readonly WaitForSeconds attack_speed_4_5 = new(.235f);
        public static readonly WaitForSeconds attack_speed_4_6 = new(.232f);
        public static readonly WaitForSeconds attack_speed_4_7 = new(.229f);
        public static readonly WaitForSeconds attack_speed_4_8 = new(.227f);
        public static readonly WaitForSeconds attack_speed_4_9 = new(.224f);
        public static readonly WaitForSeconds attack_speed_4_10 = new(.222f);
        public static readonly WaitForSeconds attack_speed_5 = new(.2f);
        public static readonly WaitForSeconds attack_speed_5_1 = new(.198f);
        public static readonly WaitForSeconds attack_speed_5_2 = new(.196f);
        public static readonly WaitForSeconds attack_speed_5_3 = new(.194f);
        public static readonly WaitForSeconds attack_speed_5_4 = new(.192f);
        public static readonly WaitForSeconds attack_speed_5_5 = new(.190f);
        public static readonly WaitForSeconds attack_speed_5_6 = new(.188f);
        public static readonly WaitForSeconds attack_speed_5_7 = new(.186f);
        public static readonly WaitForSeconds attack_speed_5_8 = new(.184f);
        public static readonly WaitForSeconds attack_speed_5_9 = new(.182f);
        public static readonly WaitForSeconds attack_speed_5_10 = new(.18f);
        public static readonly WaitForSeconds attack_speed_6 = new(.166f);
        public static readonly WaitForSeconds attack_speed_6_1 = new(.165f);
        public static readonly WaitForSeconds attack_speed_6_2 = new(.163f);
        public static readonly WaitForSeconds attack_speed_6_3 = new(.162f);
        public static readonly WaitForSeconds attack_speed_6_4 = new(.161f);
        public static readonly WaitForSeconds attack_speed_6_5 = new(.16f);
        public static readonly WaitForSeconds attack_speed_6_6 = new(.158f);
        public static readonly WaitForSeconds attack_speed_6_7 = new(.157f);
        public static readonly WaitForSeconds attack_speed_6_8 = new(.156f);
        public static readonly WaitForSeconds attack_speed_6_9 = new(.155f);
        public static readonly WaitForSeconds attack_speed_6_10 = new(.153f);
        public static readonly WaitForSeconds attack_speed_7 = new(.142f);
        public static readonly WaitForSeconds attack_speed_7_1 = new(.141f);
        public static readonly WaitForSeconds attack_speed_7_2 = new(.14f);
        public static readonly WaitForSeconds attack_speed_7_3 = new(.139f);
        public static readonly WaitForSeconds attack_speed_7_4 = new(.138f);
        public static readonly WaitForSeconds attack_speed_7_5 = new(.137f);
        public static readonly WaitForSeconds attack_speed_7_6 = new(.136f);
        public static readonly WaitForSeconds attack_speed_7_7 = new(.135f);
        public static readonly WaitForSeconds attack_speed_7_8 = new(.134f);
        public static readonly WaitForSeconds attack_speed_7_9 = new(.132f);
        public static readonly WaitForSeconds attack_speed_7_10 = new(.131f);
        #endregion

        public static readonly WaitForSecondsRealtime wfsr_1 = new(1);
        public static readonly WaitForSecondsRealtime wfsr_2 = new(2);
        public static readonly WaitForSecondsRealtime wfsr_5 = new(5);
    }

    public class Layers {
        public const int Enemy = 14;
        public const int EnemyPoolState = 13;
        public int Unit = LayerMask.GetMask("Unit");
    }

    public enum STATE {

        // db
        DB_CONNECTING = 9000,
        DB_CONNECTED,
        DB_WAIT,
        DB_MODIFYING
    }

    public enum SCENE_NUMBER {
        NONE = 0,
        LOBBY,
        SHOP,
        SETTING,
        FIELD_1,
        FIELD_2,
        FIELD_3,
        FIELD_4,
        FIELD_5,
        FIELD_6,
        FIELD_7,
        FIELD_8
    }

    public class ShapeDefense : MonoBehaviour {

    }

}