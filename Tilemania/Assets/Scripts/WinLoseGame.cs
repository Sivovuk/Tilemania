using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseGame : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelLose;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinGame();
    }

    private void WinGame()
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
        _panelWin.SetActive(true);
    }

    public void LoseGame() 
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
        _panelLose.SetActive(true);
    }
}
