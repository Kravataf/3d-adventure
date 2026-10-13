using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrimary : MonoBehaviour
{
    private void Update()
    {
        if (InputSystem.actions["Primary"].WasPressedThisFrame())
        {
            // lowkey nemam item types takze ako mam toto dpc robit
        }
    }
}
