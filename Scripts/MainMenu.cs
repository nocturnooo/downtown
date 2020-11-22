using Godot;

public class MainMenu : Node2D
{
    private Button playButton;
    private Button quitButton;

    public override void _Ready()
    {
        playButton = GetNode<Button>("PlayButton");
        quitButton = GetNode<Button>("QuitButton");
    }

    public override void _Process(float delta)
    {
        if (playButton.Pressed) GetTree().ChangeScene("Scenes/Levels/Tutorial.tscn");
        if (quitButton.Pressed) GetTree().Quit();
    }
}
