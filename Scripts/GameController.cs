using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int cnt;
    public float spawnWait, startWait,waveWait;

    public GUIText scoreText, restartText, gameOverText;
    private bool gameOver, restart, pause;
    private int score;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameOver = false;
        restart = false;
        pause = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine( SpawnWaves());
    }

    void Update()
    {
        if(restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if(!pause)
                {
                    Time.timeScale = 0;
                    audioSource.Pause();
                    pause = true;
                }
                else
                {
                    Time.timeScale = 1;
                    audioSource.Play();
                    pause = false;
                }
            }
                
        }
        
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < cnt; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
                
        }
        
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
