using Godot;

public class Player3D : KinematicBody
{
    public bool hasKey = false;

    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private ProgressBar ammoBar;

    private int speed = 4;
    private float sens = 0.5f;

    public override void _Ready()
    {
        healthBar = GetNode<ProgressBar>("../HUD/Overlay/HealthBar");
        staminaBar = GetNode<ProgressBar>("../HUD/Overlay/StaminaBar");
        ammoBar = GetNode<ProgressBar>("../HUD/Overlay/AmmoBar");
        ammoBar.Value = 0;

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
    }
}