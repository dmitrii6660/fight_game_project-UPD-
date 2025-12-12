/* Tämä scripti on tarkoitettu alku asetukseille, esimerkiksi fade effect*/

using UnityEngine;

public class start_level_controller : MonoBehaviour
{
   // public GameObject end;
    public FadeController fade;

    public bool hideCursor = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level_storage.isLevelCleared = false;
        level_storage.points = 0;
        if(hideCursor == true)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        fade.FadeOut();
    }
}
