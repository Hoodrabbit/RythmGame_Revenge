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

public enum NoteType
{
    Normal,

    White,

    Dark

};

public enum BossNoteType
{
    Appear,

    Disappear,

    Dash_Normal,

    Dash_Long,

    Dash_Disappear


}


