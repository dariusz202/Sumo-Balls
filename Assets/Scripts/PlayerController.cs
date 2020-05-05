using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour 
{
    public float horizontalInput;
    public float verticalInput;
    public float ballSpeed = 5.0f;
    private float powerUpStrength = 7.07f;
    private float projectilesStrength = 10.0f;
    private Quaternion startrotation;

    public bool pickPowerUp = false;
    public bool pickSpeedBoost = false;
    public bool pickJumpBoost = false;
    public bool pickProjectiles = false;
    public bool isOnGround = true;

    private GameObject focalPoint;
    public GameObject powerUpIndicate;
    public GameObject speedBoostIndicate;
    public GameObject jumpBoostIndicate;
    public GameObject projectilesIndicate;
    private GameObject enemy;

    private Rigidbody playerRB;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startrotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            verticalInput = Input.GetAxis("Vertical");
            playerRB.AddForce(focalPoint.transform.forward * verticalInput * ballSpeed);


            powerUpIndicate.transform.position = transform.position;
            speedBoostIndicate.transform.position = transform.position;
            jumpBoostIndicate.transform.position = transform.position + new Vector3(0, 1.5f, 0);
            projectilesIndicate.transform.position = transform.position;

            if (pickProjectiles && Input.GetKeyDown(KeyCode.Space))
            {
                enemy = GameObject.Find("Enemy(Clone)");
                Rigidbody enemyRigidbody = enemy.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = enemy.transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * projectilesStrength, ForceMode.Impulse);
                pickProjectiles = false;
                projectilesIndicate.gameObject.SetActive(false);
            }

            if (pickJumpBoost && Input.GetKeyDown(KeyCode.Space))
            {
                playerRB.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);

            }
            if (transform.position.y < -5)
            {
                if (gameManager.lives > 1)
                {
                    transform.position = new Vector3(0, 5f, 0);
                    transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                    verticalInput = -Input.GetAxis("Vertical");
                }
                gameManager.UpdateLives();
                pickPowerUp = false;
                pickSpeedBoost = false;
                pickJumpBoost = false;
                pickProjectiles = false;
                projectilesIndicate.gameObject.SetActive(false);
                powerUpIndicate.gameObject.SetActive(false);
                speedBoostIndicate.gameObject.SetActive(false);
                jumpBoostIndicate.gameObject.SetActive(false);
            }
        }



    }
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        pickPowerUp = false;
        powerUpIndicate.gameObject.SetActive(false);
    }
    IEnumerator SpeedBoostCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        pickSpeedBoost = false;
        ballSpeed = 5.0f;
        speedBoostIndicate.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && pickJumpBoost == false && pickProjectiles == false && pickSpeedBoost == false && pickPowerUp == false)
        {
            Destroy(other.gameObject);
            pickPowerUp = true;
            powerUpIndicate.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }
        if (other.CompareTag("SpeedBoost") && pickJumpBoost == false && pickProjectiles == false && pickSpeedBoost == false && pickPowerUp == false)
        {
            Destroy(other.gameObject);
            pickSpeedBoost = true;
            ballSpeed = 10.0f;
            speedBoostIndicate.gameObject.SetActive(true);
            StartCoroutine(SpeedBoostCountDownRoutine());
        }
        if (other.CompareTag("Projectiles") && pickJumpBoost == false && pickProjectiles == false && pickSpeedBoost == false && pickPowerUp == false)
        {
            Destroy(other.gameObject);
            pickProjectiles = true;
            projectilesIndicate.gameObject.SetActive(true);
        }

        if (other.CompareTag("JumpBoost") && pickJumpBoost == false && pickProjectiles == false && pickSpeedBoost == false && pickPowerUp == false)
        {
            Destroy(other.gameObject);
            pickJumpBoost = true;
            jumpBoostIndicate.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && pickPowerUp )
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log(pickPowerUp);

        }
        if (collision.gameObject.CompareTag("Ground") && pickJumpBoost)
        {
            Enemy[] enemyList = GameObject.FindObjectsOfType<Enemy>(); ;
            for (var i = 0; i < enemyList.Length; i++)
            {
                Rigidbody enemyRigidbody = enemyList[i].gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = enemyList[i].transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * projectilesStrength, ForceMode.Impulse);
            }
            pickJumpBoost = false;
            jumpBoostIndicate.gameObject.SetActive(false);

        }
    }

}
