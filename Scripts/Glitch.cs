using Godot;
using System;

public class Glitch : Node2D
{
    private Node2D player;
    private Label fakeTreeLabel;
    private Label fakeTreeLabel2;

    private Timer timer;

    [Export]
    private string scenePath;

    public override void _Ready()
    {
        player = GetNode<Player>("../Player");
        fakeTreeLabel = GetNode<Label>("FakeTreeLabel");
        fakeTreeLabel2 = GetNode<Label>("FakeTreeLabel2");

        var fakeTreeArea = GetNode<Area2D>("FakeTreeArea");
        fakeTreeArea.Connect("area_entered", this, nameof(OnFakeTreeCollision));
        fakeTreeArea.Connect("area_exited", this, nameof(OnExitFakeTreeCollision));
    }

    private void OnFakeTreeCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            fakeTreeLabel.Visible = true; fakeTreeLabel2.Visible = true;
        }
    }

    private async void OnExitFakeTreeCollision(Area2D with)
    {
        timer = new Timer();
        timer.WaitTime = 5;
        timer.Autostart = true;
        this.AddChild(timer);

        await ToSignal(timer, "timeout");
        GetTree().ChangeScene(scenePath);
    }
}
