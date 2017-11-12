using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private AudioSource audioSource;
    private CheckPointsManager checkPointManager;

    [SerializeField]
    private GameManager.Colors checkPointColor;
    public GameManager.Colors CheckPointColor
    {
        get { return checkPointColor; }
        set { checkPointColor = value; }
    }

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        checkPointManager = GetComponentInParent<CheckPointsManager>();
    }

    public void Activate()
    {
        CheckPointsManager.CheckPointGroupe checkPointGroupe = checkPointManager.GetCheckPointGroupeByCheckPoint(this);
        
        foreach(CheckPoint checkPoint in checkPointGroupe.checkPoints)
        {
            checkPoint.Explode();
        }

        checkPointManager.IncrementeNbrActivatedCheckPoint();
    }

    public void Explode()
    {
        GetComponent<SphereCollider>().enabled = false;

        audioSource.Play();

        Debug.Log("activate");
        //TODO do a particule effect
    }
}
