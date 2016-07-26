using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed, tilt;
    public Boundry boundries;
    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
       
    }

	void FixedUpdate()
    {

        float moveHorizontal=Input.GetAxis("Horizontal");
        float moveVertical=Input.GetAxis("Vertical");

        Vector3 movement=new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity=movement*speed;

        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundries.xMin, boundries.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundries.zMin, boundries.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        
    }
}
