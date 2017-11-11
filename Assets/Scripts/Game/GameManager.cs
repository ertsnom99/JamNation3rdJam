using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum Colors { Blue, Green, Red, Yellow };

    // Tags
    public const string PLAYER_TAG = "Player";
    public const string PATH_COLLIDER_TAG = "PathCollider";
    public const string PARTICLE_TRAIL_TAG = "ParticleTrail";
    public const string CHECK_POINT_TAG = "CheckPoint";

    /*[SerializeField]
    private InitialLauncher initialLauncher;*/

    private void Start()
    {
        //initialLauncher.enabled = true;
    }
}
