using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum Colors { Blue, Green, Red, Yellow };

    // Tags
    public const string PLAYER_TAG = "Player";
    public const string PATH_COLLIDER_TAG = "PathCollider";
    public const string PARTICLE_TRAIL_TAG = "ParticleTrail";
    public const string CHECK_POINT_TAG = "CheckPoint";

    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private Path[] paths;

    [SerializeField]
    private InitialLaunch initialLaunch;

    private void Start()
    {
        // Associate each playe rto it<s path
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i].SetFollowingPlayer(players[i]);
        }

        initialLaunch.enabled = true;
    }
}
