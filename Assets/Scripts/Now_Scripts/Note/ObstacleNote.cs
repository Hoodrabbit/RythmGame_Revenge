using UnityEngine;

public class ObstacleNote : Note
{
    Animator obstacleAnimator;
    bool floorCheck = false;

    protected override void Start()
    {
        base.Start();
        obstacleAnimator = GetComponent<Animator>();
        if (transform.position.y < 0)
        {
            floorCheck = true;
            Debug.Log("�۵�");
            if(floorCheck)
            {
                obstacleAnimator.Play("SlicedObstacleRotate");
            }
            
        }


    }



}
