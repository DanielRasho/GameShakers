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
    [SerializeField] private Toggle entregarCafeToggle;


    public bool tieneCafe = false;
    public bool cafeEntregado = false;

    public bool TieneCafe => tieneCafe;
    public bool CafeEntregado => cafeEntregado;


    public void Start()
    {
        if (informeToggle != null) informeToggle.isOn = false;
        if (entregarCafeToggle != null) entregarCafeToggle.isOn = false;
    }


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

    //--- cafe ---
    public void RecogerCafe()
    {
        if (tieneCafe || cafeEntregado)
            return;

        tieneCafe = true;
        Debug.Log("Cafe recogido");

    }

    public void EntregarCafe()
    {
        if (!tieneCafe || cafeEntregado)
            return;

        tieneCafe = false;
        cafeEntregado = true;

        Debug.Log("Cafe entregado a la jefa");

        if (entregarCafeToggle != null)
            entregarCafeToggle.isOn = true;
    }
}
