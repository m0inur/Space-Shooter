using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    #region Init Game Objects
    public GameObject hazard;
    public GameObject enemyShip;
    #endregion
    #region Wait Time Coroutine Values
    public float hazardSpawnWait;
    public float startWait;
    public float waveWait;
    #endregion
    #region Boundary Values
    public float minX;
    public float maxX;
    public float z;
    #endregion
    #region Other Game Values
    private Quaternion enemyShipRotation = new Quaternion (180f, 0f, 0f, 0f);
    private Vector3 enemyShipSpawnPos;
    public int hazardCount;
    private bool gameOver;
    private int score;
    #endregion
    #region UI Objects
    public Button restartBtn;
    public Text gameOverText;
    public Text scoreText;
    #endregion
    #region Give Init Vars Values

    void Start () {
        gameOver = false;
        score = 0;

        gameOverText.text = "";
        restartBtn.gameObject.SetActive (false);

        StartCoroutine (SpawnWaves ());
    }
    #endregion

    #region Spawn Waves of Strikes
    IEnumerator SpawnWaves () {
        yield return new WaitForSeconds (startWait);

        while (!gameOver) {
            for (int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3 (Random.Range (minX, maxX), 0, z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (hazardSpawnWait);
            }

            enemyShipSpawnPos = new Vector3 (Random.Range (minX, maxX), 0, z + 5);
            Instantiate (enemyShip, enemyShipSpawnPos, enemyShipRotation);
            yield return new WaitForSeconds (waveWait);
        }
    }
    #endregion

    #region Add Score
    public void AddScore (int val) {
        score += val;
        scoreText.text = "Score: " + score;
    }
    #endregion

    #region Game Over
    public void GameOver () {
        StopCoroutine (SpawnWaves());
        gameOverText.text = "Game Over";
        restartBtn.gameObject.SetActive (true);
    }
    #endregion

    #region Go back to the lobby
    public void GoToMenu () {
        if (PlayerPrefs.GetInt ("Highscore") < score) {
            PlayerPrefs.SetInt ("Highscore", score);
        }
        // Go back to menu
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }
    #endregion
}