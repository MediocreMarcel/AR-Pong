using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreEntry
{
    public HighScoreEntry(int score, GameMode mode) =>
    (Score, Mode) = (score, mode);

    public int Score { get; }
    public GameMode Mode { get; }
}
