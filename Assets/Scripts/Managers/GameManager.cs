using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Puzzle Puzzle;

    public void StartGame()
    {
        Puzzle.StartGame("Gift");
    }
}
