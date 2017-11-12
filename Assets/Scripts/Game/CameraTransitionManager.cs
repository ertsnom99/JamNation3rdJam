using System;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    private Camera cameraComponent;

    [Header("Intro Values")]
    public float m_MoveToIntroTime;
    public float m_MoveToGameTime;

    public Vector3 m_introPosition;
    public Vector3 m_gamePosition;

    public AnimationCurve m_speedToGame;
    public AnimationCurve m_speedToIntro;
    public AnimationCurve m_zoomIntro;

    public float m_introFOVAtIntro;
    public float m_gameFOVAtIntro;

    [Header("Launch Values")]
    public float m_launchTimeToMoveUp;
    public float m_launchTimeToMoveDown;

    public Vector3 m_lauchStartPosition;
    public Vector3 m_launchEndPosition;

    public AnimationCurve m_speedOnLaunch;
    public AnimationCurve m_zoomOnLaunch;
    
    public float m_startFOVAtLaunch;
    public float m_endFOVAtLaunch;

    // Transition values
    private float m_currentLerpTime;
    private float m_startLerpTime;
    private float m_lerpDuration;

    private Vector3 m_startPosition;
    private Vector3 m_endPosition;

    private AnimationCurve m_movementCurveSpeed;
    private AnimationCurve m_cameraCurveZoom;

    private float m_startFOV;
    private float m_endFOV;

    private Action transitionEndCallback;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    private void Update()
    {
        m_currentLerpTime = Mathf.Clamp01((Time.time - m_startLerpTime) / m_lerpDuration);

        transform.position = Vector3.Lerp(m_startPosition, m_endPosition, m_movementCurveSpeed.Evaluate(m_currentLerpTime));
        cameraComponent.fieldOfView = Mathf.Lerp(m_startFOV, m_endFOV, m_cameraCurveZoom.Evaluate(m_currentLerpTime));

        if (transitionEndCallback != null && IsPositionsNear(gameObject.transform.position, m_endPosition, 0.01f))
        {
            transitionEndCallback();
        }
    }

    private bool IsPositionsNear(Vector3 p1, Vector3 p2, float threshold)
    {
        return Vector3.Distance(p1, p2) <= threshold;
    }

    public void SetMoveToIntroTransition()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_MoveToIntroTime;
        
        m_startPosition = transform.position;
        m_endPosition = m_introPosition;

        m_movementCurveSpeed = m_speedToIntro;
        m_cameraCurveZoom = m_zoomIntro;

        m_startFOV = cameraComponent.fieldOfView;
        m_endFOV = m_introFOVAtIntro;

        transitionEndCallback = GameManager.Instance.EndGame;
    }

    public void SetMoveToGameTransition()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_MoveToGameTime;

        m_startPosition = m_introPosition;
        m_endPosition = m_gamePosition;

        m_movementCurveSpeed = m_speedToGame;
        m_cameraCurveZoom = m_zoomIntro;

        m_startFOV = m_introFOVAtIntro;
        m_endFOV = m_gameFOVAtIntro;

        transitionEndCallback = GameManager.Instance.StartNextLevel;
    }

    public void SetMoveUpLaunchTransition()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_launchTimeToMoveUp;

        m_startPosition = m_lauchStartPosition;
        m_endPosition = m_launchEndPosition;

        m_movementCurveSpeed = m_speedOnLaunch;
        m_cameraCurveZoom = m_zoomOnLaunch;

        m_startFOV = m_startFOVAtLaunch;
        m_endFOV = m_endFOVAtLaunch;

        transitionEndCallback = null;
    }

    public void SetMoveDownLaunchTransition()
    {
        m_currentLerpTime = 0.0f;
        m_startLerpTime = Time.time;
        m_lerpDuration = m_launchTimeToMoveDown;

        m_startPosition = m_launchEndPosition;
        m_endPosition = m_lauchStartPosition;

        m_movementCurveSpeed = m_speedOnLaunch;
        m_cameraCurveZoom = m_zoomOnLaunch;

        m_startFOV = m_endFOVAtLaunch;
        m_endFOV = m_startFOVAtLaunch;

        transitionEndCallback = null;
    }
}
