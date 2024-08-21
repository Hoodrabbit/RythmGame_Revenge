using System;
using System.Reflection;
using UnityEngine;

interface IButton
{
    string ButtonName { get; }

    void Click();

}
class PlayMusic : IButton
{
    //���� ��� ��ư

    string name = "PlayMusic";

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

    string name = "StopMusic";

    public string ButtonName => name;

    public void Click()
    {
        UIManager.Instance.StopMusic();
    }

}

class AddBeatLine : IButton
{
    string name = "AddBeatLine";

    public string ButtonName => name;

    public void Click()
    {
        EditManager.Instance.ActivateBeatLine();
    }
}

class MakeNormalNote : IButton
{
    string name = "MakeNormalNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_NormalNote();
    }

}

class MakeLongNote : IButton
{
    string name = "MakeLongNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_LongNote();
    }

}

class MakeGhostNote : IButton
{
    string name = "MakeGhostNote";

    public string ButtonName => name;

    public void Click()
    {
        NoteMaker_EditScene.instance.Instantiate_GhostNote();
    }

}

class SaveNote : IButton
{
    string name = "SaveNote";

    public string ButtonName => name;

    public void Click()
    {
        DataManager.Instance.SaveNote();
    }

}

class LoadNote : IButton
{
    string name = "LoadNote";

    public string ButtonName => name;

    public void Click()
    {
        DataManager.Instance.LoadNote();
    }

}








class Back : IButton
{
    string name = "Back";

    public string ButtonName => name;

    public void Click()
    {

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

//�̺�Ʈ�� ��� ��ư�� Ŭ������ �� �̺�Ʈ�� �����ϸ� ���� �ѵ� �װ� �� ���� �� ����

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




//��ư���� ������ �ִ� ��ũ��Ʈ 

//class ButtonButton
//{
//    Button button;
//    void Start()
//    {
//        button.addlistener(OnclickButton);
//    }

//    void OnclickButton()
//    {
//        //���� ��ư�� �̸��� �����ͼ� ��ưContext�� ������ �Ѱ����
//        //�׸��� �ٷ� click�Լ� ����

//    }


//}