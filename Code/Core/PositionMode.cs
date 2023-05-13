namespace Celeste.Mod.TrollGrabBag.Core;

public enum PositionMode
{
    /// <summary>
    /// The effect spawns at the player's position.
    /// </summary>
    AtPlayer,

    /// <summary>
    /// The effect spawns at each node.
    /// </summary>
    AtNode,

    /// <summary>
    /// The effect spawns on the screen, regardless of camera position.
    /// </summary>
    OnScreen,
}
