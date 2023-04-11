using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Vector3 ballResetPosition;
    private MoveBall MoveballScript;
    private UIManager uiManagerScript;
    private DestroyBlock destroyBlockScript;
    private int lives;
    private int initialLives;
    public float ballSpeed = 1;

    private bool isPlaying = false;
    private bool isPaused = false;
    

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        initialLives = 3;
        uiManagerScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        MoveballScript = GameObject.Find("Ball").GetComponent<MoveBall>();
        destroyBlockScript = GameObject.Find("Block").GetComponent<DestroyBlock>();
        ballResetPosition = new Vector3(MoveballScript.transform.position.x, MoveballScript.transform.position.y, MoveballScript.transform.position.z);
        uiManagerScript.UpdateLives(lives);
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (isPaused)
        {
            if (Input.anyKeyDown && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))) // This line is used to detect only keys from the keyboard
            {
            UnpauseGame();
            }
        }
    }

    // Method used to reset the ball and update the number of lifes on screen
    public void BallLost()
    {
        MoveballScript.transform.position = ballResetPosition;
        MoveballScript.ResetVelocity();

        MoveballScript.combo = 1;
        lives--;
        uiManagerScript.UpdateLives(lives);
        if (lives < 0)
        {
            Debug.Log("Game Over");
            uiManagerScript.ShowGameOverPanel();
            isPlaying = false;
            PauseGame();
        }
    }

    // We make the game start and multiply the speed of the ball by the difficulty the user chose
    public void StartGame(float difficulty)
    {
        isPlaying = true;
        ballSpeed *= difficulty;
        ResetGame();
    }

    // We use this method to reset every variable in the game
    public void ResetGame()
    {
        lives = initialLives;
        uiManagerScript.score = 0;
        uiManagerScript.UpdateLives(lives);
        uiManagerScript.UpdateScore(uiManagerScript.score);
        uiManagerScript.HideStartPanel();
        uiManagerScript.HideGameOverPanel();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        if (isPlaying)
        {
            uiManagerScript.ShowPausePanel();
        }

    }

    public void UnpauseGame()
    {
        if (isPlaying)
        {
            isPaused = false;
            Time.timeScale = 1;
            uiManagerScript.HidePausePanel();
        }

    }

    // To quit the game and restart it from the beginning we reload the scene
    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
