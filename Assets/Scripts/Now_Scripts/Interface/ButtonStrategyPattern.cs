using System;
using UnityEngine;



//제발 클래스에 인터페이스를 상속시키자..
interface IButton 
{
    string ButtonName { get; }

    void Click();

}


class Back : IButton
{
    readonly string name = "Back";

    public string ButtonName => name;

    public void Click()
    {

    }

}

class PlayMusic : IButton
{
    //음악 재생 버튼

    readonly string name = "PlayMusic";

    public string ButtonName => name;

    public void Click()
    {

        Debug.Log("음악 재생해요");
        UIManager.Instance.PlayMusic();


        //QuitPanel 정보를 들고 있는 다른 스크립트에서 해당 QuitPanel 정보를 받아온다음 그 패널을 열어줌
    }

}

class StopMusic : IButton
{
    //음악 일시정지 버튼

    readonly string name = "StopMusic";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.StopMusic();
    }

}

class AddBeatLine : IButton
{
    readonly string name = "AddBeatLine";

    public string ButtonName => name;

    public void Click()
    {
        EditManager.Instance.ActivateBeatLine();
    }
}



class MakeNormalNote : IButton
{
    readonly string name = "MakeNormalNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_NormalNote();
    }

}

class MakeMediumNote : IButton
{
    readonly string name = "MakeMediumNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_MiddleNote();
    }

}



class MakeLongNote : IButton
{
    readonly string name = "MakeLongNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_LongNote();
    }

}

class MakeGhostNote : IButton
{
    readonly string name = "MakeGhostNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_GhostNote();
    }

}

class MakeObstacle : IButton
{
    readonly string name = "MakeObstacle";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_Obstacle();
    }

}



class UsingKeyboard : IButton
{
    //UI매니저에서 키보드 사용체크 마우스 인터렉션 on 
   readonly string name = "UsingKeyboard";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.KeyBoardOperating();
    }


}

class UsingMouse : IButton
{
    //반대
    readonly string name = "UsingMouse";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.MouseOperating();
    }
}


class DeleteAllNotes : IButton
{
    readonly string name = "DeleteAllNotes";

    public string ButtonName => name;

    public void Click()
    {
        //모든 정보 초기화 
        DataManager.Instance.DestroyEverything();
    }
    

}



class SaveNote : IButton
{
   readonly string name = "SaveNote";

    public string ButtonName => name;

    public void Click()
    {
        DataManager.Instance.SaveNote();
    }

}

class LoadNote : IButton
{
   readonly string name = "LoadNote";

    public string ButtonName => name;

    public void Click()
    {
        DataManager.Instance.LoadNote();
    }

}


/*


    게임 내 여러 이벤트들의 생성 관련 버튼 클래스들



 */
class MakeBossAppearNote : IButton
{
   readonly string name = "MakeBossAppearNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_MakeBossAppearNote();
    }

}

class MakeBossDisappearNote : IButton
{
    readonly string name = "MakeBossDisappearNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_BossDisappearNote();
    }

}

class MakeBossDashNote : IButton
{
    readonly string name = "MakeBossDashNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_BossDashNote();
    }


}





class MakeEndEvent : IButton
{
    readonly string name = "MakeEndEvent";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_EndEvent();
    }
}

class MakeNoteOutSpawnEvent : IButton
{
    readonly string name = "MakeNoteOutSpawnEvent";
    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_NoteSpawnOutsideEvent();
    }

}

class MakeNoteOutSpawnReverseEvent : IButton
{
    readonly string name = "MakeNoteOutSpawnReverseEvent";
    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_NoteSpawnOutsideReverseEvent();
    }
}


class GamePauseButton : IButton
{
    readonly string name = "GamePauseButton";
    public string ButtonName => name;

    public void Click()
    {
        GameManager.Instance.PauseAudio();
        //UI버튼도 켜주기 
    }
}

class GameMenuPanelCloseButton : IButton
{
    readonly string name = "GameMenuPanelCloseButton";

    public string ButtonName => name;

    public void Click()
    {
        GameManager.Instance.PauseAudio();
        
    }


}














class ButtonContext
{
    IButton btn;

    public void SetButtonInfo(IButton button)
    {
        btn = button;
        Click();
    }


    public void Click()
    {
        btn.Click();
    }


}

class BtnController
{
    ButtonContext btnContext = new ButtonContext();

    public void SetButton(string buttonName)
    {
        Type interfaceType = typeof(IButton);

        Type type = Type.GetType(buttonName);

        if (type != null && interfaceType.IsAssignableFrom(type))
        {
            Debug.Log(type);
        }

        IButton btnGet = Activator.CreateInstance(type) as IButton;

        btnContext.SetButtonInfo(btnGet);

    }


}
//---------------------------------------------------------------------------------------------------------------------------
