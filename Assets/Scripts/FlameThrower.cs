using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private HpBar hpBar;
    public ParticleSystem particles;
    public Collider2D colliderToResize;
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        // Listen for when particles are emitted and resize the collider accordingly
        particles.trigger.SetCollider(0, colliderToResize);

        // Adjust the collider size initially
        ResizeCollider();
    }
    private void Update()
    {
        // Update the collider size each frame
        ResizeCollider();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ship")
        {
            hpBar.SetHealth(0.5f);
        }
    }
    private void ResizeCollider()
    {
        ParticleSystem.Particle[] particleArray = new ParticleSystem.Particle[particles.main.maxParticles];
        int particleCount = particles.GetParticles(particleArray);

        if (particleCount > 0)
        {
            Bounds bounds = new Bounds(particleArray[0].position, Vector3.zero);

            for (int i = 1; i < particleCount; i++)
            {
                bounds.Encapsulate(particleArray[i].position);
            }

            // Resize the collider to match the bounds
            //colliderToResize.transform.position = bounds.center;
            colliderToResize.transform.localScale = bounds.size;
        }
    }
}
