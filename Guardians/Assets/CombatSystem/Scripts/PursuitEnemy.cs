using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2;
using UnityEngine;

public class PursuitEnemy : MonoBehaviour
{
    private Rabbit parent;
    private float speed = 7f;
    private Vector3 dir;
    
    void Start()
    {
        parent = gameObject.GetComponentInParent<Rabbit>();
        if (parent.statsSO._childItemImage is not null)
            GetComponent<SpriteRenderer>().sprite = parent.statsSO._childItemImage;
    }

    // Update is called once per frame
    void Update()
    {
        dir = parent.enemy.gameObject.transform.position - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime));
        
        if (Vector3.Distance(parent.enemy.gameObject.transform.position,transform.position) < 1f)
        {
            Destroy(gameObject);
        }
    }
}
