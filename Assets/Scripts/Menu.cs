using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Text highscoreText;

    private void Start () {
        highscoreText.text = PlayerPrefs.GetInt ("Highscore").ToString ();
    }
    public void StartGame () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
}