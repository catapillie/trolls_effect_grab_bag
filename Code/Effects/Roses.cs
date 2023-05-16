﻿using Celeste.Mod.Entities;

namespace Celeste.Mod.TrollGrabBag.Effects;

[CustomEntity("trolls_effect_grab_bag/roses")]
public sealed class Roses : Trigger
{
    public Roses(EntityData data, Vector2 offset)
        : base(data, offset)
    { }

    public override void OnEnter(Player player)
    {
        base.OnEnter(player);

        Scene.Add(new RosesHUD());
        Audio.Play("event:/SFX/Trollpack/Roses");

        RemoveSelf();
    }
}

file sealed class RosesHUD : Entity
{
    private readonly MTexture rose;
    private float opacity;

    public RosesHUD()
    {
        Tag = Tags.HUD;
        rose = GFX.Gui["troll_effects/rose"];
    }

    public override void Update()
    {
        opacity = Calc.Approach(opacity, 1f, Engine.DeltaTime / 0.5f);
    }

    public override void Render()
    {
        Level level = Scene as Level;
        if (level.Paused)
            return;

        Vector2 center = new Vector2(320, 180) / 2;

        float rotation = level.TimeActive * 0.1f;

        const int count = 12;
        const float rad = 124f;
        for (int i = 0; i < count; i++)
        {
            float angle = rotation + MathHelper.TwoPi * i / count;
            Vector2 pos = center + Calc.AngleToVector(angle, rad);

            float spin = level.TimeActive * 0.2f * (i % 2 == 0 ? 1 : -1) + i;
            float scale = i % 2 == 0
                ? 0.3f
                : -0.25f;

            Color color = i % 2 == 0
                ? Calc.HexToColor("cca1b5")
                : Calc.HexToColor("ad343c");

            rose.DrawCentered(pos * 6, color * opacity, scale, spin);
        }

        GFX.Gui["troll_effects/rose_vignette"].Draw(Vector2.Zero, Vector2.Zero, Color.White * 0.5f * opacity);
    }
}