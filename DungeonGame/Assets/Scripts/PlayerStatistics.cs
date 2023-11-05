using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public static PlayerStatistics instance;

    private byte _countOfOpenedLevels;
    private byte _countOfOpenedCards;
    [SerializeField] private byte _totalCountOfLevels = 10;
    [SerializeField] private byte _totalCountOfCards = 9;

    public byte CountOfOpenedLevels => _countOfOpenedLevels;
    public byte CountOfOpenedCards => _countOfOpenedCards;
    public byte TotalCountOfLevels => _totalCountOfLevels;
    public byte TotalCountOfCards =>_totalCountOfCards;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
