using Godot;

public class Portal : Node2D
{
    [Export]
    private bool useVector = true;
    [Export]
    private Vector2 posVector = new Vector2(0, 0);
    [Export]
    private string scenePath = "null";

    private KinematicBody2D player;
    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        player = GetNode<KinematicBody2D>("../Player");
        animationPlayer = GetNode<AnimationPlayer>("../Player/AnimationPlayer");
        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, nameof(OnCollision));
    }

    private void OnCollision(KinematicBody2D with)
    {
        if (with.GetParent() is Player player)
        {
            if (useVector)
            {
                player.Position = posVector;
            }
            else
            {
                GetTree().ChangeScene(scenePath);
            }
        }
        animationPlayer.Play("PortalExit");
    }
}
