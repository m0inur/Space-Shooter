using UnityEngine;
using UnityEngine.UI;

public class DestroyByBoundary : MonoBehaviour {
    private GameController gameController;
    public GameObject healthBar;
    private Slider slider;

    private void Start () {
        slider = healthBar.GetComponent<Slider> ();
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController> ();
        } else {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.tag == "Asteroid") {
            slider.value -= 1;
        } else if (other.tag == "EnemyShip") {
            slider.value -= 2;
        }

        if (slider.value == 0) {
            gameController.GameOver ();
        }
        Destroy (other.gameObject);
    }
}