using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameDataState
{
    Data_UnLoad,

    FinishData_Load,
}

public enum Scene_Type
{
    SelectSong,

    NoteEdit,

    OffsetMode,

    Play
};


public enum GameState
{
    None,

    Title,

    SongSelect_Mode,

    Offset_Mode,

    Debug_Mode,

    Play_Mode,

    Finish_Play
};

public enum JudgeMentState
{
    Perfect,

    Great,

    Good,

    Miss,

    Null
};

public enum MelodyType
{
    Normal,

    Yellow,

    Purple,

    Obstacle

};

public enum NoteHeight
{ 
    None,
    UP,
    DOWN,
    OUTSIDE_UP,
    OUTSIDE_DOWN,
    REVERSE_UP,
    REVERSE_DOWN
}





public enum BossNoteType
{
    Appear= 100,

    Disappear,

    Dash_Normal,

    Dash_Long,

    Dash_Disappear

}


//음악 마디의 가시성 상태에 따라 에디터 상에서 키를 눌렀을 때 어느 정도를 이동할 것인지 체크를 해줌
public enum BeatNoteLine_Visble_Status
{
    // 마디만 있는 경우(1박)
    OneNoteInBar, 

    // 마디 사이에 음표가 하나 있는 경우 (2박)
    TwoNoteInBar,
    
    // 마디 사이에 음표가 3개 있는 경우 (4박)
    FourNoteInBar,

    // 마디 사이에 음표가 7개 있는 경우 (8박)
    EightNoteInBar
}


public enum NoteEditOperatingState
{
    KeyBoard,
    Mouse
}


public enum EventType
{
    None,
    SpawnOutside,
    SpawnOutside_Reverse,
    End,

    Appear = 100,

    Disappear,

    Dash

    

}


