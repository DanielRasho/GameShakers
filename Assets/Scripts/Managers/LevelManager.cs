using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int coinsRequired;

    [SerializeField] private GameObject victoryUI;
    private int coins = 0;

    [SerializeField] private int informesRequired;
    private int informes = 0;
    public int Informes => informes;

    [SerializeField] private Toggle informeToggle;


    // ---- COINS -----
    public int Coins
    {
        get { return coins; }
    }

    public void AddCoin()
    {
        if (coins < coinsRequired)
            coins++;
        Debug.Log("Coin Added: " + coins);
    }

    private void FixedUpdate()
    {
        if (coins == coinsRequired)
        {
            victoryUI.SetActive(true);
        }
    }

    // --- Informe ---
    public void AddInforme()
    {
        if (informes < informesRequired)
            informes++;

        Debug.Log("Informe recogido: " + informes);

        if (informes >= informesRequired)
            informeToggle.isOn = true;
    }

}
