using Godot;

public class Enemy : KinematicBody2D
{
    public float speed = 120;
    private Timer timer;
    private int health = 20;
    private ProgressBar healthBar;

    public void Damage(int amount)
    {
        health -= amount;
        healthBar.Value = health;
        if (health <= 0) QueueFree();
        if (health <= 8) speed = 180;
        if (health <= 4) speed = 320;
    }

    public override void _Ready()
    {
        var area = GetNode<Area2D>("EnemyArea");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(OnCollisionExit));

        var mouseArea = GetNode<Area2D>("MouseArea");
        mouseArea.Connect("mouse_entered", this, nameof(OnMouse));
        mouseArea.Connect("mouse_exited", this, nameof(OnMouseExit));

        healthBar = GetNode<ProgressBar>("HealthBar");

        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    public override void _Process(float delta)
    {
        var player = GetNode<Player>("../Player");
        float moveAmount = speed * delta;
        Vector2 moveDirection = (player.Position - Position).Normalized();
        MoveAndCollide(moveDirection * moveAmount);
    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            timer.Start(1);
        }
    }

    private void OnCollisionExit(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            timer.Stop();
        }
    }

    private void OnTimerTimeout()
    {
        var player = GetNode<Player>("../Player");
        if (player != null)
        {
            player.Health -= 2;
        }
    }

    private void OnMouse()
    {
        healthBar.Visible = true;
    }

    private void OnMouseExit()
    {
        healthBar.Visible = false;
    }
}
