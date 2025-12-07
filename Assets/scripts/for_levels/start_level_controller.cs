/* Tämä scripti on tarkoitettu alku asetukseille, esimerkiksi fade effect*/

using UnityEngine;

public class start_level_controller : MonoBehaviour
{
    public FadeController fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
