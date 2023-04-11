using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    private GameManager gameManager;
    public float difficulty;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // To call the StartGame method while passing as an argument the difficulty the user select
    public void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
