using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{

    MonsterMove atest;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(StageManager.Inst.playerTransform.position.x - this.transform.position.x > 0)
        {
            
        }
        else
        {

        }
    }


}
