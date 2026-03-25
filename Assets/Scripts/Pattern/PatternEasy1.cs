using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEasy1 : Pattern1
{
    public override void SetPatternData(PlayerMovement player, CoinSpawning coinSpawning, FeverSystem feverSystem)
    {
        base.SetPatternData(player, coinSpawning, feverSystem);

        Debug.Log("PatternEasy1 Initialized ✅");

        // 👉 add extra behavior here if needed
    }

    public override Vector3 GetHighestPos()
    {
        return base.GetHighestPos();
    }
}