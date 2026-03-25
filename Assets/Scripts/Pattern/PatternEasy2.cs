using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEasy2 : Pattern1
{
    [SerializeField] List<Cloud> cloudList = new List<Cloud>();
    // Start is called before the first frame update
    public override void SetPatternData(PlayerMovement player, CoinSpawning coinSpawning, FeverSystem feverSystem)
    {
        base.SetPatternData(player, coinSpawning, feverSystem);

        Debug.Log("PatternEasy2 INIT");

        // get feathers (same as before)
        FeatherScript[] feathers = GetComponentsInChildren<FeatherScript>();

        foreach (var f in feathers)
        {
            f.SetData(player, coinSpawning, feverSystem);
        }

        // Cloud
        Cloud[] clouds = GetComponentsInChildren<Cloud>();

        foreach (var c in clouds)
        {
            c.SetData(player, coinSpawning, feverSystem);
        }
    }

    public override Vector3 GetHighestPos()
    {
        return base.GetHighestPos();
    }


}
