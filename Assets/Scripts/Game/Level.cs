using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private float time = 5.0f;

    [SerializeField]
    private Path[] paths;

    private void Start()
    {
        // Associate each player to it's path
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i].SetFollowingPlayer(GameManager.Instance.Players[i]);
        }

        GameManager.Instance.InitialLaunch.enabled = true;
    }

    public void StartLevelTimer()
    {
        StartCoroutine(WaitForLevelEnd());
    }

    IEnumerator WaitForLevelEnd()
    {
        yield return new WaitForSeconds(time);
        EndLevel();
    }

    private void EndLevel()
    {
        Debug.Log("The level ended");
        // Check if anyplayer falled to reproduce the curve
    }
}
