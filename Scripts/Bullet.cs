using Godot;

public class Bullet : Node2D
{
    public float Range = 800; // Distance until bullet is removed
    private float distanceTravelled;

    // Equivalent of Unity's void Start() {}
    public override void _Ready()
    {
        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, "OnCollision");
        area.Connect("body_entered", this, "OnCollision");
    }

    public override void _Process(float delta)
    {
        float speed = 1500;
        float moveAmount = speed * delta;
        Position += Transform.x.Normalized() * moveAmount;
        distanceTravelled += moveAmount;
        if (distanceTravelled > Range) QueueFree();
    }

    private void OnCollision(Node2D with)
    {
        if (with.Name == "EnemyArea")
        {
            with.GetParent<Enemy>().Damage(2);
            QueueFree();
        }
    }
}
