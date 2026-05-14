using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class ColorSettingLoad : Singleton<ColorSettingLoad>
{

    [SerializeField] private Material background;
    [SerializeField] private Material background_footer;
    [SerializeField] private Material icon_background;
    [SerializeField] private Material icon;

    public void ColorSetLoad() {
        //SetOption tmp;
        string[] colorField;
        Color32 set_color;
        // background
        /*
        tmp = datahub.Setoption[0] as SetOption;
        colorField = tmp.Value.Split(',');
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        */
        colorField = PlayerPrefs.GetString("background_top").Split(',');
        //Debug.Log(PlayerPrefs.GetString("background_top"));
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        background.SetColor("_Color01", set_color);
        background_footer.SetColor("_Color02", set_color);

        /*
        tmp = datahub.Setoption[1] as SetOption;
        colorField = tmp.Value.Split(",");
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255 );
        */
        colorField = PlayerPrefs.GetString("background_bottom").Split(',');
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        background.SetColor("_Color02", set_color);
        background_footer.SetColor("_Color01", set_color);

        // icon background
        /*
        tmp = datahub.Setoption[2] as SetOption;
        colorField = tmp.Value.Split(",");
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        */
        colorField = PlayerPrefs.GetString("icon_background").Split(',');
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        icon_background.SetColor("_MainColor", set_color);

        // icon
        /*
        tmp = datahub.Setoption[3] as SetOption;
        colorField = tmp.Value.Split(",");
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        */
        colorField = PlayerPrefs.GetString("icon").Split(',');
        set_color = new(byte.Parse(colorField[0]),
                        byte.Parse(colorField[1]),
                        byte.Parse(colorField[2]),
                        255);
        icon.SetColor("_MainColor", set_color);
    }
}
