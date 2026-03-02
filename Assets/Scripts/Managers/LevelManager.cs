using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int coinsRequired;
    private int coins = 0;

    // ---- COINS -----
    public int Coins
    {
        get { return coins; }
    }

    public void AddCoin()
    {
        if (coins < coinsRequired)
            coins++;
    }
}
