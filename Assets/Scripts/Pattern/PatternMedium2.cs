using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMedium2 : Pattern1
{
    [SerializeField] List<ColdWind> coldWindList = new List<ColdWind> ();
    [SerializeField] List<WarmWind1> warmWindList = new List<WarmWind1>();
    // Start is called before the first frame update
    public override void SetPatternData(PlayerMovement player, CoinSpawning coinSpawning, FeverSystem feverSystem)
    {
        base.SetPatternData(player, coinSpawning, feverSystem);

        FeatherScript[] feathers = GetComponentsInChildren<FeatherScript>();

        foreach (var c in coldWindList)
        {
            c.SetData(player, coinSpawning, feverSystem);
        }

        WarmWind1[] warmWinds = GetComponentsInChildren<WarmWind1>();
        ColdWind[] coldWinds = GetComponentsInChildren<ColdWind>();

        foreach (var w in warmWindList)
        {
            w.SetData(player, coinSpawning, feverSystem);
        }
    }

    // Update is called once per frame
    public override Vector3 GetHighestPos()
    {
        return base.GetHighestPos();
    }
}
