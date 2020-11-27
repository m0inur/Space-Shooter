using System;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

#region Boundary
[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}
#endregion

public class PlayerController : MonoBehaviour {
    #region Initialize Game Objects
    // Init Game objects and positions
    private GameController gameController;
    public GameObject healthBar;
    public GameObject explosion;
    public Transform shotSpawn;
    public Boundary boundary;
    public GameObject shot;
    Rigidbody rb;
    Slider slider;
    #endregion
    #region Initialize Other Variables
    public float tilt;
    public float speed;
    public float fireRate;
    private float nextFire;
    private bool gameOver;
    #endregion

    #region Give Variables Values
    void Start () {
        rb = GetComponent<Rigidbody> ();
        slider = healthBar.GetComponent<Slider> ();
        gameOver = false;

        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController> ();
        } else {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }
    #endregion

    #region Shoot Shots
    void Update () {
        if (slider.value <= 0) {
            Debug.Log ("earth died");
            gameOver = true;
        }

        if (!gameOver && Input.GetButton ("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource> ().Play ();
        }
    }
    #endregion

    #region Move Player
    void FixedUpdate () {
        if (!gameOver) {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

            Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

            rb.velocity = movement * speed;
            rb.position = new Vector3 (Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));
            rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }
    #endregion

    #region Collsion Detection
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "EnemyBolt") {
            Instantiate (explosion, transform.position, transform.rotation);
            Destroy (gameObject);
            Destroy (other.gameObject);
            gameController.GameOver ();
        }
    }
    #endregion
}