local emote = {}

emote.name = "trolls_effect_grab_bag/emote"

emote.nodeLimits = {0, -1}
emote.nodeLineRenderType = "fan"

emote.fieldInformation = {
    mode = {
        editable = false,
        options = {
            ["At Player"] = "AtPlayer",
            ["At Node"] = "AtNode",
            ["On Screen"] = "OnScreen",
        }
    }
}

emote.placements = {
    {
        name = "default",
        data = {
            event = "event:/none",
            path = "",
            once = false,
            scale_x = 1.0,
            scale_y = 1.0,
            rotation = 0.0,
            mode = "AtPlayer",
            offset_x = 0.0,
            offset_y = 0.0,
        }
    },
    {
        name = "granny_laugh",
        data = {
            event = "event:/SFX/Trollpack/laugh",
            path = "troll_effects/granny_laugh",
            once = false,
            scale_x = 1.0,
            scale_y = 1.0,
            rotation = 0.0,
            mode = "AtPlayer",
            offset_x = 0.0,
            offset_y = 0.0,
        }
    },
    {
        name = "poop",
        data = {
            event = "event:/SFX/Trollpack/FartNoise",
            path = "troll_effects/poop",
            once = false,
            scale_x = 1.0,
            scale_y = 1.0,
            rotation = 0.0,
            mode = "AtPlayer",
            offset_x = 0.0,
            offset_y = 0.0,
        }
    },
    {
        name = "buzzer",
        data = {
            event = "event:/SFX/Trollpack/Loud_Buzzer",
            path = "troll_effects/x",
            once = false,
            scale_x = 1.0,
            scale_y = 1.0,
            rotation = 0.0,
            mode = "AtPlayer",
            offset_x = 0.0,
            offset_y = 0.0,
        }
    }
}


return emote