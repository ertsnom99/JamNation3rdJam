using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollider : MonoBehaviour
{
    private Path parentPathScript;

    private void Awake()
    {
        parentPathScript = GetComponentInParent<Path>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameManager.PLAYER_TAG && other.gameObject == parentPathScript.FollowingPlayer)
        {
            parentPathScript.NotifyColliderEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameManager.PLAYER_TAG && other.gameObject == parentPathScript.FollowingPlayer)
        {
            parentPathScript.NotifyColliderExited();
        }
    }
}
