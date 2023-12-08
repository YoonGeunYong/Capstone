using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    GameObject[] effect;
    void Start()
    {
        effect = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            effect[i] = transform.GetChild(i).gameObject;
        }
    }

    IEnumerator OnEffect()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var t in effect)
        {
            t.SetActive(true);
        }
        foreach(var unitUI in MiniMap.instance.GetMiniMapTile().unitsOnTile)
        {
            unitUI.unit.stats.healthPoint += GameController.instance.preStats[(int)UnitTypes.Fairy]._stats.attack;
        }
    }
}
