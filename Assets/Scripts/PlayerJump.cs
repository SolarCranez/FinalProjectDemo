using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerJump : MonoBehaviour
{
    public Rigidbody playerRb, _rightFoot, _leftFoot;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    //public void Jump()
    //{
    //    if(PlayerInput.)
    //}
    // Start is called before the first frame update

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

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
