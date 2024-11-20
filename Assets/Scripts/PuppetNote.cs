using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetNote : Note
{
    public Animator MonsterAnimator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //MonsterAnimator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (transform.position.x <= 40)
        {
            MonsterAnimator.enabled = true;

        }
    }


}
