using Godot;

public class GoldWorldGenerator : Spatial
{
    PackedScene tree;

    [Export]
    private int treeAmount = 200;

    public override void _Ready()
    {
        // Seed randomizer
        GD.Randomize();

        tree = GD.Load<PackedScene>("res://Scenes3D/GoldTree3D.tscn");

        // Instance trees
        for (int i = 0; i < treeAmount; i++)
        {
            StaticBody instanceTree = (StaticBody)tree.Instance();
            instanceTree.Translation = new Vector3((float)GD.RandRange(-100, 100), 0, (float)GD.RandRange(100, -100));
            CallDeferred("add_child", instanceTree);
        }
    }
}
