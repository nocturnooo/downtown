using Godot;

public class Player3D : KinematicBody
{
    PackedScene bullet;

    private float health = 20;
    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private ProgressBar ammoBar;

    private int speed = 4;
    private float sens = 0.5f;

    private bool canShoot = true;
    private int ammo = 10;

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

    public override void _Ready()
    {
        healthBar = GetNode<ProgressBar>("../HUD/Overlay/HealthBar");
        staminaBar = GetNode<ProgressBar>("../HUD/Overlay/StaminaBar");
        ammoBar = GetNode<ProgressBar>("../HUD/Overlay/AmmoBar");

        bullet = GD.Load<PackedScene>("res://Scenes3D/Bullet3D.tscn");

        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion @eventMouseMotion)
        {
            var rotationDegrees = RotationDegrees;
            rotationDegrees.y -= sens * @eventMouseMotion.Relative.x;
            RotationDegrees = rotationDegrees;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        var moveVector = new Vector3();
        if (Input.IsKeyPressed((int)KeyList.W)) moveVector.z -= 1;
        if (Input.IsKeyPressed((int)KeyList.S)) moveVector.z += 1;
        if (Input.IsKeyPressed((int)KeyList.A)) moveVector.x -= 1;
        if (Input.IsKeyPressed((int)KeyList.D)) moveVector.x += 1;
        moveVector = moveVector.Normalized();
        moveVector = moveVector.Rotated(new Vector3(0, 1, 0), Rotation.y);
        MoveAndCollide(moveVector * speed * delta);

        if (staminaBar.Value > 0)
        {
            if (Input.IsKeyPressed((int)KeyList.Shift))
            {
                speed = 8;
                staminaBar.Value--;
            }
            else speed = 4;
        }
        else speed = 4;

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
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == (int)ButtonList.Left && mouseEvent.Pressed)
            {
                if (canShoot)
                {
                    Bullet3D instanceBullet = (Bullet3D)bullet.Instance();
                    ammo--;
                    animationPlayer.Play("Shoot3D");
                    instanceBullet.Translation = Translation; instanceBullet.Rotation = Transform.basis.y * 25;
                    GetParent().AddChild(instanceBullet);
                    GetTree().SetInputAsHandled();
                }
            }
        }
    }
}
