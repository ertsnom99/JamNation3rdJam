using UnityEngine;

public class CheckPointActivator : MonoBehaviour
{
    private Firework fireworkScript; 

    private GameObject checkPointInRange;

    private void Start()
    {
        fireworkScript = GetComponent<Firework>();
    }

    public void ActivateCheckPoint()
    {
        if (checkPointInRange != null)
        {
            checkPointInRange.GetComponent<CheckPoint>().Activate();
            checkPointInRange = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanActivateCheckPoint(other))
        {
            checkPointInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == checkPointInRange)
        {
            checkPointInRange = null;
        }
    }

    private bool CanActivateCheckPoint(Collider other)
    {
        return (other.tag == GameManager.CHECK_POINT_TAG
               && other.GetComponent<CheckPoint>().CheckPointColor == fireworkScript.FireworkColor);
    }
}
