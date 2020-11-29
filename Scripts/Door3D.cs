using Godot;

public class Door3D : StaticBody
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
            if (player.hasKey)
            {
                GetTree().ChangeScene("Scenes3D/End.tscn");
            }
        }
    }
}
