using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public Rigidbody buttonTopRigid;
    // top position of button
    public Transform buttonTop;
    // lower limit that button can be pressed to
    public Transform buttonLowerLimit;
    // upper limit the button can return to
    public Transform buttonUpperLimit;
    // threshold value between lower/upper limit that determines whether button is pressed or released
    public float threshHold;
    // force applied to return the button to original state
    public float force = 10;
    // used with threshold
    private float upperLowerDiff;
    // is the button pressed
    public bool isPressed;
    // previous state of button
    private bool prevPressedState;
    // audio sounds
    public AudioSource pressedSound;
    public AudioSource releasedSound;
    // colliders to ignore
    public Collider[] CollidersToIgnore;
    // event that occurs on press
    public UnityEvent onPressed;
    // event that occurs on release
    public UnityEvent onReleased;

    // Start is called before the first frame update
    void Start()
    {
        Collider localCollider = GetComponent<Collider>();

        // ignore collision between bottom of button and top of button
        if (localCollider != null)
        {
            Physics.IgnoreCollision(localCollider, buttonTop.GetComponentInChildren<Collider>());

            foreach (Collider singleCollider in CollidersToIgnore)
            {
                Physics.IgnoreCollision(localCollider, singleCollider);
            }
        }

        // set upperLowerDiff -- calculated differently whether the button is at an angle or not
        if (transform.eulerAngles != Vector3.zero)
        {
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        } 
        else
        {
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
        }
    }

    // when button is in pressed state
    void Pressed()
    {
        prevPressedState = isPressed;
        pressedSound.pitch = 1;
        pressedSound.Play();
        onPressed.Invoke();
    }

    // when button is in released state
    void Released()
    {
        prevPressedState = isPressed;
        releasedSound.pitch = Random.Range(1.1f, 1.2f);
        releasedSound.Play();
        onReleased.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // set top of button to have clamped X and Z position (can only move up and down)
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
        // set local angle to be 0
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);

        // check if button top's y position is >= 0 in order to set back to upper limit
        if (buttonTop.localPosition.y >= 0)
        {
            buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        }
        else
        {
            buttonTopRigid.AddForce(buttonTop.transform.up * force * Time.deltaTime);
        }

        // check if button's lower limit is >=top position in order to set back to lower limit
        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)
        {
            buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);
        }

        // if physically within the threshold of being pressed
        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
        {
            isPressed = true;
        } 
        // else not pressed
        else
        {
            isPressed = false;
        }

        if (isPressed && prevPressedState != isPressed)
        {
            Pressed();
        }

        if (!isPressed && prevPressedState != isPressed)
        {
            Released();
        }
    }
}
