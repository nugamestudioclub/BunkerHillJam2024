using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashAction : MonoBehaviour, IAction
{

    private bool canAirDash = true;
    private bool touchedGrass = true;

    [SerializeField]
    private float duration = 1f;

    public void Act()
    {
        if (canAirDash)
        {
            canAirDash = false;
            touchedGrass = false;
            PlayerController.Dash();
            StartCoroutine(IECooldown());
        }
    }

    public void EndAct()
    {
        // pass
    }

    public string GetActionName()
    {
        return "Air Dash";
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.IsGrounded() && !touchedGrass)
        {
            touchedGrass = true;
        }
    }

    private IEnumerator IECooldown()
    {
        yield return new WaitForSeconds(duration);
        yield return new WaitUntil(() => touchedGrass);

        canAirDash = true;
    }
}
