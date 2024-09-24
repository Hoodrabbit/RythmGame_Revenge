using System;
using System.Reflection;
using UnityEngine;



//���� Ŭ������ �������̽��� ��ӽ�Ű��..
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
    //���� ��� ��ư

    readonly string name = "PlayMusic";

    public string ButtonName => name;

    public void Click()
    {

        Debug.Log("���� ����ؿ�");
        UIManager.Instance.PlayMusic();


        //QuitPanel ������ ��� �ִ� �ٸ� ��ũ��Ʈ���� �ش� QuitPanel ������ �޾ƿ´��� �� �г��� ������
    }

}

class StopMusic : IButton
{
    //���� �Ͻ����� ��ư

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
    //UI�Ŵ������� Ű���� ���üũ ���콺 ���ͷ��� on 
   readonly string name = "UsingKeyboard";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.KeyBoardOperating();
    }


}

class UsingMouse : IButton
{
    //�ݴ�
    readonly string name = "UsingMouse";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.MouseOperating();
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


    ���� �� ���� �̺�Ʈ���� ���� ���� ��ư Ŭ������



 */
class MakeBossAppearNote : IButton
{
   readonly string name = "MakeBossAppearNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_BossAppearNote();
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

class MakeEndEvent : IButton
{
    readonly string name = "MakeEndEvent";

    public string ButtonName => name;

    public void Click()
    {
        throw new NotImplementedException();
    }
}

class MakeNoteOutSpawnEvent : IButton
{
    readonly string name = "MakeNoteOutSpawnEvent";
    public string ButtonName => name;

    public void Click()
    {
        throw new NotImplementedException();
    }

}

class MakeNoteOutSpawnReverseEvent : IButton
{
    readonly string name = "MakeNoteOutSpawnReverseEvent";
    public string ButtonName => name;

    public void Click()
    {
        throw new NotImplementedException();
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
