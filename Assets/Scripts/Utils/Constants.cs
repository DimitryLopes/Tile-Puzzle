public static class Constants
{
    public class Assets
    {
        public const string PUZZLE_SPRITE_PATH = "Sprites/Puzzles/{0}";
        public const string PUZZLE_ICON_SPRITE_PATH = "Sprites/Puzzles/{0}_Icon";
    }

    public class Puzzle
    {
        public const int GRID_SIZE = 3; // 3x3
        public const int PUZZLE_PIECE_POSITION_OFFSET = 195;
        public const int PUZLLE_PIECE_STARTING_POSITION_X = 105;
        public const int PUZLLE_PIECE_STARTING_POSITION_Y = -105;
        public const int FLOATING_PIECE_OFFSET = 300;
    }

    public class Events
    {
        public const float DEFAULT_OBJECT_ROTATION = 1200;
    }

    public class Screens
    {
        public const string GAME_OVER_SCREEN_VICTORY_TEXT = "Victory!";
        public const string GAME_OVER_SCREEN_DEFEAT_TEXT = "Game over";
    }
}
