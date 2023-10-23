using System;
using System.Collections.Generic;

public static class SkystoneModelFactory 
{
    public static List<SkystoneModel> CreateRandomSkystones(int count, int ownerIndex)
    {
        List<SkystoneType> possibleStones = new List<SkystoneType>();
        foreach(SkystoneType type in Enum.GetValues(typeof(SkystoneType)))
        {
            possibleStones.Add(type);
        }

        List<SkystoneModel> result = new List<SkystoneModel>();
        for(int i = 0;i < count; i++)
        {
            SkystoneType randomType = possibleStones[UnityEngine.Random.Range(0, possibleStones.Count)];
            result.Add(CreateSkystone(randomType, ownerIndex));
            possibleStones.Remove(randomType);
        }


        return result;
    }

    public static SkystoneModel CreateSkystone(SkystoneType type, int ownerIndex)
    {
        SkystoneModel result = new SkystoneModel();
        result.ownerIndex = ownerIndex;

        switch (type)
        {
            case SkystoneType.BlasterTroll:
                result.SetSpikes(0, 3, 0, 3);
                break;
            case SkystoneType.DrowLanceMaster:
                result.SetSpikes(0, 0, 3, 3);
                break;
            case SkystoneType.InHumanShield:
                result.SetSpikes(0, 3, 2, 2);
                break;
            case SkystoneType.MohawkCyclops:
                result.SetSpikes(0, 3, 0, 3);
                break;
            case SkystoneType.ChompyBot9000:
                result.SetSpikes(2, 2, 1, 1);
                break;
            case SkystoneType.DrowArcher:
                result.SetSpikes(3, 3, 0, 0);
                break;
            case SkystoneType.ArkeyanJouster:
                result.SetSpikes(3, 0, 2, 2);
                break;
            case SkystoneType.D_Riveter:
                result.SetSpikes(2, 2, 0, 3);
                break;
            case SkystoneType.JawBreaker:
                result.SetSpikes(2, 2, 3, 0);
                break;
            case SkystoneType.ArkeyanDuelist:
                result.SetSpikes(4, 0, 0, 0);
                break;
            case SkystoneType.BagOBomb:
                result.SetSpikes(0, 0, 4, 0);
                break;
            case SkystoneType.ShadowDuke:
                result.SetSpikes(0, 4, 0, 0);
                break;
            case SkystoneType.ArkeyanSniper:
                result.SetSpikes(0, 0, 0, 4);
                break;
            case SkystoneType.Axecutioner:
                result.SetSpikes(3, 3, 3, 3);
                break;
            case SkystoneType.ArmoredChompy:
                result.SetSpikes(3, 1, 1, 1);
                break;
            case SkystoneType.BlazerBrewer:
                result.SetSpikes(3, 2, 1, 2);
                break;
        }

        return result;
    } 
}
