using Celeste.Mod.Entities;
using Celeste.Mod.TrollGrabBag.Core;
using Celeste.Mod.TrollGrabBag.Utils;
using System.Collections;

namespace Celeste.Mod.TrollGrabBag.Effects;

[CustomEntity("trolls_effect_grab_bag/emote")]
public sealed class Emote : Trigger
{
    private readonly Vector2[] nodes;
    private readonly PositionMode mode;

    private readonly string sound;
    private readonly string path;

    private readonly bool once;
    private readonly Vector2 offset, scale;
    private readonly float rotation;

    public Emote(EntityData data, Vector2 offset)
        : base(data, offset)
    {
        nodes = data.NodesOffset(offset);
        mode = data.Enum("mode", PositionMode.AtPlayer);

        sound = data.Attr("event", SFX.NONE);
        path = data.Attr("path", string.Empty);

        once = data.Bool("once", false);
        this.offset = new(data.Float("offset_x", 0.0f), data.Float("offset_y", 0.0f));
        scale = new(data.Float("scale_x", 1.0f), data.Float("scale_y", 1.0f));
        rotation = data.Float("rotation", 0.0f).ToRad();
    }

    public override void OnEnter(Player player)
    {
        MTexture emote = GFX.Gui[path];
        switch (mode)
        {
            case PositionMode.AtPlayer:
            {
                DoAtPlayer(player, emote);
                break;
            }

            case PositionMode.AtNode:
            {
                if (nodes.Length == 0)
                {
                    DoAtPlayer(player, emote);
                    break;
                }

                foreach (var node in nodes)
                    Scene.Add(new EmoteHUD(emote, node + offset, scale, rotation));
                Audio.Play(sound, nodes.Length == 1 ? nodes[0] : player.Center);

                break;
            }

            case PositionMode.OnScreen:
            {
                Vector2 at = new Vector2(160, 90 + 16) + offset;
                Scene.Add(new EmoteHUD(emote, at, scale, rotation, absolute: true));
                Audio.Play(sound, player.Center);
                break;
            }
        }

        if (once)
            RemoveSelf();
    }

    private void DoAtPlayer(Player player, MTexture emote)
    {
        Vector2 at = player.Center - Vector2.UnitY * 16;
        Scene.Add(new EmoteHUD(emote, at + offset, scale, rotation));
        Audio.Play(sound, Center);
    } 
}

file sealed class EmoteHUD : Entity
{
    private readonly MTexture texture;
    private readonly Vector2 size;
    private readonly float rotation;
    private readonly bool absolute;

    private Vector2 scale = Vector2.One;

    public EmoteHUD(MTexture texture, Vector2 at, Vector2 size, float rotation, bool absolute = false)
    {
        Tag = Tags.HUD;

        this.texture = texture;
        this.size = size;
        this.rotation = rotation;
        this.absolute = absolute;

        Position = at;

        Add(new Coroutine(Animation(at)));
    }

    public override void Render()
    {
        Level level = Scene as Level;
        if (level.Paused)
            return;

        Vector2 position = Position;
        if (!absolute)
            position -= level.Camera.Position;
        position *= 6f;

        texture.DrawCentered(position, Color.White, scale * 1 / texture.Width * 80 * size, rotation);
    }

    private IEnumerator Animation(Vector2 at)
    {
        yield return Routines.Interpolate(0.35f, percent =>
        {
            float ease = Ease.BigBackOut(percent);
            Position = at - Vector2.UnitY * 8 * 2.5f * ease;

        });
        yield return 0.25f;
        yield return Routines.Interpolate(0.2f, percent =>
        {
            scale = Vector2.One * Ease.SineOut(1 - percent);
        });
    }
}

