using System;

public class OptionsScreenController : ScreenController
{
    public readonly Action OnBackButtonClicked;

    public OptionsScreenController(Action onBackButtonClicked)
    {
        OnBackButtonClicked = onBackButtonClicked;
    }
}
