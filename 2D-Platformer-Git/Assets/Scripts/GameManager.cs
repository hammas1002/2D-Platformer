using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance != null) return instance;
            return FindObjectOfType<GameManager>(); }
            }

    public TMP_Text scoreText;


    [SerializeField]
    int score=0;
    private void Start()
    {
        instance = this;
        scoreText.text = score.ToString();
    }

    public void IncreaseCount()
    {
        score += 1;
        scoreText.text = score.ToString();
        
    }
}
