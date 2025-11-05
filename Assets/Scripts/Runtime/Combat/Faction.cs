using System;

namespace NewKris.Runtime.Combat {
    [Flags]
    public enum Faction {
        NONE = 0,
        FRIENDLY = 1,
        ENEMY = 2,
        ENVIRONMENT = 4,
    }
}
