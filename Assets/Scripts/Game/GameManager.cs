using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum Colors { Blue, Green, Red, Yellow };

    // Tags
    public const string PLAYER_TAG = "Player";
    public const string PATH_COLLIDER_TAG = "PathCollider";
    public const string PARTICLE_TRAIL_TAG = "ParticleTrail";
    public const string CHECK_POINT_TAG = "CheckPoint";

    private int currentLevelIndex = 0;

    public Level CurrentLevel { get; private set; }

    [SerializeField]
    private GameObject[] players;
    public GameObject[] Players
    {
        get { return players; }
        private set { players = value; }
    }

    [SerializeField]
    private Level[] levels;

    [SerializeField]
    private InitialLaunch initialLaunch;
    public InitialLaunch InitialLaunch
    {
        get { return initialLaunch; }
        private set { initialLaunch = value; }
    }

    private void Start()
    {
        foreach (GameObject player in Players)
        {
            // Diseable the movement of the player
            player.GetComponent<PlayerControls>().EnableMovementControls(false);

            // Disable all particule system
            player.GetComponent<Firework>().enabledParticuleSystem(false);

            // Replace the player at his starting point
            player.GetComponent<PlayerMovement>().ReplaceAtBeginning();
        }

        StartNextLevel();
    }

    public void StartNextLevel()
    {
        if (currentLevelIndex != 0)
        {
            InitialLaunch.ReplaceAtBeginning();
            InitialLaunch.EnableParticleEmission(true);

            Destroy(CurrentLevel.gameObject);
        }

        CurrentLevel = Instantiate(levels[currentLevelIndex]);
        currentLevelIndex++;
    }

    public void StartNextLevelTest()
    {
        if (currentLevelIndex != 0)
        {
            InitialLaunch.ReplaceAtBeginning();
            InitialLaunch.EnableParticleEmission(true);

            Destroy(CurrentLevel.gameObject);
        }
        
        CurrentLevel = Instantiate(levels[currentLevelIndex]);
        //currentLevelIndex++;
    }
}
