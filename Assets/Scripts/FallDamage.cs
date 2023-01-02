using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void calculateFallDamage(float fallingTime)
    {
        if(fallingTime >= 1.5f)
        {
            int causedDamage;

            causedDamage = (int)(10*(fallingTime * fallingTime));
            GetComponent<TakeDamage>().calculateDamage(causedDamage, true);
            Debug.Log("Fall damage: "+causedDamage+" Fall time: "+fallingTime);
        }
    }
}
