using Godot;

public class StaminaPickup : Node2D
{
    private ProgressBar staminaBar;

   public override void _Ready()
   {
       staminaBar = GetNode<ProgressBar>("../HUD/Overlay/StaminaBar");
        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, "OnCollision");
        area.Connect("body_entered", this, "OnCollision");
   }

    private void OnCollision(Node with)
    {
        if (with.Name == "PlayerArea")
        {
            staminaBar.Value += 50;
            QueueFree();
        }
    }
}
