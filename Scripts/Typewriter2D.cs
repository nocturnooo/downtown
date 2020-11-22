using Godot;

public class Typewriter2D : Node2D
{
    private float lapsed;
    private Label label;

    public override void _Ready()
    {
        label = GetNode<Label>("Text");
        lapsed = 0;
    }

    public override void _PhysicsProcess(float delta)
    {
        lapsed += delta;
        label.VisibleCharacters = (int)(lapsed / 0.1);
    }
}
