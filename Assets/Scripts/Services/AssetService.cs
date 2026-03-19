using UnityEngine;

public class AssetService
{
    public static Sprite GetPuzzleSprite(string spriteKey, int spriteIndex)
    {
        string path = string.Format(Constants.Assets.PUZZLE_SPRITE_PATH, spriteKey);

        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        return sprites[spriteIndex];
    }

    public static Sprite GetBoardSprite(string spriteKey)
    {
        string path = string.Format(Constants.Assets.PUZZLE_ICON_SPRITE_PATH, spriteKey);

        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }
}
