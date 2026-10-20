using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private float duration;

    void Start() 
    {
        Destroy(gameObject, duration);
    }
}
