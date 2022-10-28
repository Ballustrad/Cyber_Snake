using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManagerBlack : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextMeshProScoreBUI;
    
    private int _scrB;
    
    public int ScoreB
    {
        get { return _scrB; }
        set
        {
            _scrB = value;
            TextMeshProScoreBUI.text = ScoreB.ToString();
        }
    }
    
}