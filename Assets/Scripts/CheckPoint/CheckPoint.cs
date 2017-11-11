using UnityEngine;

public class CheckPoint : MonoBehaviour
{
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
        checkPointManager = GetComponentInParent<CheckPointsManager>();
    }

    public void Activate()
    {
        CheckPointsManager.CheckPointGroupe checkPointGroupe = checkPointManager.GetCheckPointGroupeByCheckPoint(this);
        
        foreach(CheckPoint checkPoint in checkPointGroupe.checkPoints)
        {
            checkPoint.Explode();
        }
    }

    public void Explode()
    {
        GetComponent<SphereCollider>().enabled = false;

        Debug.Log("activate");
        //TODO do a particule effect
    }
}
