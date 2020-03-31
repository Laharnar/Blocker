using UnityEngine;

public class ParticleEmmision:MonoBehaviour {
    public ParticleSystem particles;

    public void ParticlesOn()
    {
        var emission = particles.emission;
        emission.enabled = true;
    }
    public void ParticlesOff()
    {
        var emission = particles.emission;
        emission.enabled = false;
    }
}
