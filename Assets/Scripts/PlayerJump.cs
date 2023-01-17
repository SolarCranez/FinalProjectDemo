using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerJump : MonoBehaviour
{
    // variables relevant to player
    public Rigidbody playerRb, _rightFoot, _leftFoot;
    private Animator playerAnim;
    private AudioSource playerAudio;

    // variables for jumping
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    // audio variables
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // particle variables
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;


    // Start is called before the first frame update

    void Start()
    {
        // get player animator and audiosource
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // check if player's touching ground or on an object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // player jumps when spacebar pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isOnGround = false;
            Debug.Log("spacebar pressed!");
            //playerAnim.SetTrigger("Jump_trig");
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
            //dirtParticle.Stop();
        }
    }
}
