using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InfiniteLevelScroll_NoHide : MonoBehaviour
{
    public FadeController fade;
    public Vector3 targetScale = new Vector3(2f, 2f, 2f);
    public float duration = 1f;
    //level name and scene number variables
    //public TextMeshProUGUI level_name;

    public TextMeshProUGUI level_name_hover;
    //public TextMeshProUGUI scene_number;

    public GameObject[] levels;    // array for levels
    
    public float levelWidth = 10f; // level width
    public float moveSpeed = 10f;  // moving speed

    private int currentIndex = 0;           // start index
    private Vector3 targetOffset;         // moveing all levels
    private bool isMoving = false;

    void Start()
    {
    
    }
    void switchLevel()
    {
        if (currentIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(teleportCoroutine("level_1_intro"));
                //SceneManager.LoadScene("level_1");
            }
        }
        else if (currentIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(teleportCoroutine("level_2_intro"));
                //SceneManager.LoadScene("level_2");
            }
        }
         else if (currentIndex == 2)
        {
            //level_name.text = "final level";
            //scene_number.text = "scene 3";
        }
    }

    private IEnumerator teleportCoroutine(string levelName)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }

    void Update()
    {
        switchLevel();
        if (isMoving) return; // while moving = no raction

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentIndex != 2) // here level sum - 1
        {
            currentIndex += 1;
            targetOffset -= Vector3.right * levelWidth;
            StartCoroutine(MoveLevels(targetOffset));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex != 0) // nothing to here
        {
            currentIndex -= 1;
            targetOffset += Vector3.right * levelWidth;
            StartCoroutine(MoveLevels(targetOffset));
            Debug.Log(currentIndex);
        }
    }
    

    // smooth test
    System.Collections.IEnumerator MoveLevels(Vector3 target)
    {
        isMoving = true;

        // while current position is not finished target
        while (Vector3.Distance(levels[0].transform.position, target) > 0.01f)
        {
            foreach (GameObject level in levels)
            {
                level.transform.position = Vector3.MoveTowards(
                    level.transform.position,
                    level.transform.position + (target - levels[0].transform.position),
                    moveSpeed * Time.deltaTime
                );
            }
            yield return null;
        }

        // to target
        /*for (int i = 0; i < levels.Length; i++)
        {
            levels[i].transform.position = new Vector3(
                target.x + i * levelWidth,
                levels[i].transform.position.y,
                levels[i].transform.position.z
            );
        }*/

        isMoving = false;
    }
}
