using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class training : MonoBehaviour
{
    public GameObject target;
    public GameObject secondEnemy;
    public GameObject weapon;
    public GameObject firstEnemy;
    public string[] trainingTip;
    public GameObject trainingObj;
    public TextMeshProUGUI trainingUI;

    private int currentTraining = 0;

    private int currentTip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstEnemy.SetActive(false);
        secondEnemy.SetActive(false);
        weapon.SetActive(false);
        trainingUI.text = "hei, tässä sinua opetetaan miten pelataan tätä peliä, pain W,A,S,D liikkumiseen";
    }

    // Update is called once per frame
    void Update()
    {
        allTrainings();
    }

    private void allTrainings()
    {
        if(currentTraining == 0)
        {
            StartCoroutine(hold());
        }
        else if(currentTraining == 1)
        {
            destroyEnemy();
        }
        else if(currentTraining == 2)
        {
            destroyEnemyWithWeapon();
        }
    }

    private void trainingText()
    {
        if(currentTip < trainingTip.Length)
        {
            currentTip += 1;
            trainingUI.text = trainingTip[currentTip];
        }
    }

    private IEnumerator hold()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            yield return new WaitForSeconds(3f);
            trainingText();
            //Debug.Log("coroutine is finish");
            StopAllCoroutines();
            currentTraining += 1;
            firstEnemy.SetActive(true);
        }
    }

    private void destroyEnemy()
    {
        if(firstEnemy == null)
        {
            Debug.Log("enemy is destroyed");
            currentTraining += 1;
            trainingText();
            secondEnemy.SetActive(true);
            weapon.SetActive(true);
        }
    }

    private void destroyEnemyWithWeapon()
    {
        weapon.SetActive(true);
        if(secondEnemy == null)
        {
            trainingText();
            target.SetActive(true);
        }
    }
}
