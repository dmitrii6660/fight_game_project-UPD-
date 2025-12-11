/* 
tämä scripti on tarkoitettu tasojen valitsemiseen
*/


using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InfiniteLevelScroll_NoHide : MonoBehaviour
{
    public FadeController fade; //tumennus
    public Vector3 targetScale = new Vector3(2f, 2f, 2f);
    public float duration = 1f;

    public TextMeshProUGUI level_name_hover;
    //public TextMeshProUGUI scene_number;

    public GameObject[] levels;    // tasojen array
    
    public float levelWidth = 10f; // kuinka paljon tasot voivat liikkua
    public float moveSpeed = 10f;  // liikuvuus nopeus

    private int currentIndex = 0; // alku indexi
    private bool isMoving = false; // liikukko tasot nyt

    // koordinatit ja positions
    private Vector3 centerPos = Vector3.zero;
    private Vector3 rightPos;
    private Vector3 leftPos;

    void Start()
    {
        // laitetaan alku positiot
        rightPos = new Vector3(levelWidth, 0, 0);
        leftPos = new Vector3(-levelWidth, 0, 0);

        // laitetaan kaikki muut tasot piilon paitsi nykyinen taso
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == 0)
            {
                levels[i].transform.position = centerPos;
                levels[i].SetActive(true);
            }
            else
            {
                // muut tasot piilotetaan tai laitetaan niitä kauaksi
                levels[i].transform.position = leftPos; 
                levels[i].SetActive(false); 
            }
        }
    }
    void switchLevel()
    {
        if (currentIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("level 1");
                //StartCoroutine(teleportCoroutine("level_1_intro"));
                //SceneManager.LoadScene("level_1");
            }
        }
        else if (currentIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("level 2");
                StartCoroutine(teleportCoroutine("level_2_intro"));
                //SceneManager.LoadScene("level_2");
            }
        }
        else if (currentIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("level 3");
                StartCoroutine(teleportCoroutine("level_2_intro"));
                //SceneManager.LoadScene("level_2");
            }
        }
    }

    // coroutine jolla siirrytään tasolle
    private IEnumerator teleportCoroutine(string levelName) 
    {
        fade.FadeIn(); // tumennus effekti
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }

    void Update()
    {
        if (isMoving) return;

        switchLevel();

        // liikuminen oikealle
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // kotsotaan onko seuraava elementiä levels taulukossa
            if (currentIndex < levels.Length - 1)
            {
                StartCoroutine(SwitchSlide(currentIndex, currentIndex + 1, true));
                currentIndex++;
            }
        }

        // liikkuminen vasemalle
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // kotsotaan onko seuraava elementiä levels taulukossa
            if (currentIndex > 0)
            {
                StartCoroutine(SwitchSlide(currentIndex, currentIndex - 1, false));
                currentIndex--;
            }
        }
    }
    

    // tasojen luukkumis coroutine
    IEnumerator SwitchSlide(int oldIndex, int newIndex, bool moveRight)
    {
        isMoving = true;

        GameObject oldSprite = levels[oldIndex];
        GameObject newSprite = levels[newIndex];

        // taso joka tulee sauraavaksi
        newSprite.SetActive(true);
        
        // jos painetaan oikealle niin sitten tasot vaihtuvat
        Vector3 oldTarget = moveRight ? rightPos : leftPos;
        Vector3 startPosForNew = moveRight ? leftPos : rightPos;
        
        newSprite.transform.position = startPosForNew;

        // animaatio
        while (Vector3.Distance(newSprite.transform.position, centerPos) > 0.01f)
        {
            oldSprite.transform.position = Vector3.MoveTowards(oldSprite.transform.position, oldTarget, moveSpeed * Time.deltaTime);

            newSprite.transform.position = Vector3.MoveTowards(newSprite.transform.position, centerPos, moveSpeed * Time.deltaTime);

            yield return null; 
        }

        // laitetaan elementi keskelle
        newSprite.transform.position = centerPos;
        oldSprite.transform.position = oldTarget;
        
        // laitetaan jälkeinen taso piilon
        oldSprite.SetActive(false);

        // animaatio on ohi
        isMoving = false;
    }
      
}
