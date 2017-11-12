using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    public Vector3 m_initialDirection;
    public float m_currentSpeed;
    public float m_thresholdUpdate;

    private Rigidbody m_rigidbody;
    private Vector3 m_currentDirection;

    public bool IsFollowingCurve { get; private set; }
    public float NotFollowingCurveMaxDistance { get; private set; }

    private float currentNotFollowingCurveDistance = 0.0f;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    public void StartMovement(bool start)
    {
        m_rigidbody.velocity = start ? (m_initialDirection * m_currentSpeed) : Vector3.zero;
    }

    public void UpdateMovement(Hashtable inputs)
    {
        if (IsOverThreshold((float)inputs["horizontalInput"]) || IsOverThreshold((float)inputs["verticalInput"]))
        {
            m_currentDirection.Set((float)inputs["horizontalInput"], (float)inputs["verticalInput"], 0.0f);
            m_currentDirection.Normalize();

            m_rigidbody.velocity = m_currentDirection * m_currentSpeed;
        }

        if (!IsFollowingCurve)
        {
            currentNotFollowingCurveDistance += m_currentSpeed * Time.deltaTime;
        }
    }
    
    private bool IsOverThreshold(float value)
    {
        return Mathf.Abs(value) >= m_thresholdUpdate;
    }

    /***
     * Debug
     **/

    /// <summary>
    /// Display a debug message with the given vector
    /// </summary>
    /// <param name="vec"></param>
    public static void DebugVector(Vector3 vec)
    {
        Debug.Log(string.Format("{0}, {1}, {2}", vec.x, vec.y, vec.z));
    }
    /// <summary>
    /// Display a debug message with the given vector
    /// </summary>
    /// <param name="vec"></param>
    public static void DebugVector(Vector2 vec)
    {
        Debug.Log(string.Format("{0}, {1}", vec.x, vec.y));
    }

    public void SaveCurrentNotFollowingCurveDistance()
    {
        if (IsWorstScore())
        {
            NotFollowingCurveMaxDistance = currentNotFollowingCurveDistance;
        }
    }
    
    public void ResetNotFollowingCurveDistance()
    {
        currentNotFollowingCurveDistance = 0.0f;
        NotFollowingCurveMaxDistance = 0.0f;
    }

    public void SetIsFollowingCurve(bool isFollowing)
    {
        if (JustStopFollowingPath(isFollowing))
        { 
            currentNotFollowingCurveDistance = 0.0f;
        }
        else if(JustStartFollowingPath(isFollowing) && IsWorstScore())
        {
            NotFollowingCurveMaxDistance = currentNotFollowingCurveDistance;
        }

        IsFollowingCurve = isFollowing;
    }

    private bool JustStopFollowingPath(bool isFollowing)
    {
        return (!isFollowing
                && IsFollowingCurve);
    }

    private bool JustStartFollowingPath(bool isFollowing)
    {
        return (isFollowing
                && !IsFollowingCurve);
    }

    private bool IsWorstScore()
    {
        return (currentNotFollowingCurveDistance > NotFollowingCurveMaxDistance);
    }
}
