using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class InitialLaunch : MonoBehaviour
{
    public float m_timeToExplode;
    public Vector3 m_positionTobegin;
    public Vector3 m_positionToExplode;
    public List<GameObject> m_players = new List<GameObject>();
    public AnimationCurve m_cameraZoomOnLaunch;
    public AnimationCurve m_fireworkSpeedOnLaunch;

    private ParticleSystem[] m_particleSystems;
    private Rigidbody m_rigidbody;
    private Camera m_currentCamera;

    // Camera
    public float m_startFOV;
    public float m_endFOV;

    // Firework
    Vector3 m_startPosition;
    float m_currentLerpTime;
    float m_startLerpTime;

    private void OnEnable()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
    }

    private void Awake()
    {
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_currentCamera = GetComponentInChildren<Camera>();
    }

    // Use this for initialization
    void Start()
    {
        m_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLerpTime = Mathf.Clamp01((Time.time - m_startLerpTime) / m_timeToExplode);

        transform.position = Vector3.Lerp(m_startPosition, m_positionToExplode, m_fireworkSpeedOnLaunch.Evaluate(m_currentLerpTime));
        m_currentCamera.fieldOfView = Mathf.Lerp(m_startFOV, m_endFOV, m_cameraZoomOnLaunch.Evaluate(m_currentLerpTime));
        
        if (IsPositionsNear(gameObject.transform.position, m_positionToExplode, 0.01f))
        {
            StartGame();
        }
    }

    public void ReplaceAtBeginning()
    {
        Debug.Log(m_positionTobegin);
        transform.position = m_positionTobegin;
    }

    private void StartGame()
    {
        // Stop the firework launcher
        m_rigidbody.velocity = Vector3.zero;
        
        foreach (var player in m_players)
        {
            // Enable the movement of the player
            player.GetComponent<PlayerControls>().EnableMovementControls(true);

            // Enable all particule system
            player.GetComponent<Firework>().enabledParticuleSystem(true);
        }

        // Stop all particule system
        EnableParticleEmission(false);

        GameManager.Instance.CurrentLevel.StartLevelTimer();

        enabled = false;
    }

    public void EnableParticleEmission(bool enable)
    {
        foreach (ParticleSystem particleSystem in m_particleSystems)
        {
            if (particleSystem.main.loop)
            {
                if (enable)
                {
                    particleSystem.Play();
                }
                else
                {
                    particleSystem.Stop();
                }
            }
        }
    }

    private bool IsPositionsNear(Vector3 p1, Vector3 p2, float threshold)
    {
        return Vector3.Distance(p1, p2) <= threshold;
    }


    public Vector3 Sinerp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.z, end.z, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }
}
