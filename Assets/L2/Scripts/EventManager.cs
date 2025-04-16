using UnityEngine;
using UnityEngine.Events;
public class EventManager : MonoBehaviour
{

    public static EventManager Instance;

    [SerializeField]
    public UnityEvent FoodEaten;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
