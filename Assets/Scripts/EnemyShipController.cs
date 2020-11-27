using System.Collections;
using System.Timers;
using UnityEngine;

public class EnemyShipController : MonoBehaviour {
    #region Initialize Game Objects
    public GameObject shipExplosion;
    public GameObject shot;
    public Transform shotSpawner;
    GameController gameController;
    GameObject player;
    Rigidbody rb;
    #endregion
    #region Initialize Other Variables
    public float fireRate;
    bool canShoot;
    public float speed;
    public int score;
    #endregion

    #region Initialize Objects Values

    // Start is called before the first frame update
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        player = GameObject.FindWithTag ("Player");
        rb = GetComponent<Rigidbody> ();

        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController> ();
        } else {
            Debug.Log ("Cannot find 'GameController' script");
        }
        canShoot = true;
        rb.velocity = transform.forward * speed;
    }

    #endregion
    #region Collision Detection

    // Detect collision
    void OnTriggerEnter (Collider other) {
        // If the collider is not player nor bolt
        if (other.tag != "Player" && other.tag != "PlayerBolt") {
            return;
        }
        gameController.AddScore (score);
        Instantiate (shipExplosion, transform.position, transform.rotation);
        // If collided with player end the game
        if (other.tag == "Player") {
            Instantiate (shipExplosion, player.transform.position, player.transform.rotation);
            gameController.GameOver ();
        }

        Destroy (other.gameObject);
        Destroy (gameObject);
    }

    #endregion
    #region Shoot
    // Shoot
    void Update () {
        if (canShoot) {
            GetComponent<AudioSource> ().Play ();
            Instantiate (shot, shotSpawner.position, shotSpawner.rotation);
            canShoot = false;
            StartCoroutine (FireDelay ());
        }
    }

    // Delay for 'fireRate' seconds before shooting
    IEnumerator FireDelay () {
        yield return new WaitForSeconds (fireRate);
        canShoot = true;
    }
    #endregion
}