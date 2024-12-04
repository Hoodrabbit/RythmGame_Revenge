using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NantaParticlePooling : MonoBehaviour
{
    //게임 씬에서만 사용되는 변수들
    //이렇게 쓰면 안되는데 큰일남

    [SerializeField] private Judgement UP;
    [SerializeField] private Judgement DOWN;


    [SerializeField] private GameObject particleParent;
    [SerializeField] private ParticleSystem HitParticle;
    [SerializeField] private int HitParticleQueuesize = 100;
    private Queue<ParticleSystem> HitParticleQueue = new Queue<ParticleSystem>();



    void Start()
    {
        if (GameManager.Instance.state == GameState.Play_Mode)
        {
            for (int i = 0; i < HitParticleQueuesize; i++)
            {
                ParticleSystem particle = Instantiate(HitParticle, particleParent.transform);
                particle.gameObject.SetActive(false);
                HitParticleQueue.Enqueue(particle);
            }
        }

        //UP.NantaHit += VisualizeNantaParticle;
        //DOWN.NantaHit += VisualizeNantaParticle;

    }

    public void VisualizeNantaParticle()
    {
        if (HitParticleQueue.Count == 0)
        {
            ParticleSystem newParticle = Instantiate(HitParticle, transform);
            HitParticleQueue.Enqueue(newParticle);
        }

        ParticleSystem particle = HitParticleQueue.Dequeue();
        particle.transform.position = transform.position;
        particle.gameObject.SetActive(true);
        particle.Play();

        StartCoroutine(ReturnToPool(particle));
    }

    private IEnumerator ReturnToPool(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);
        particle.gameObject.SetActive(false);
        HitParticleQueue.Enqueue(particle);
    }



}
