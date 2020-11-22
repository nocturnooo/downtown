using Godot;

public class Enemy3D : KinematicBody
{
    public float speed = 2;
    private Timer timer;
    private int health = 20;

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0) QueueFree();
        if (health <= 8) speed = 6;
        if (health <= 4) speed = 10;
    }

    public override void _Ready()
    {
        var area = GetNode<Area>("EnemyArea");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(OnCollisionExit));

        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    public override void _Process(float delta)
    {
        var player = GetNode<Player3D>("../Player3D");
        float moveAmount = speed * delta;
        Vector3 moveDirection = (player.Translation - Translation).Normalized();
        MoveAndCollide(moveDirection * moveAmount);
    }

    private void OnCollision(Area with)
    {
        if (with.GetParent() is Player3D player)
        {
            timer.Start(1);
        }
    }

    private void OnCollisionExit(Area with)
    {
        if (with.GetParent() is Player3D player)
        {
            timer.Stop();
        }
    }

    private void OnTimerTimeout()
    {
        var player = GetNode<Player3D>("../Player3D");
        if (player != null)
        {
            player.Health -= 2;
        }
    }
}
