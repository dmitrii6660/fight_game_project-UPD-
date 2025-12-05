using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class get_points : MonoBehaviour
{
    public TextMeshProUGUI points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points.text = "Points " + level_storage.points;
    }
}
