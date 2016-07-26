using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    private Rigidbody rb;
    public GameObject explosion, playerExplosion;

    public int scoreValue;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();

        if (gameController == null)
            Debug.Log("Cannot find 'GameController' script");
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundry") || other.CompareTag("Enemy"))
        {
            return;
        }

        if(explosion != null)
             Instantiate(explosion, transform.position, transform.rotation);

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(rb.gameObject);
    }
}
