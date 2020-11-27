using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    #region Init Game Objects
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    #endregion

    public int score;
    
    #region Give Variables Values
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController> ();
        } else {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }
    #endregion

    #region Collision Detection
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "boundary" || other.tag == "EnemyBolt" || other.tag == "EnemyShip") {
            return;
        }

        Instantiate (explosion, transform.position, transform.rotation);

        if (other.tag == "Player") {
            Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver ();
        } else {
            gameController.AddScore (score);
        }

        Destroy (other.gameObject);
        Destroy (gameObject);
    }
    #endregion
}