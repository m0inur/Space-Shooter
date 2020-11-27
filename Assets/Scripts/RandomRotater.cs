using UnityEngine;

public class RandomRotater : MonoBehaviour {
    public float tumble;
    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody> ();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}