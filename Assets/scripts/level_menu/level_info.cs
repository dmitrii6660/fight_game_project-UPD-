using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class level_info : MonoBehaviour
{

    public TextMeshProUGUI level_name;

    public TextMeshProUGUI scene_number_hover;

    public TextMeshProUGUI scene_number;

    private int currentIndex;

    void switchValues()
    {
        if (currentIndex == 0)
        {
            level_name.text = "training";
            scene_number.text = "scene 0";
            scene_number_hover.text = "scene 0";
        }
        else if (currentIndex == 1)
        {
            //switch level name text
            level_name.text = "phone calls";
            scene_number.text = "scene 1";
            scene_number_hover.text = "scene 1";
        }
        else if (currentIndex == 2)
        {
            level_name.text = "down under";
            scene_number.text = "scene 2";
            scene_number_hover.text = "scene 2";
        }
         else if (currentIndex == 3)
        {
            level_name.text = "all ends here";
            scene_number.text = "scene 3";
            scene_number_hover.text = "scene 3";
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switchValues();

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentIndex != 3) // here level sum - 1
        {
            currentIndex += 1;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex != 0)
        {
            currentIndex -= 1;
        }
        
    }
}
