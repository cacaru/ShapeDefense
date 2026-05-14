using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;


namespace ShapeDefenseSpace {

    /// 공용 변수 저장
    public class PublicData {
        public static readonly int MAX_Difficulty = 15;

        public static readonly string DEFAULT_STR = "";
        public static readonly string Complete_Text = "완료";
        public static readonly string ANI_ACTIVATE = "Activate";

        #region achivement quest 관련색
        // prefab background color 설정
        //private Color DEFAULT_BACKGROUND = new(196 / 255f, 215 / 255f, 221 / 255f, 1f);
        //private Color DEFAULT_BACKGROUND = new(221 / 255f, 238 / 255f, 228 / 255f, 1f);
        //private Color DEFAULT_BACKGROUND = new(228 / 255f, 196 / 255f, 186 / 255f, 1f);
        //private Color DEFAULT_BACKGROUND = new(189 / 255f, 225 / 255f, 189 / 255f, 1f);
        //private Color DEFAULT_BACKGROUND = new(176 / 255f, 212 / 255f, 144 / 255f, 1f);

        //private Color DEFAULT_BACKGROUND = new(218 / 255f, 246 / 255f, 210 / 255f, 1f);
        //private Color DEFAULT_BACKGROUND = new(218 / 255f, 246 / 255f, 210 / 255f, 120 / 255f);
        //private Color DEFAULT_BACKGROUND = new(130 / 255f, 162 / 255f, 224 / 255f, 120 / 255f);
        //public static readonly Color DEACTIVATE_BACKGROUND = new(92 / 255f, 162 / 255f, 105 / 255f, 120 / 255f);

        //public static readonly Color DEFAULT_BACKGROUND = new(206 / 255f, 228 / 255f, 186 / 255f, 1f);
        public static readonly Color DEFAULT_BACKGROUND = new(240 / 255f, 239 / 255f, 235 / 255f, 1f);
        //public static readonly Color DEACTIVATE_BACKGROUND = new(92 / 255f, 162 / 255f, 105 / 255f, 120 / 255f);
        //public static readonly Color DEACTIVATE_BACKGROUND = new(188 / 255f, 165 / 255f, 145 / 255f, 120 / 255f);
        public static readonly Color DEACTIVATE_BACKGROUND = new(84 / 255f, 76 / 255f, 69 / 255f, 120 / 255f);

        //public static readonly Color BTN_ACTIVATE_STATE = new(128 / 255f, 192 / 255f, 72 / 255f, 1f);
        public static readonly Color BTN_ACTIVATE_STATE = new(219 / 255f, 248 / 255f, 183 / 255f, 1f);
        public static readonly Color BTN_DISABLE_STATE = new(119 / 255f, 135 / 255f, 99 / 255f, 1f);

        public static readonly Color ACTIVE_TEXT = new(1f, 1f, 1f, 1f);
        public static readonly Color DISABLE_TEXT = new(50 / 255f, 50 / 255f, 50 / 255f, 1f);

        #endregion

        #region 적 유닛 sprite 
        // 처음부터 흰색으로 받아서 색상 칠하기
        private static readonly SpriteAtlas enemy_atlas = Resources.Load<SpriteAtlas>("Sprite/EnemyAtlas");
        public static readonly Sprite enemy_sprite_clover_1 = enemy_atlas.GetSprite("clover_1"); //Resources.Load<Sprite>("Sprite/Enemy/clover_1");
        public static readonly Sprite enemy_sprite_clover_2 = enemy_atlas.GetSprite("clover_2"); //Resources.Load<Sprite>("Sprite/Enemy/clover_2");
        public static readonly Sprite enemy_sprite_heart_1 = enemy_atlas.GetSprite("heart_1"); //Resources.Load<Sprite>("Sprite/Enemy/heart_1");
        public static readonly Sprite enemy_sprite_heart_2 = enemy_atlas.GetSprite("heart_2"); //Resources.Load<Sprite>("Sprite/Enemy/heart_2");
        public static readonly Sprite enemy_sprite_spade_1 = enemy_atlas.GetSprite("spade_1"); //Resources.Load<Sprite>("Sprite/Enemy/spade_1");
        public static readonly Sprite enemy_sprite_spade_2 = enemy_atlas.GetSprite("spade_2"); //Resources.Load<Sprite>("Sprite/Enemy/spade_2");
        public static readonly Sprite enemy_sprite_boss_1 = enemy_atlas.GetSprite("boss_1"); //Resources.Load<Sprite>("Sprite/Enemy/boss_1");
        public static readonly Sprite enemy_sprite_boss_2 = enemy_atlas.GetSprite("boss_2"); //Resources.Load<Sprite>("Sprite/Enemy/boss_2");
        public static readonly Sprite enemy_sprite_boss_3 = enemy_atlas.GetSprite("boss_3"); //Resources.Load<Sprite>("Sprite/Enemy/boss_3");
        public static readonly Sprite enemy_sprite_boss_4 = enemy_atlas.GetSprite("boss_4"); //Resources.Load<Sprite>("Sprite/Enemy/boss_4");

        public static readonly Color f_enemy_color = new(1, 1, 1);
        public static readonly Color e_enemy_color = new(144 / 255f, 140 / 255f, 30 / 255f, 1);
        public static readonly Color d_enemy_color = new(77 / 255f, 111 / 255f, 70 / 255f, 1);
        public static readonly Color c_enemy_color = new(35 / 255f, 78 / 255f, 140 / 255f, 1);
        public static readonly Color reinforced_enemy_color = new(169 / 255f, 46 / 255f, 195 / 255f);

        #endregion

        #region 조합옵션 문구
        public static readonly string CombineCheckOptionON = "유닛을 조합할 때 확인창을 띄웁니다.";
        public static readonly string CombineCheckOptionOFF = "유닛을 조합할 때 확인창 없이 바로 조합합니다.";
        #endregion

        #region 변환 옵션 문구
        public static readonly string RollCheckOptionON = "특수 재료 (별 / 달의 조화 혹은 결정) 를 변환할 때 확인창을 띄웁니다.";
        public static readonly string RollCheckOptionOFF = "특수 재료 (별 / 달의 조화 혹은 결정) 를 변환할 때 확인창 없이 바로 변환합니다.";
        #endregion

        #region 상세페이지에서 강화가능 표기용
        public static readonly Color Impossible_announce = new(224 / 255f, 63 / 255f, 79 / 255f);
        public static readonly Color Possible_announce = new(170 / 255f, 1f, 250 /255f);
        #endregion

        #region 합성 가능 표기용
        public static readonly Color Can_Amalgation = new(170 / 255f, 1f, 250 / 255f, 1f);
        public static readonly Color Amalgation_Impossible = new(64 / 255f, 64 / 255f, 64 / 255f);
        #endregion

        #region 등급별 생상 코드
        public static readonly Color core_color = new(125 / 255f, 180 / 255f, 238 / 255f, 1);
        public static readonly Color unicore_color = new(193 / 255f, 123 / 255f, 159 / 255f, 1);
        public static readonly Color crystal_color = new(161 / 255f, 194 / 255f, 199 / 255f, 1);
        // e d c b a s 컬러 생성
        public static readonly Color color_e = new(204 / 255f, 209 / 255f, 48 / 255f, 1);
        public static readonly Color color_d = new(28 / 255f, 195 / 255f, 0, 1);
        public static readonly Color color_c = new(50 / 255f, 117 / 255f, 215 / 255f, 1);
        public static readonly Color color_b = new(151 / 255f, 120 / 255f, 238 / 255f, 1);
        public static readonly Color color_a = new(255 / 255f, 170 / 255f, 83 / 255f, 1);
        public static readonly Color color_s = new(224 / 255f, 83 / 255f, 90 / 255f, 1);
        #endregion

        #region 공격형식 테두리 색
        public static readonly Color color_poison_border = new(38 / 255f, 185 / 255f, 59 / 255f);
        public static readonly Color color_blust_border = new(226 / 255f, 44 / 255f, 79 / 255f);
        public static readonly Color color_paralysis_border = new(1f, 202 / 255f, 0f);
        #endregion

        #region 타일
        public static readonly AnimatedTile unit_ani_tile = Resources.Load<AnimatedTile>("Sprite/Tile/set_unit_tile");
        public static readonly TileBase unit_base = Resources.Load<TileBase>("Sprite/Tile/floating_board_2");
        public static readonly TileBase empty_base = Resources.Load<TileBase>("Sprite/Tile/floating_board_1");
        #endregion

        #region 모양 이름 string 
        public static readonly string CIRCLE = "circle";
        public static readonly string TRIANGLE = "triangle";
        public static readonly string SQUARE = "square";
        public static readonly string AMAL = "amalgation";
        public static readonly string STAR = "star";
        public static readonly string MOON = "moon";
        public static readonly string SUN = "sun";

        public static readonly string E = "E";
        public static readonly string D = "D";
        public static readonly string C = "C";
        public static readonly string B = "B";
        public static readonly string A = "A";
        public static readonly string S = "S";
        public static readonly string IC = "IC";
        public static readonly string IB = "IB";
        public static readonly string IA = "IA";


        #endregion

        #region unit sprite path value
        private static readonly SpriteAtlas unit_atlas = Resources.Load<SpriteAtlas>("Sprite/UnitAtlas");
        public static readonly Sprite unit_e_circle = unit_atlas.GetSprite("e_circle");//Resources.Load<Sprite>("Sprite/Unit/e_circle");
        public static readonly Sprite unit_e_triangle = unit_atlas.GetSprite("e_triangle"); //Resources.Load<Sprite>("Sprite/Unit/e_triangle");
        public static readonly Sprite unit_e_square = unit_atlas.GetSprite("e_square"); //Resources.Load<Sprite>("Sprite/Unit/e_square");
        public static readonly Sprite unit_e_star = unit_atlas.GetSprite("e_star"); //Resources.Load<Sprite>("Sprite/Unit/e_star");
        public static readonly Sprite unit_e_moon = unit_atlas.GetSprite("e_moon"); //Resources.Load<Sprite>("Sprite/Unit/e_moon");
        public static readonly Sprite unit_e_sun = unit_atlas.GetSprite("e_sun"); //Resources.Load<Sprite>("Sprite/Unit/e_sun");

        public static readonly Sprite unit_d_circle = unit_atlas.GetSprite("e_circle"); //Resources.Load<Sprite>("Sprite/Unit/e_circle");
        public static readonly Sprite unit_d_triangle = unit_atlas.GetSprite("e_triangle");//Resources.Load<Sprite>("Sprite/Unit/e_triangle");
        public static readonly Sprite unit_d_square = unit_atlas.GetSprite("e_square");//Resources.Load<Sprite>("Sprite/Unit/e_square");

        public static readonly Sprite unit_c_circle_1 = unit_atlas.GetSprite("c_circle_1"); //Resources.Load<Sprite>("Sprite/Unit/c_circle_1");
        public static readonly Sprite unit_c_circle_2 = unit_atlas.GetSprite("c_circle_2"); //Resources.Load<Sprite>("Sprite/Unit/c_circle_2");
        public static readonly Sprite unit_c_circle_3 = unit_atlas.GetSprite("c_circle_3"); //Resources.Load<Sprite>("Sprite/Unit/c_circle_3");
        public static readonly Sprite unit_c_triangle_1 = unit_atlas.GetSprite("c_triangle_1"); // Resources.Load<Sprite>("Sprite/Unit/c_triangle_1");
        public static readonly Sprite unit_c_triangle_2 = unit_atlas.GetSprite("c_triangle_2");//Resources.Load<Sprite>("Sprite/Unit/c_triangle_2");
        public static readonly Sprite unit_c_triangle_3 = unit_atlas.GetSprite("c_triangle_3"); //Resources.Load<Sprite>("Sprite/Unit/c_triangle_3");
        public static readonly Sprite unit_c_square_1 = unit_atlas.GetSprite("c_square_1"); //Resources.Load<Sprite>("Sprite/Unit/c_square_1");
        public static readonly Sprite unit_c_square_2 = unit_atlas.GetSprite("c_square_2"); //Resources.Load<Sprite>("Sprite/Unit/c_square_2");
        public static readonly Sprite unit_c_square_3 = unit_atlas.GetSprite("c_square_3"); //Resources.Load<Sprite>("Sprite/Unit/c_square_3");
        public static readonly Sprite unit_c_amalgation_1 = unit_atlas.GetSprite("c_amalgation_1"); //Resources.Load<Sprite>("Sprite/Unit/c_amalgation_1");
        public static readonly Sprite unit_c_amalgation_2 = unit_atlas.GetSprite("c_amalgation_2"); //Resources.Load<Sprite>("Sprite/Unit/c_amalgation_2");
        public static readonly Sprite unit_c_amalgation_3 = unit_atlas.GetSprite("c_amalgation_3"); //Resources.Load<Sprite>("Sprite/Unit/c_amalgation_3");
        public static readonly Sprite unit_c_star = unit_atlas.GetSprite("c_star"); //Resources.Load<Sprite>("Sprite/Unit/c_star");
        public static readonly Sprite unit_c_moon = unit_atlas.GetSprite("c_moon"); //Resources.Load<Sprite>("Sprite/Unit/c_moon");
        public static readonly Sprite unit_c_sun = unit_atlas.GetSprite("c_sun"); //Resources.Load<Sprite>("Sprite/Unit/c_sun");


        public static readonly Sprite unit_b_circle_1 = unit_atlas.GetSprite("b_circle_1"); //Resources.Load<Sprite>("Sprite/Unit/b_circle_1");
        public static readonly Sprite unit_b_circle_2 = unit_atlas.GetSprite("b_circle_2"); //Resources.Load<Sprite>("Sprite/Unit/b_circle_2");
        public static readonly Sprite unit_b_circle_3 = unit_atlas.GetSprite("b_circle_3"); //Resources.Load<Sprite>("Sprite/Unit/b_circle_3");
        public static readonly Sprite unit_b_triangle_1 = unit_atlas.GetSprite("b_triangle_1"); //Resources.Load<Sprite>("Sprite/Unit/b_triangle_1");
        public static readonly Sprite unit_b_triangle_2 = unit_atlas.GetSprite("b_triangle_2"); //Resources.Load<Sprite>("Sprite/Unit/b_triangle_2");
        public static readonly Sprite unit_b_triangle_3 = unit_atlas.GetSprite("b_triangle_3"); //Resources.Load<Sprite>("Sprite/Unit/b_triangle_3");
        public static readonly Sprite unit_b_square_1 = unit_atlas.GetSprite("b_square_1"); //Resources.Load<Sprite>("Sprite/Unit/b_square_1");
        public static readonly Sprite unit_b_square_2 = unit_atlas.GetSprite("b_square_2"); //Resources.Load<Sprite>("Sprite/Unit/b_square_2");
        public static readonly Sprite unit_b_square_3 = unit_atlas.GetSprite("b_square_3"); //Resources.Load<Sprite>("Sprite/Unit/b_square_3");
        public static readonly Sprite unit_b_star = unit_atlas.GetSprite("b_star"); //Resources.Load<Sprite>("Sprite/Unit/b_star");
        public static readonly Sprite unit_b_moon = unit_atlas.GetSprite("b_moon"); //Resources.Load<Sprite>("Sprite/Unit/b_moon");
        public static readonly Sprite unit_b_sun = unit_atlas.GetSprite("b_sun"); //Resources.Load<Sprite>("Sprite/Unit/b_moon");

        public static readonly Sprite unit_a_circle = unit_atlas.GetSprite("a_circle"); //Resources.Load<Sprite>("Sprite/Unit/a_circle");
        public static readonly Sprite unit_a_triangle = unit_atlas.GetSprite("a_triangle"); //Resources.Load<Sprite>("Sprite/Unit/a_triangle");
        public static readonly Sprite unit_a_square = unit_atlas.GetSprite("a_square"); //Resources.Load<Sprite>("Sprite/Unit/a_square");
        public static readonly Sprite unit_a_star = unit_atlas.GetSprite("a_star"); //Resources.Load<Sprite>("Sprite/Unit/a_star");
        public static readonly Sprite unit_a_moon = unit_atlas.GetSprite("a_moon"); //Resources.Load<Sprite>("Sprite/Unit/a_moon");
        public static readonly Sprite unit_a_sun = unit_atlas.GetSprite("a_sun"); //Resources.Load<Sprite>("Sprite/Unit/a_moon");

        public static readonly Sprite unit_s_circle = unit_atlas.GetSprite("s_circle"); //Resources.Load<Sprite>("Sprite/Unit/s_circle");
        public static readonly Sprite unit_s_triangle = unit_atlas.GetSprite("s_triangle"); //Resources.Load<Sprite>("Sprite/Unit/s_triangle");
        public static readonly Sprite unit_s_square = unit_atlas.GetSprite("s_square"); //Resources.Load<Sprite>("Sprite/Unit/s_square");
        public static readonly Sprite unit_s_star = unit_atlas.GetSprite("s_star"); //Resources.Load<Sprite>("Sprite/Unit/s_star");
        public static readonly Sprite unit_s_moon = unit_atlas.GetSprite("s_moon"); //Resources.Load<Sprite>("Sprite/Unit/s_moon");
        public static readonly Sprite unit_s_sun = unit_atlas.GetSprite("s_sun"); //Resources.Load<Sprite>("Sprite/Unit/s_moon");

        public static readonly Sprite unit_c_circle_0 = Resources.Load<Sprite>("Sprite/Unit/c_circle_0");
        public static readonly Sprite unit_c_triangle_0 = Resources.Load<Sprite>("Sprite/Unit/c_triangle_0");
        public static readonly Sprite unit_c_square_0 = Resources.Load<Sprite>("Sprite/Unit/c_square_0");
        public static readonly Sprite unit_c_star_0 = Resources.Load<Sprite>("Sprite/Unit/c_star_0");
        public static readonly Sprite unit_c_moon_0 = Resources.Load<Sprite>("Sprite/Unit/c_moon_0");
        public static readonly Sprite unit_c_sun_0 = Resources.Load<Sprite>("Sprite/Unit/c_sun_0");

        public static readonly Sprite unit_b_circle_0 = Resources.Load<Sprite>("Sprite/Unit/b_circle_0");
        public static readonly Sprite unit_b_triangle_0 = Resources.Load<Sprite>("Sprite/Unit/b_triangle_0");
        public static readonly Sprite unit_b_square_0 = Resources.Load<Sprite>("Sprite/Unit/b_square_0");
        public static readonly Sprite unit_b_star_0 = Resources.Load<Sprite>("Sprite/Unit/b_star_0");
        public static readonly Sprite unit_b_moon_0 = Resources.Load<Sprite>("Sprite/Unit/b_moon_0");
        public static readonly Sprite unit_b_sun_0 = Resources.Load<Sprite>("Sprite/Unit/b_sun_0");

        public static readonly Sprite unit_a_harmony_of_star_moon = Resources.Load<Sprite>("Sprite/Unit/a_harmony_of_star_moon");
        public static readonly Sprite unit_a_harmony_of_sun_moon = Resources.Load<Sprite>("Sprite/Unit/a_harmony_of_sun_moon");
        public static readonly Sprite unit_a_harmony_of_sun_star = Resources.Load<Sprite>("Sprite/Unit/a_harmony_of_sun_star");

        #endregion

        #region unit out border
        public static readonly Sprite poison_border = Resources.Load<Sprite>("Sprite/Unit_Border/poison_border");
        public static readonly Sprite blust_border = Resources.Load<Sprite>("Sprite/Unit_Border/blust_border");
        public static readonly Sprite paralysis_border = Resources.Load<Sprite>("Sprite/Unit_Border/paralysis_border");
        #endregion

        #region bullet sprite value
        private static readonly SpriteAtlas bullet_atlas = Resources.Load<SpriteAtlas>("Sprite/BulletAtlas");
        public static readonly Sprite cir_bullet = bullet_atlas.GetSprite("circle_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/circle_bullet");
        public static readonly Sprite tri_bullet = bullet_atlas.GetSprite("triangle_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/triangle_bullet");
        public static readonly Sprite squ_bullet = bullet_atlas.GetSprite("square_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/square_bullet");
        public static readonly Sprite sta_bullet = bullet_atlas.GetSprite("star_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/star_bullet");
        public static readonly Sprite moo_bullet = bullet_atlas.GetSprite("moon_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/moon_bullet");
        public static readonly Sprite sun_bullet = bullet_atlas.GetSprite("sun_bullet"); //Resources.Load<Sprite>("Sprite/Bullet/sun_bullet");

        #endregion


        #region 유닛 최대 최소 비 저장
        public static readonly int E_start = 1001;
        public static readonly int E_end = 1003;
        public static readonly int E_end_1 = 1004;

        public static readonly int D_start = 2001;
        public static readonly int D_end = 2003;
        public static readonly int D_end_1 = 2004;

        public static readonly int C_start = 3001;
        public static readonly int C_end = 3015;
        public static readonly int C_end_1 = 3016;

        public static readonly int B_start = 4001;
        public static readonly int B_end = 4012;
        public static readonly int B_end_1 = 4013;

        public static readonly int A_start = 5001;
        public static readonly int A_end = 5006;
        public static readonly int A_end_1 = 5007;

        public static readonly int S_start = 6001;
        public static readonly int S_end = 6006;
        public static readonly int S_end_1 = 6007;
        #endregion
    }
}