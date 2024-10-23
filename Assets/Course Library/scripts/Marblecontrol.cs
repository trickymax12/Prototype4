using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marblecontrol : MonoBehaviour
{
    private Rigidbody PlayerRb;
    public float Speed = 5.0f;
    private GameObject FocalPoint;
    public bool hasPowerup;
    public float PowerupStrength = 15.0f;
    public GameObject PowerupIndicator;
    public ParticleSystem powerup;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        FocalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        PlayerRb.AddForce(FocalPoint.transform.forward * Speed * forwardInput);
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            PowerupIndicator.gameObject.SetActive(true);
            powerup.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromplayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("collided with " + collision.gameObject.name + "with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromplayer * PowerupStrength, ForceMode.Impulse);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }
    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        PowerupIndicator.gameObject.SetActive(false);
        powerup.Stop();
        powerup.Clear();
    }
}
