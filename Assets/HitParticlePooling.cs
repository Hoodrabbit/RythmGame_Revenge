using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HitParticlePooling : MonoBehaviour
{
    [SerializeField] private ParticleSystem AttackParticle;

    [SerializeField] private int poolsize = 100;
    //[SerializeField] private int poolsize_Down = 20;

    private Queue<ParticleSystem> particlePool = new Queue<ParticleSystem>();


    private void Start()
    {
        for(int i=0; i< poolsize; i++)
        {
            ParticleSystem particle = Instantiate(AttackParticle,transform);
            particle.gameObject.SetActive(false);
            particlePool.Enqueue(particle);
            
        }
        

    }

    public ParticleSystem GetParticle(Vector3 position)
    {
        if (particlePool.Count == 0)
        {
            ParticleSystem newParticle = Instantiate(AttackParticle, transform);
            particlePool.Enqueue(newParticle);
        }

        ParticleSystem particle = particlePool.Dequeue();
        particle.transform.position = position;
        particle.gameObject.SetActive(true);
        particle.Play();

        StartCoroutine(ReturnToPool(particle));
        return particle;
    }

    private IEnumerator ReturnToPool(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);
        particle.gameObject.SetActive(false);
        particlePool.Enqueue(particle);
    }
}

