
public class SetOption {

    private int id;                 // 옵션 id
    private string name;            // 옵션 이름
    private string value;           // 옵션의 설정 값 -> string형식으로 저장한 뒤 필요한 곳에서 필요환 형으로 변환시킴
    // color setting
    // type : COlor32
    // 255,255,255 >> , 로 구분되게 저장
    

    public int Id { get { return id; } set { id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string Value { get { return value; } set { this.value = value; } }
}
