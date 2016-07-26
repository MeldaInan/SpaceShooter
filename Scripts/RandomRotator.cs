using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{ 
    public float tumble;
    private Rigidbody rb;
    public GameObject explosion, playerExplosion;

    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();

        if (gameController == null)
            Debug.Log("Cannot find 'GameController' script");

        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundry" || other.tag =="Enemy")
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if(other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
             
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(rb.gameObject);
    }
}
