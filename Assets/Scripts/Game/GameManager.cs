using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // Tags
    public const string PLAYER_TAG = "Player";
    public const string PATH_COLLIDER_TAG = "PathCollider";
    public const string CHECK_POINT_TAG = "CheckPoint";
}
