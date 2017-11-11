using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private Path[] paths;

    private void Start ()
    {
        for(int i = 0; i < paths.Length; i++)
        {
            paths[i].SetFollowingPlayer(players[i]);
        }
    }
}
