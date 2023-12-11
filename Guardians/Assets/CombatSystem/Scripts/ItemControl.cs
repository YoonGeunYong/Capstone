using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    private Rabbit parent;
    private GameObject effect;
    private float speed = 7f;
    private Vector3 dir;
    
    void Start()
    {
        parent = gameObject.GetComponentInParent<Rabbit>();
        if (parent.statsSO._childItemImage is not null)
            GetComponent<SpriteRenderer>().sprite = parent.statsSO._childItemImage;
        else
        {
            foreach(var unitUI in MiniMap.instance.GetMiniMapTile().unitsOnTile)
            {
                unitUI.unit.stats.healthPoint += GameController.instance.preStats[(int)UnitTypes.Fairy]._stats.attack;
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        dir = parent.enemy.gameObject.transform.position - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime));
        
        if (Vector3.Distance(parent.enemy.gameObject.transform.position, transform.position) < 1f)
        {
            parent.enemy.stats.healthPoint -= parent.stats.attack;
            Destroy(gameObject);
        }
    }
}
