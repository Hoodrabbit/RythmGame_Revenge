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
    //음악 재생 버튼

    string name = "PlayMusic";

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

//이벤트로 모든 버튼을 클릭했을 때 이벤트를 구독하면 좋긴 한데 그건 좀 빡셀 것 같음

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




//버튼마다 가지고 있는 스크립트 

//class ButtonButton
//{
//    Button button;
//    void Start()
//    {
//        button.addlistener(OnclickButton);
//    }

//    void OnclickButton()
//    {
//        //지금 버튼의 이름을 가져와서 버튼Context에 정보를 넘겨줘요
//        //그리고 바로 click함수 실행

//    }


//}