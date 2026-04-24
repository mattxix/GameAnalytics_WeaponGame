using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;



public class RayCastFromPlayer : MonoBehaviour
{

    public float raycastDistance = 5.0f;
    bool holdingItem = false;
    GameObject heldObject;

    public bool redBox = false;
    public bool blueBox = false;
    public GameObject doorButton;

    public Animator leftDoor;
    public Animator rightDoor;

    bool doorUnlocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
        if (redBox && blueBox)
        {
            doorButton.GetComponent<Renderer>().material.color = Color.green;
            doorUnlocked = true;
        }
        else
        {
            doorButton.GetComponent<Renderer>().material.color = Color.red;
            doorUnlocked = false;
        }
    }

    public void PickUpItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("PickupItem"))
                {
                    hit.collider.GetComponent<PickupObject>().PickUp();
                    holdingItem = true;
                    heldObject = hit.collider.gameObject;
                    //hit.collider.tag = "Untagged";
                }

            }
        }
        if (ctx.canceled)
        {
            if(!holdingItem)
            {
                heldObject.GetComponent<PickupObject>().PickUp();
            }
        }
    }

    public void interactableObject(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag("DoorButton") && doorUnlocked)
                {
                    leftDoor.SetTrigger("OpenDoor");
                    rightDoor.SetTrigger("OpenDoor");

                }

            }
        }
    }

}
