using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private GameManager.Colors checkPointColor;
    public GameManager.Colors CheckPointColor
    {
        get { return checkPointColor; }
        set { checkPointColor = value; }
    }

    public GameObject FollowingPlayer { get; private set; }
    private PlayerMovement followingPlayerMovement;

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
        followingPlayerMovement = followingPlayer.GetComponent<PlayerMovement>();
    }

    public void NotifyColliderEntered()
    {
        //Stop particule trail emittion
        if (ColliderInCount == 0)
        {
            foreach (ParticleSystem followingPlayerParticleSystem in followingPlayerParticleSystems)
            {
                followingPlayerParticleSystem.Play();
                followingPlayerMovement.SetIsFollowingCurve(true);
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
                if (followingPlayerParticleSystem.name != "HeadTrail")
                {
                    followingPlayerParticleSystem.Stop();
                }
            }
            followingPlayerMovement.SetIsFollowingCurve(false);
        }
    }
}
