using Godot;

public class Key3D : StaticBody
{
    public override void _Ready()
    {
        var area = GetNode<Area>("Area");
        area.Connect("area_entered", this, nameof(OnCollision));
    }

    private void OnCollision(Area with)
    {
        if (with.GetParent() is Player3D player)
        {
            player.hasKey = true;
            QueueFree();
        }
    }
}
