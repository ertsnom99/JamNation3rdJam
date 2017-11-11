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

    private Rigidbody m_rigidbody;

    Vector3 m_startPosition;
    float m_currentLerpTime;
    float m_startLerpTime;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start()
    {
        m_startPosition = transform.position;
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLerpTime = Mathf.Clamp01((Time.time - m_startLerpTime) / m_timeToExplode);
        transform.position = Vector3.Lerp(m_startPosition, m_positionToExplode, m_currentLerpTime);

        if(IsPositionsNear(gameObject.transform.position, m_positionToExplode, 0.01f))
        {
            StartGame();
        }
    }

    public void ReplaceAtBeginning()
    {
        transform.position = m_positionTobegin;
    }

    private void StartGame()
    {
        // Stop the firework launcher
        m_rigidbody.velocity = Vector3.zero;

        // Activate every player
        foreach (var player in m_players)
        {
            player.SetActive(true);
        }

        // Hide firework launcher
        GetComponent<Renderer>().enabled = false;

        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach(ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }

        GameManager.Instance.CurrentLevel.StartLevelTimer();

        enabled = false;
    }

    private bool IsPositionsNear(Vector3 p1, Vector3 p2, float threshold)
    {
        return Vector3.Distance(p1, p2) <= threshold;
    }
}
