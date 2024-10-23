using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour
{
    //VARIABLES

    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public float jumpForce;
    public bool isOnGround = true;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody rb;

    [Header("Shooting")]
    public GameObject projectile;
    public float shootDelay;
    public GameObject spawnPoint;
    private bool CanShoot = true;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement

        
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);


        //Clockwise and counterclockwise Rotation
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * moveSpeed * turnSpeed * Time.deltaTime);



        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) 
        {
            isOnGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        }



        //Shooting
        if (Input.GetKey(KeyCode.Q) && CanShoot)
        {
            CanShoot = false;
            Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            StartCoroutine(ShootDelay());
        }



    }
    //Make coroutine to delay shooting
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        CanShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
