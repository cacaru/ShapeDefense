using System.Collections;

public class Achievement {

    /*
     * +로 다수의 보상 구분
     * _로 reward 내용과 내용에 따른 보상 액 구분
     * . 으로 반복에 따른 배수 구분
     *   -> .이 있으면 .앞의 값 * checker를 reward val에 넣기
     * , 로 반복 달성 필요량의 증가분 구분
     *   -> , 가 있으면 매치값으로 구분 repeat를 true로 설정하고 
     *   -> reward_val[checker]에 해당하는 값이 필요 reward 값으로 들어가고
     * */
    
    #region VALUE
    private int id;                          // 업적 번호
    private string name;                     // 업적 이름
    private int checker;                     // 보상했는지(0,1) or 몇번 반복했었는지
    private int counter;                     // 업적의 내용을 몇번 했는지
    private ArrayList reward_list;           // 보상받을 아이템 리스트
    private ArrayList reward_val;            // 보상 아이템당 갯수
    private ArrayList repeat_reward_request; // 반복 요구 값
    private bool repeat = false;             // n에 의한 반복 보상 판별 여부
    private int end_time;                    // 반복 보상의 마지막 단계
    private int endless_value;               // 무한 반복 단위
    private bool can_recive = false;         // 지금 받을 수 있는지 여부
    #endregion

    #region property
    public int Id { get { return id; } set { id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public int Checker { get { return checker; } set { checker = value; } }
    public int Counter { get { return counter; } set { counter = value; } }
    public ArrayList RewardList { get { return reward_list; } set { reward_list = value; } }
    public ArrayList RewardVal { get { return reward_val; } set { reward_val = value; } }
    public ArrayList RepeatRewardRequest { get { return repeat_reward_request; } set { repeat_reward_request = value; } }
    public int EndTime { get { return end_time; } set { end_time = value; } }
    public int EndlessValue { get { return endless_value; } set { endless_value = value; } }
    public bool Repeat { get { return repeat;} set { repeat = value; } }
    public bool CanRecive { get {  return can_recive; } set {  can_recive = value; } }
    #endregion
}
