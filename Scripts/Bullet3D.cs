using Godot;

public class Bullet3D : Spatial
{
    public float Range = 20; // Distance until bullet is removed
    private float distanceTravelled;

    // Equivalent of Unity's void Start() {}
    public override void _Ready()
    {
        var area = GetNode<Area>("Area");
        area.Connect("area_entered", this, "OnCollision");
        area.Connect("body_entered", this, "OnCollision");
    }

    public override void _PhysicsProcess(float delta)
    {
        float speed = 5;
        float moveAmount = speed * delta;
        Translation += GlobalTransform.basis.x * moveAmount;
        distanceTravelled += moveAmount;
        if (distanceTravelled > Range) QueueFree();
    }

    private void OnCollision(Node with)
    {
        if (with.Name == "EnemyArea")
        {
            with.GetParent<Enemy3D>().Damage(2);
            QueueFree();
        }
    }
}
