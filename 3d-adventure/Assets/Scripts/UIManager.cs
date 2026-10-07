using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private UICanvas[] panels;
    [SerializeField] private GameObject panelPause;
    public State currentState;
    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetState(State.Focus);
    }

    private void Update()
    {
        if (InputSystem.actions["Escape"].WasPressedThisFrame() || !Application.isFocused)
        {
            isPaused = true;
        }

        if (!isPaused)
        {
            if (InputSystem.actions["Inventory"].WasPressedThisFrame())
            {
                if (currentState == State.Inventory)
                {
                    SetState(State.Focus);
                }
                else
                {
                    SetState(State.Inventory);
                }
            }

            switch (currentState)
            {
                case State.Focus:
                    Cursor.lockState = CursorLockMode.Locked;
                break;

                case State.Inventory:
                    Cursor.lockState = CursorLockMode.None;
                break;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            if (InputSystem.actions["Attack"].WasPressedThisFrame())
            {
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        panelPause.SetActive(isPaused);
    }

    public void SetState(State state)
    {
        foreach (var p in panels) p.canvas.SetActive(p.state == state);
        currentState = state;
    }
}

public enum State
{
    Focus,
    Inventory
}

[System.Serializable]
public struct UICanvas
{
    public State state;
    public GameObject canvas;
}
