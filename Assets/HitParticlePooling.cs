using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HitParticlePooling : MonoBehaviour
{
    [SerializeField] private GameObject Normal_p;
    //일반 노트 파티클
    [SerializeField] private ParticleSystem NormalAttackParticle;

    [SerializeField] private GameObject Long_p;
    //롱 노트 파티클
    [SerializeField] private ParticleSystem LongAttackParticle;


    [SerializeField] private int NormalNormalParticleQueuesize = 100;
    [SerializeField] private int LongNoteParticleQueuesize = 3;

    //[SerializeField] private int NormalNormalParticleQueuesize_Down = 20;

    private Queue<ParticleSystem> NormalParticleQueue = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> LongParticleQueue = new Queue<ParticleSystem>();

    private void Start()
    {
        for(int i=0; i< NormalNormalParticleQueuesize; i++)
        {
            ParticleSystem particle = Instantiate(NormalAttackParticle, Normal_p.transform);
            particle.gameObject.SetActive(false);
            NormalParticleQueue.Enqueue(particle);
        }

        for(int i=0; i< LongNoteParticleQueuesize; i++)
        {
            ParticleSystem particle = Instantiate(LongAttackParticle, Long_p.transform);
            particle.gameObject.SetActive(false);
            LongParticleQueue.Enqueue(particle);
        }


    }

    public ParticleSystem GetLongParticle(Vector3 position)
    {
        //ParticleSystem LongnewParticle = Instantiate(LongAttackParticle, transform);
        //LongParticleQueue.Enqueue(LongnewParticle);

        ParticleSystem particle = LongParticleQueue.Dequeue();
        particle.transform.position = position;
        particle.gameObject.SetActive(true);
        particle.Play();

        return particle;
    }



    public ParticleSystem GetNormalParticle(Vector3 position)
    {
        if (NormalParticleQueue.Count == 0)
        {
            ParticleSystem newParticle = Instantiate(NormalAttackParticle, transform);
            NormalParticleQueue.Enqueue(newParticle);
        }

        ParticleSystem particle = NormalParticleQueue.Dequeue();
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
        NormalParticleQueue.Enqueue(particle);
    }
}

