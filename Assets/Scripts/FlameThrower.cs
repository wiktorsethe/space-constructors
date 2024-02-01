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
        // Nas³uchuj, kiedy cz¹steczki zostan¹ wyemitowane, i dostosuj odpowiednio rozmiar kolidera 
        particles.trigger.SetCollider(0, colliderToResize);
    }
    private void Update()
    {
        // Aktualizuj rozmiar kolidera w ka¿dej klatce
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
        // Pobierz tablicê cz¹steczek
        ParticleSystem.Particle[] particleArray = new ParticleSystem.Particle[particles.main.maxParticles];
        int particleCount = particles.GetParticles(particleArray);

        if (particleCount > 0)
        {
            // Utwórz granicê obejmuj¹c¹ wszystkie cz¹steczki ognia
            Bounds bounds = new Bounds(particleArray[0].position, Vector3.zero);

            for (int i = 1; i < particleCount; i++)
            {
                bounds.Encapsulate(particleArray[i].position);
            }
            // Dostosuj rozmiar kolidera, aby pasowa³ do granicy
            colliderToResize.transform.localScale = bounds.size;
        }
    }
}
