using Godot;

public class WorldGenerator : Spatial
{
    PackedScene tree;

    [Export]
    private int amount = 30;

    public override void _Ready()
    {
        GD.Randomize();

        tree = GD.Load<PackedScene>("res://Scenes3D/Tree3D.tscn");
        for (int i = 0; i < amount; i++)
        {
            StaticBody instanceTree = (StaticBody)tree.Instance();
            instanceTree.Translation = new Vector3((float)GD.RandRange(-100, 100), 0, (float)GD.RandRange(100, -100));
            CallDeferred("add_child", instanceTree);
        }
        
    }
}
