using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class dialog : MonoBehaviour
{
    public GameObject player;
    movement playerMove;

    Rigidbody2D playerRB;
    private bool inTrigger = false;
    public TextMeshProUGUI dialogUI;

    public string[] dialogText;
    private int currentDialog = 0;

    private void OnTriggerEnter2D()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMove.enabled = false;
        inTrigger = true;
    }

    private void dialogChange()
    {
        if(currentDialog < dialogText.Length)
        {
            currentDialog += 1;
            dialogUI.text = dialogText[currentDialog];
        }
        else
        {
            playerRB.constraints = RigidbodyConstraints2D.None;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerMove.enabled = true;
            gameObject.SetActive(false);
            dialogUI.text = "";
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMove = player.GetComponent<movement>();
        dialogUI.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && inTrigger == true)
        {
            dialogChange();
        }
    }
}
