using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is null!");
            }
            return _instance;
        }
    }
    public bool HasKeyToCastle{get; set;}
    public bool hasFlameSword{get; set;}
    public Player Player {get; private set;}
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
         Player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
