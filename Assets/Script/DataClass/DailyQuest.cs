using System.Collections;


public class DailyQuest
{
    #region VALUE
    private int id;                          // 번호
    private string name;                     // 이름
    private int checker;                     // 보상 받았는지 여부
    private int counter;                     // 보상 달성 여부
    private ArrayList reward_list;           // 보상받을 아이템 리스트
    private ArrayList reward_val;            // 보상 아이템당 갯수
    private int request_counter;             // 보상받을때 까지 필요한 counter
    private bool can_recive = false;         // 지금 받을 수 있는지 여부
    #endregion

    #region property
    public int Id { get { return id; } set { id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public int Checker { get { return checker; } set { checker = value; } }
    public int Counter { get { return counter; } set { counter = value; } }
    public ArrayList RewardList { get { return reward_list; } set { reward_list = value; } }
    public ArrayList RewardVal { get { return reward_val; } set { reward_val = value; } }
    public int RequestCounter { get { return request_counter; } set { request_counter = value; } }
    public bool CanRecive { get { return can_recive; } set { can_recive = value; } }
    #endregion
}
