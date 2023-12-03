namespace Blast
{
    public static class BoardElementHelper
    {
        public static bool IsBaseType(BoardElementType type, BaseElementType baseElementType) => ((int)type & (int)baseElementType) > 0;

        public static bool IsColorType(BoardElementType type, ColorType colorType) => ((int)colorType & (int)type) > 0;

        public static bool IsBlockerType(BoardElementType type, BlockerType blockerType)
        {
            int blocker = (int)BaseElementType.Blocker ^ (int)type;
            return ((BlockerType)(blocker & (~ (int)ColorType.ColorMask))) == blockerType;
        }

        public static bool IsBackgroundType(BoardElementType type, BackgroundType backgroundType)
        {
            int background = (int)BaseElementType.TileBackground ^ (int)type;
            return ((BackgroundType)(background & (~ (int)ColorType.ColorMask))) == backgroundType;
        }
    }
    
    
    public enum BoardElementType
    {
        //None and tile should be obsolete 
        None = 0,
        Tile = 1,
        
        //Stones
        RandomStone =                BaseElementType.ColorStone | ColorType.Random,
        BlueStone =                  BaseElementType.ColorStone | ColorType.Blue,
        YellowStone =                BaseElementType.ColorStone | ColorType.Yellow,
        RedStone =                   BaseElementType.ColorStone | ColorType.Red,
        PurpleStone =                BaseElementType.ColorStone | ColorType.Purple,
        GreenStone =                 BaseElementType.ColorStone | ColorType.Green,
                 
        //Boosters         
        HorizontalBooster =          BaseElementType.Booster | BoosterType.Horizontal,
        VerticalBooster =            BaseElementType.Booster | BoosterType.Vertical,
        PlusBooster =                BaseElementType.Booster | BoosterType.Plus,
        SquareBooster =              BaseElementType.Booster | BoosterType.Square,
        VortexBooster =              BaseElementType.Booster | BoosterType.Vortex,
        
        //Spawners         
        StoneSpawner =               BaseElementType.Spawner | SpawnerType.Stone,
        
        //Blockers
        BreakableBlocker =           BaseElementType.Blocker | BlockerType.Breakable,
        CoverBlocker =               BaseElementType.Blocker | BlockerType.Cover,
        ColoredBlocker =             BaseElementType.Blocker | BlockerType.Colored,
        BlueColoredBlocker =         BaseElementType.Blocker | BlockerType.Colored | ColorType.Blue,
        YellowColoredBlocker =       BaseElementType.Blocker | BlockerType.Colored | ColorType.Yellow,
        RedColoredBlocker =          BaseElementType.Blocker | BlockerType.Colored | ColorType.Red,
        PurpleColoredBlocker =       BaseElementType.Blocker | BlockerType.Colored | ColorType.Purple,
        GreenColoredBlocker =        BaseElementType.Blocker | BlockerType.Colored | ColorType.Green,
        
        //Backgrounds
        GrassBackground =            BaseElementType.TileBackground | BackgroundType.Grass,
        ColoredBackground =          BaseElementType.TileBackground | BackgroundType.Colored,
        BlueColoredBackground =      BaseElementType.TileBackground | BackgroundType.Colored | ColorType.Blue,
        YellowColoredBackground =    BaseElementType.TileBackground | BackgroundType.Colored | ColorType.Yellow,
        RedColoredBackground =       BaseElementType.TileBackground | BackgroundType.Colored | ColorType.Red,
        PurpleColoredBackground =    BaseElementType.TileBackground | BackgroundType.Colored | ColorType.Purple,
        GreenColoredBackground =     BaseElementType.TileBackground | BackgroundType.Colored | ColorType.Green,

    }
    
    public enum ColorType
    {
        //Increment one byte to left for each added color
        ColorMask = 0b_0000_0000_0000_0000_0000_1111_1100_0000,
        
        Random = 1<<6,
        Blue = 1<<7,
        Yellow = 1<<8,
        Red = 1<<9,
        Purple = 1<<10,
        Green = 1<<11
    }

    public enum BaseElementType
    {
        //Increment one byte to left for each added base type
        BaseElementMask = 0b_0000_0000_0000_0011_1111_0000_0000_0000,
        
        ColorStone =      1<<12,
        Booster =         1<<13,
        Collectible =     1<<14,
        Spawner =         1<<15,
        Blocker =         1<<16,
        TileBackground =  1<<17,
    }
    
    public enum TileLayerType
    {
        None = 0,
        BlockerLayer,       //Blockers
        ItemLayer,          //Color stones, boosters and other playables
        TileAbilityLayer,   //Spawners, teleports and other abilities
        TileBackgroundLayer,//Elements under all tile elements
    }

    public enum BoosterType 
    {
        None,
        Horizontal = 2,
        Vertical = 4,
        Plus = 6,
        Square = 8,
        Vortex = 10,
    }

    public enum SpawnerType
    {
        None,
        Stone,
    }

    public enum BlockerType
    {
        None,
        Breakable,
        Cover,
        Colored,
    }

    public enum BackgroundType
    {
        None,
        Grass,
        Colored,
        Galaxy
    }
}