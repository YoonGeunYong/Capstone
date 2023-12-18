using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnit : Conditional
{
    public override TaskStatus OnUpdate()
    {
        foreach(MiniMapTile tile in MiniMap.instance.miniMapTiles)
        {
            if(tile.enemyUnitsOnTile.Count > 0)
            {

                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
