using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextMeshProScoreUI;
    
    private int _scr;
    
    public int Score
    {
        get { return _scr; }
        set
        {
            _scr = value;
            TextMeshProScoreUI.text = Score.ToString();
        }
    }
   
}