using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

public class ColorPicker : MonoBehaviour
{

    [SerializeField] private Image Palette;
    [SerializeField] private Image Cursor;
    [SerializeField] private Image Change_Img;
    [SerializeField] private Color32 change_color;
    [SerializeField] private Image Now_Img;

    // 변경될  background
    [SerializeField] private GameObject Background_Main;
    [SerializeField] private GameObject Background_Footer;

    // 변경될 icon
    [SerializeField] private Material Icon_Back;
    [SerializeField] private Material Icon;

    [SerializeField] private GameObject Canvas;

    // 현재 변경중인 색 테마를 받을 변수
    private int type;
    public int Type { set { type = value; } }

    private string query;

    private Vector2 palette_size;
    private CircleCollider2D palette_collider;

    // Start is called before the first frame update
    void Start()
    {
        palette_collider = Palette.GetComponent<CircleCollider2D>();

        palette_size = new Vector2( Palette.GetComponent<RectTransform>().rect.width,
                                    Palette.GetComponent<RectTransform>().rect.height );
    }

    private void SelectColor() {
        Vector3 offset = Input.mousePosition - transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, palette_collider.radius);
        //Debug.Log(Cursor.transform.position);
        // 커서 위치를 팔레트 안으로 제한
        Cursor.transform.position = transform.position + diff;

        change_color = GetColor();
        Change_Img.color = change_color;
    }

    private Color32 GetColor() {

        Vector2 palette_pos = Palette.transform.position;
        Vector2 cursor_pos = Cursor.transform.position;

        Vector2 pos = cursor_pos - palette_pos + palette_size * 0.5f;
        Vector2 normalized = new(
            (pos.x / (Palette.GetComponent<RectTransform>().rect.width)),
            (pos.y / (Palette.GetComponent<RectTransform>().rect.height)));
        
        Texture2D texture = Palette.mainTexture as Texture2D;
        Color32 now_selected = texture.GetPixelBilinear(normalized.x, normalized.y);

        return now_selected;
    }

    public void mousePointerDown() {
        SelectColor();
    }

    public void mouseDrag() {
        SelectColor();
    }

    public void ChangeColor() {
        string query;
        switch (type) {
            case 1:
                query = ChangeTopColor();
                break;
            case 2:
                query = ChangeBottomColor();
                break;
            case 3:
                // 아이콘 배경 변경
                query = ChangeIconBackColor();
                break;
            case 4:
                // 아이콘 색 변경
                query = ChangeIconColor();
                break;

            default:
                query = "";
                break;
        }

        gameObject.SetActive(false);
        //Debug.Log(query);
        modifyDB.ControllDB(query, "setting");

        // 외부 이미지 reload
        Canvas.GetComponent<SettingPageLoad>().LoadPage();
    }

    private string ChangeTopColor() {
        // 변경으로 선택한 색을 background에 대입
        Background_Main.GetComponent<SpriteRenderer>().material.SetColor("_Color01", change_color);
        Background_Footer.GetComponent<SpriteRenderer>().material.SetColor("_Color02", change_color);

        query = UtilityHub.query_builder.Append("UPDATE setting SET value='")
                                        .Append(change_color.r)
                                        .Append(",")
                                        .Append(change_color.g)
                                        .Append(",")
                                        .Append(change_color.b)
                                        .Append("' WHERE id=1")
                                        .ToString();
        UtilityHub.query_builder.Clear();

        PlayerPrefs.SetString("background_top", change_color.ToString());

        //Debug.Log(PlayerPrefs.GetString("background_top"));

        return query;
    }

    private string ChangeBottomColor() {
        // 변경으로 선택한 색을 background에 대입
        Background_Main.GetComponent<SpriteRenderer>().material.SetColor("_Color02", change_color);
        Background_Footer.GetComponent<SpriteRenderer>().material.SetColor("_Color01", change_color);

        query = UtilityHub.query_builder.Append("UPDATE setting SET value='")
                                        .Append(change_color.r)
                                        .Append(",")
                                        .Append(change_color.g)
                                        .Append(",")
                                        .Append(change_color.b)
                                        .Append("' WHERE id=2")
                                        .ToString();
        UtilityHub.query_builder.Clear();

        PlayerPrefs.SetString("background_bottom", change_color.ToString());
        //Debug.Log(PlayerPrefs.GetString("background_bottom"));

        return query;
    }

    private string ChangeIconBackColor() {
        Icon_Back.SetColor("_MainColor", change_color);

        query = UtilityHub.query_builder.Append("UPDATE setting SET value='")
                                        .Append(change_color.r)
                                        .Append(",")
                                        .Append(change_color.g)
                                        .Append(",")
                                        .Append(change_color.b)
                                        .Append("' WHERE id=3")
                                        .ToString();
        UtilityHub.query_builder.Clear();

        PlayerPrefs.SetString("icon_background", change_color.ToString());

        return query;
    }
    private string ChangeIconColor() {
        Icon.SetColor("_MainColor", change_color);

        query = UtilityHub.query_builder.Append("UPDATE setting SET value='")
                                        .Append(change_color.r)
                                        .Append(",")
                                        .Append(change_color.g)
                                        .Append(",")
                                        .Append(change_color.b)
                                        .Append("' WHERE id=4")
                                        .ToString();
        UtilityHub.query_builder.Clear();

        PlayerPrefs.SetString("icon", change_color.ToString());

        return query;
    }
    public void ChangeColorExit() {
        // 커서 및 색 원래로 돌림

        gameObject.SetActive(false);
    }
    public void PickerOpen() {
        // now img 설정
        switch (type) {
            case 1:
                Now_Img.color = Background_Main.GetComponent<SpriteRenderer>().material.GetColor("_Color01");
                break;
            case 2:
                Now_Img.color = Background_Main.GetComponent<SpriteRenderer>().material.GetColor("_Color02");
                break;
            case 3:
                Now_Img.color = Icon_Back.GetColor("_MainColor");
                break;
            case 4:
                Now_Img.color = Icon.GetColor("_MainColor");
                break;
        }
        Change_Img.color = Now_Img.color;
        // 커서 중앙위치
        Cursor.transform.position = new(0, 0, 0);
    }

}
