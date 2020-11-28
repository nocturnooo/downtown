using Godot;

public class WorldGenerator : Spatial
{
    PackedScene tree;
    PackedScene key;
    PackedScene door;
    PackedScene enemy;

    [Export]
    private int enemyAmount = 10;

    [Export]
    private int treeAmount = 200;

    public override void _Ready()
    {
        // Seed randomizer
        GD.Randomize();

        tree = GD.Load<PackedScene>("res://Scenes3D/Tree3D.tscn");
        key = GD.Load<PackedScene>("res://Scenes3D/Key3D.tscn");
        door = GD.Load<PackedScene>("res://Scenes3D/Door3D.tscn");
        enemy = GD.Load<PackedScene>("res://Scenes3D/Enemy3D.tscn");

        // Instance trees
        for (int i = 0; i < treeAmount; i++)
        {
            StaticBody instanceTree = (StaticBody)tree.Instance();
            instanceTree.Translation = new Vector3((float)GD.RandRange(-100, 100), 0, (float)GD.RandRange(100, -100));
            CallDeferred("add_child", instanceTree);
        }

        // Instance one time scenes
        StaticBody instanceKey = (StaticBody)key.Instance();
        instanceKey.Translation = new Vector3((float)GD.RandRange(-100, 100), 0, (float)GD.RandRange(100, -100));
        CallDeferred("add_child", instanceKey);

        StaticBody instanceDoor = (StaticBody)door.Instance();
        instanceDoor.Translation = new Vector3((float)GD.RandRange(-100, 100), 0, (float)GD.RandRange(100, -100));
        CallDeferred("add_child", instanceDoor);
    }
}
