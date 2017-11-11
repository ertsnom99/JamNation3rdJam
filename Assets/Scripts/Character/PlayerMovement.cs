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

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        m_rigidbody.velocity = m_initialDirection * m_currentSpeed;
    }

    public void UpdateMovement(Hashtable inputs)
    {
        if (IsOverThreshold((float)inputs["horizontalInput"]) || IsOverThreshold((float)inputs["verticalInput"]))
        {
            m_currentDirection.Set((float)inputs["horizontalInput"], (float)inputs["verticalInput"], 0.0f);
            m_currentDirection.Normalize();

            m_rigidbody.velocity = m_currentDirection * m_currentSpeed;
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
}
