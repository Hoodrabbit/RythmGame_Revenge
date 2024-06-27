using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
}

public enum GameState
{
    None,
    Debug_Mode,
    Play_Mode
};