using UnityEngine;

public class TriggerPlate : MonoBehaviour
{
    public RayCastFromPlayer raycastScript;
    [SerializeField]
    public string objTag;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(objTag))
        {
            if (other.gameObject.name == "RedBox")
            {
                raycastScript.redBox = true;
            }
            if (other.gameObject.name == "BlueBox")
            {
                raycastScript.blueBox = true;
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(objTag))
        {
            if (other.gameObject.name == "RedBox")
            {
                raycastScript.redBox = false;
            }
            if (other.gameObject.name == "BlueBox")
            {
                raycastScript.blueBox = false;
            }

        }
    }

}
