using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public float PlayerCurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CalcVelocity());
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            var prevPos = transform.position;
            yield return new WaitForEndOfFrame();
            PlayerCurrentSpeed = (prevPos - transform.position).magnitude / Time.deltaTime;
            if (prevPos == transform.position)
            {
                PlayerCurrentSpeed = 0;
            }

            if (PlayerController.MovementControl.anim)
            {
                PlayerController.MovementControl.anim.SetFloat("CurrentPlayerSpeed",PlayerCurrentSpeed);
            }
        }
    }


}
