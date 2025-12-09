/* Tämä scripti on tarkoitettu alku asetukseille, esimerkiksi fade effect*/

using UnityEngine;

public class start_level_controller : MonoBehaviour
{
   // public GameObject end;
    public FadeController fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //end.SetActive(false);
        fade.FadeOut();
        //Cursor.visible = false;       
        //Cursor.lockState = CursorLockMode.None; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
