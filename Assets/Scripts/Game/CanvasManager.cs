using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject PL;
    [SerializeField] private TextMeshProUGUI VidaText;
    [SerializeField] private TextMeshProUGUI Pointstext;
    [SerializeField] private TextMeshProUGUI Distancetext;
    void Update()
    {
        VidaText.text = "Vida: " + PL.GetComponent<PlayerMovement>().player_lives.ToString();
        Pointstext.text = "Puntos: " + PL.GetComponent<PlayerMovement>().point.ToString();
        Distancetext.text = "Distance: " + PL.GetComponent<PlayerMovement>().Distance.ToString("F0");
    }
}
