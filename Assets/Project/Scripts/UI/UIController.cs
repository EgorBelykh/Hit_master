using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Screen loadingScreen;
    [SerializeField] private Screen startScreen;
    [SerializeField] private Screen batleScreen;

    private Screen currentScreen;

    public void Initialization()
    {
        AllScreenHide();
        ChangeScreen(loadingScreen);
    }

    public void LoadingScreenShow()
    {
        ChangeScreen(loadingScreen);
    }

    public void StartScreenShow()
    {
        ChangeScreen(startScreen);
    }

    public void BatleScreenShow()
    {
        ChangeScreen(batleScreen);
    }

    public void CurrentScreenHide()
    {
        if (currentScreen) currentScreen.Hide();
    }

    private void ChangeScreen(Screen screen)
    {
        CurrentScreenHide();
        currentScreen = screen;
        currentScreen.Show();
       
    }

    private void AllScreenHide()
    {
        loadingScreen.Hide();
        startScreen.Hide();
        batleScreen.Hide();
    }
}
