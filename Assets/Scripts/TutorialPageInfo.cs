using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialPageInfo : MonoBehaviour
{
    public List<Sprite> tutorialImageList = new List<Sprite>();
    public Image NowImage;


    public GameObject TutorialPage_Circle;
    public GameObject PageCheck_Panel;

    Color StartColor;

    public List<SpriteRenderer> PageCircle = new List<SpriteRenderer>();
    public int pageCount;



    // Start is called before the first frame update
    void Start()
    {

        for(int i=0; i<tutorialImageList.Count; i++)
        {
            PageCircle.Add(Instantiate(TutorialPage_Circle, PageCheck_Panel.transform).GetComponent<SpriteRenderer>());
        }

        pageCount = 0;
        StartColor = PageCircle[0].color;
        PageCircle[0].color = Color.white;
        NowImage.sprite = tutorialImageList[pageCount];
        //NowImage = GetComponent<Image>();
    }

    public void NextPage()
    {
        if(pageCount < tutorialImageList.Count) 
        {
            pageCount++;
            
            NowImage.sprite = tutorialImageList[(pageCount)];
            InitializePageColor(); PageCircle[pageCount].color = Color.white;
        }
    }

    public void PreviousPage()
    {
        Debug.Log(pageCount);

        if (pageCount > 0) 
        {
            Debug.Log("¿€µø");

            pageCount--;
            NowImage.sprite = tutorialImageList[(pageCount)];
            InitializePageColor(); PageCircle[pageCount].color = Color.white;
        }
    }

    public void InitializePageColor()
    {
        foreach(var sprite in PageCircle) 
        {
            sprite.color = StartColor;
        }
    }



}
