using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private float time = 5.0f;
    [SerializeField]
    private float maxCurveNotFollowedDistance = 15.0f;

    [SerializeField]
    private Path[] paths;

    private IEnumerator endLevelCoroutine;

    private CheckPointsManager checkPointsManager;

    private void Start()
    {
        checkPointsManager = GetComponentInChildren<CheckPointsManager>();

        // Associate each player to it's path
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i].SetFollowingPlayer(GameManager.Instance.Players[i]);
            GameManager.Instance.Players[i].GetComponent<PlayerMovement>().ResetNotFollowingCurveDistance();
        }

        SoundManager.Instance.PlayOneShotSound("SFXLaunchFirwork");
        
        GameManager.Instance.InitialLaunch.enabled = true;
        GameManager.Instance.CameraTransitionManager.enabled = true;

        checkPointsManager.ResetActivatedCheckPoint();
    }

    public void StartLevelTimer()
    {
        endLevelCoroutine = WaitForLevelEnd();
        StartCoroutine(endLevelCoroutine);
    }

    IEnumerator WaitForLevelEnd()
    {
        yield return new WaitForSeconds(time);
        EndLevel();
    }

    private void EndLevel()
    {
        if(!checkPointsManager.AreCheckPointAllActivated())
        {
Debug.Log("YOU LOST!!!");
            GameManager.Instance.CameraTransitionManager.SetMoveToIntroTransition();
            GameManager.Instance.CameraTransitionManager.enabled = true;
            return;
        }

        for (int i = 0; i < GameManager.Instance.Players.Length; i++)
        {
            PlayerMovement playerMovement = GameManager.Instance.Players[i].GetComponent<PlayerMovement>();

            // Make shure to check if the current not following curve distance is the new max distance
            playerMovement.SaveCurrentNotFollowingCurveDistance();
//Debug.Log(GameManager.Instance.Players[i].name  + ": " + playerMovement.NotFollowingCurveMaxDistance);
            // Check if a player falled
            if (playerMovement.NotFollowingCurveMaxDistance > maxCurveNotFollowedDistance)
            {
Debug.Log("YOU LOST!!!");
                GameManager.Instance.CameraTransitionManager.SetMoveToIntroTransition();
                GameManager.Instance.CameraTransitionManager.enabled = true;
                return;
            }
        }

        foreach (GameObject player in GameManager.Instance.Players)
        {
            // Diseable the movement of the player
            player.GetComponent<PlayerControls>().EnableMovementControls(false);

            // Disable all particule system
            player.GetComponent<Firework>().enabledParticuleSystem(false);

            // Replace the player at his starting point
            player.GetComponent<PlayerMovement>().ReplaceAtBeginning();
        }
        
        if (!GameManager.Instance.IsCurrentLevelLastLevel())
        {
            GameManager.Instance.InitialLaunch.EnableParticleEmission(true);

            GameManager.Instance.InitialLaunch.SetMoveDownTransitionValues();
            GameManager.Instance.CameraTransitionManager.SetMoveDownLaunchTransition();

            GameManager.Instance.InitialLaunch.enabled = true;
            GameManager.Instance.CameraTransitionManager.enabled = true;
        }
        else
        {
Debug.Log("ALL LEVEL DONE!!!");
            GameManager.Instance.CameraTransitionManager.SetMoveToIntroTransition();
            GameManager.Instance.CameraTransitionManager.enabled = true;
        }
    }

    public void FailLevel()
    {
        StopCoroutine(endLevelCoroutine);

Debug.Log("YOU LOST!!!");
        GameManager.Instance.CameraTransitionManager.SetMoveToIntroTransition();
        GameManager.Instance.CameraTransitionManager.enabled = true;
    }
}
