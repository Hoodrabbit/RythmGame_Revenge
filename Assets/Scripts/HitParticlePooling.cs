using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HitParticlePooling : MonoBehaviour
{
    Judgement nowjudge;

    [SerializeField] private GameObject Normal_p;
    //일반 노트 파티클
    [SerializeField] private ParticleSystem NormalAttackParticle;

    [SerializeField] private GameObject Long_p;
    //롱 노트 파티클
    [SerializeField] private ParticleSystem LongAttackParticle;


    [SerializeField] private int NormalNormalParticleQueuesize = 100;
    //[SerializeField] private int LongNoteParticleQueuesize = 3;

    //[SerializeField] private int NormalNormalParticleQueuesize_Down = 20;

    private Queue<ParticleSystem> NormalParticleQueue = new Queue<ParticleSystem>();

    //private Queue<ParticleSystem> LongParticleQueue = new Queue<ParticleSystem>();
    private ParticleSystem nowLongParticle;
    




    private void Start()
    {
        nowjudge = GetComponent<Judgement>();

        for (int i=0; i< NormalNormalParticleQueuesize; i++)
        {
            ParticleSystem particle = Instantiate(NormalAttackParticle, Normal_p.transform);
            particle.gameObject.SetActive(false);
            NormalParticleQueue.Enqueue(particle);
        }

        nowLongParticle = Instantiate(LongAttackParticle, Long_p.transform);
        nowLongParticle.transform.position = transform.position;
        nowLongParticle.gameObject.SetActive(false);

        //for(int i=0; i< LongNoteParticleQueuesize; i++)
        //{
        //    ParticleSystem particle = Instantiate(LongAttackParticle, Long_p.transform);
        //    particle.gameObject.transform.position = transform.position;
        //    particle.gameObject.SetActive(false);
        //    LongParticleQueue.Enqueue(particle);

        //    nowLongQueue = particle;

        //}

        nowjudge.PressEvent_Hit += GetNormalParticle;
        nowjudge.HoldingEvent += GetLongParticle;
        nowjudge.HoldingEndEvent += StopLongParticle;

        /*
         public Action<JudgementHeight_State> PressEvent_NoneHit;
    public Action<JudgementHeight_State> PressEvent_Hit;
    public Action<JudgementHeight_State> HoldingEvent;
    public Action<JudgementHeight_State> HoldingEndEvent;

         
         
         */




    }

    public void GetLongParticle(JudgementHeight_State state)
    {
        nowLongParticle.gameObject.SetActive(true);
        nowLongParticle.Play();
    }

    public void StopLongParticle(JudgementHeight_State state)
    {
        nowLongParticle.Stop();
        nowLongParticle.gameObject.SetActive(false);
    }






    public void GetNormalParticle(JudgementHeight_State state)
    {
        if (NormalParticleQueue.Count == 0)
        {
            ParticleSystem newParticle = Instantiate(NormalAttackParticle, transform);
            NormalParticleQueue.Enqueue(newParticle);
        }

        ParticleSystem particle = NormalParticleQueue.Dequeue();
        particle.transform.position = transform.position;
        particle.gameObject.SetActive(true);
        particle.Play();

        StartCoroutine(ReturnToPool(particle));
    }

    private IEnumerator ReturnToPool(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);
        particle.gameObject.SetActive(false);
        NormalParticleQueue.Enqueue(particle);
    }
}

