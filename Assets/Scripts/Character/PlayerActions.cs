using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public ParticleSystem m_burstEmitter;

    // Use this for initialization
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    public void UpdateActions(Hashtable inputs)
    {
        if((bool)inputs["cancelInput"])
        {
            m_burstEmitter.Stop();
            m_burstEmitter.Play();
        }
    }
}
