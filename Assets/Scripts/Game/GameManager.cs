﻿using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum Colors { Blue, Green, Red, Yellow };

    // Tags
    public const string PLAYER_TAG = "Player";
    public const string PATH_COLLIDER_TAG = "PathCollider";
    public const string PARTICLE_TRAIL_TAG = "ParticleTrail";
    public const string CHECK_POINT_TAG = "CheckPoint";

    private int currentLevel = 0;

    public Level CurrentLevel { get; private set; }

    [SerializeField]
    private CameraTransitionManager cameraTransitionManager;
    public CameraTransitionManager CameraTransitionManager
    {
        get { return cameraTransitionManager; }
        private set { cameraTransitionManager = value; }
    }

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

    private bool IsMainScreen;

    private void Start()
    {
        InitialLaunch.EnableParticleEmission(false);

        IsMainScreen = true;
    }

    private void Update()
    {
        if (IsMainScreen && Input.GetButtonDown("Submit"))
        {
            IsMainScreen = false; 
            StartGame();
        }
    }

    private void StartGame()
    {
        currentLevel = 0;

        foreach (GameObject player in Players)
        {
            // Diseable the movement of the player
            player.GetComponent<PlayerControls>().EnableMovementControls(false);

            // Disable all particule system
            player.GetComponent<Firework>().enabledParticuleSystem(false);

            // Replace the player at his starting point
            player.GetComponent<PlayerMovement>().ReplaceAtBeginning();
        }

        InitialLaunch.SetMoveDownTransitionValues();
        InitialLaunch.EnableParticleEmission(true);
        InitialLaunch.enabled = true;

        CameraTransitionManager.SetMoveToGameTransition();
        CameraTransitionManager.enabled = true;
    }

    public void StartNextLevel()
    {
        currentLevel++;
        
        if (currentLevel != 1)
        {
            Destroy(CurrentLevel.gameObject);
        }

        InitialLaunch.SetMoveUpTransitionValues();
        InitialLaunch.EnableParticleEmission(true);
        CameraTransitionManager.SetMoveUpLaunchTransition();

        CurrentLevel = Instantiate(levels[currentLevel - 1]);        
    }

    public bool IsCurrentLevelLastLevel()
    {
        return (levels.Length == currentLevel);
    }

    public void EndGame()
    {
        IsMainScreen = true;
    }
}
