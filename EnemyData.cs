using Godot;

[GlobalClass]
public partial class EnemyData : Resource {
    [Export] public PackedScene Scene;
    [Export] public StatsData Stats;
}
