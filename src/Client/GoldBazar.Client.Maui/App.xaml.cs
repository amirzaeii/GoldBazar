﻿namespace GoldBazar.Client.Maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "GoldBazar.Client.Maui" };
	}
}
