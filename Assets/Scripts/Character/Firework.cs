using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField]
    private GameManager.Colors fireworkColor;
    public GameManager.Colors FireworkColor
    {
        get { return fireworkColor; }
        set { fireworkColor = value; }
    }

    private ParticleSystem[] particleSystems;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void enabledParticuleSystem(bool enable)
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.main.loop)
            {
                if (enable)
                {
                    particleSystem.Play();
                    particleSystem.gameObject.SetActive(false);
                    particleSystem.gameObject.SetActive(true);
                }
                else
                {
                    particleSystem.Stop();
                    particleSystem.gameObject.SetActive(false);
                    particleSystem.gameObject.SetActive(true);
                }
            }
        }
    }
}
