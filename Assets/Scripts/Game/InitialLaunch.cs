using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class InitialLaunch : MonoBehaviour
{
    private ParticleSystem[] m_particleSystems;
    private Rigidbody m_rigidbody;

    //public ParticleSystem m_centralExplosion;

    [Header("Launch Values")]
    public float m_timeToMoveUp;
    public float m_timeToMoveDown;

    public Vector3 m_startPositionAtLaunchMoveUp;
    public Vector3 m_endPositionAtLaunchMoveUp;

    public Vector3 m_startPositionAtLaunchMoveDown;
    public Vector3 m_endPositionAtLaunchMoveDown;

    //public AnimationCurve m_cameraZoomOnLaunch;
    public AnimationCurve m_speedOnLaunchMoveUp;
    public AnimationCurve m_speedOnLaunchMoveDown;

    // Transition values
    float m_currentLerpTime;
    float m_startLerpTime;
    float m_lerpDuration;

    Vector3 m_startPosition;
    Vector3 m_endPosition;

    AnimationCurve m_movementCurveSpeed;

    Action transitionEndCallback;

    private void Awake()
    {
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLerpTime = Mathf.Clamp01((Time.time - m_startLerpTime) / m_lerpDuration);

        transform.position = Vector3.Lerp(m_startPosition, m_endPosition, m_movementCurveSpeed.Evaluate(m_currentLerpTime));
        
        if (IsPositionsNear(gameObject.transform.position, m_endPosition, 0.01f))
        {
            transitionEndCallback();
        }
    }

    public void SetMoveUpTransitionValues()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_timeToMoveUp;

        m_startPosition = m_startPositionAtLaunchMoveUp;
        m_endPosition = m_endPositionAtLaunchMoveUp;

        m_movementCurveSpeed = m_speedOnLaunchMoveUp;

        transform.position = m_startPositionAtLaunchMoveUp;

        transitionEndCallback = StartGame;
    }

    public void SetMoveDownTransitionValues()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_timeToMoveDown;
        
        m_startPosition = m_startPositionAtLaunchMoveDown;
        m_endPosition = m_endPositionAtLaunchMoveDown;

        m_movementCurveSpeed = m_speedOnLaunchMoveDown;

        transform.position = m_startPositionAtLaunchMoveDown;

        transitionEndCallback = GameManager.Instance.StartNextLevel;
    }

    private void StartGame()
    {
        // Stop the firework launcher
        m_rigidbody.velocity = Vector3.zero;
        
        foreach (var player in GameManager.Instance.Players)
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
