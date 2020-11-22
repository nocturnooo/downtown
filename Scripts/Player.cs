using Godot;

public class Player : KinematicBody2D
{
    PackedScene bullet;
    private float health = 20;
    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private int speed = 2;

    private ProgressBar ammoBar;
    private int ammo = 10;
    private bool canShoot = true;

    private AnimationPlayer animationPlayer;

    public float Health
    {
        get { return health; }
        set
        {
            health = value; healthBar.Value = health;
            healthBar.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            if (health <= 0) QueueFree();
        }
    }

    // Equivalent of Unity's void Start() {}
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("PortalExit");

        healthBar = GetNode<ProgressBar>("../HUD/Overlay/HealthBar");
        staminaBar = GetNode<ProgressBar>("../HUD/Overlay/StaminaBar");
        ammoBar = GetNode<ProgressBar>("../HUD/Overlay/AmmoBar");
        bullet = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
    }

    // Equivalent of Unity's void Update() {}
    public override void _Process(float delta)
    {
        if (staminaBar.Value > 0)
        {
            if (Input.IsKeyPressed((int)KeyList.Shift))
            {
                speed = 4;
                staminaBar.Value--;
            }
            else speed = 2;
        } else speed = 2;

        ammoBar.Value = ammo;
        if (ammo == 0) canShoot = false;
        if (Input.IsKeyPressed((int)KeyList.R))
        {
            if (ammo < 10)
            {
                ammo = 10;
                canShoot = true;
            }
        }

        // Moving System
        Vector2 moveVector = new Vector2(0, 0);
        if (Input.IsKeyPressed((int)KeyList.W)) moveVector.y = -speed;
        if (Input.IsKeyPressed((int)KeyList.S)) moveVector.y = speed;
        if (Input.IsKeyPressed((int)KeyList.A)) moveVector.x = -speed;
        if (Input.IsKeyPressed((int)KeyList.D)) moveVector.x = speed;
        MoveAndCollide(moveVector);

        // Rotation System
        LookAt(GetGlobalMousePosition());
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == (int)ButtonList.Left && mouseEvent.Pressed)
            {
                if (canShoot)
                {
                Bullet instanceBullet = (Bullet)bullet.Instance();
                ammo--;
                animationPlayer.Play("Shoot");
                instanceBullet.Position = Position; instanceBullet.Rotation = Rotation;
                GetParent().AddChild(instanceBullet);
                GetTree().SetInputAsHandled();
                }
            }
        }
    }
}
