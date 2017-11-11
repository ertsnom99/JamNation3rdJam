﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject FollowingPlayer { get; private set; }

    private ParticleSystem[] followingPlayerParticleSystems;
    private int ColliderInCount = 0;

    private void Start()
    {
        ParticleSystem[] particleSystems = FollowingPlayer.GetComponentsInChildren<ParticleSystem>();
        List<ParticleSystem> particleTrailSystem = new List<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.tag == GameManager.PARTICLE_TRAIL_TAG) particleTrailSystem.Add(particleSystem);
        }

        followingPlayerParticleSystems = particleTrailSystem.ToArray();
    }

    public void SetFollowingPlayer(GameObject followingPlayer)
    {
        FollowingPlayer = followingPlayer;
    }

    public void NotifyColliderEntered()
    {
        //Stop particule trail emittion
        if (ColliderInCount == 0)
        {
            foreach (ParticleSystem followingPlayerParticleSystem in followingPlayerParticleSystems)
            {
                followingPlayerParticleSystem.Play();
            }
        }

        ColliderInCount++;
    }

    public void NotifyColliderExited()
    {
        ColliderInCount--;

        //Stop particule trail emittion
        if (ColliderInCount == 0)
        {
            foreach (ParticleSystem followingPlayerParticleSystem in followingPlayerParticleSystems)
            {
                followingPlayerParticleSystem.Stop();
            }
        }
    }
}
