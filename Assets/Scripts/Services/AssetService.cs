using UnityEngine;

public class AssetService
{
    public static Sprite GetPuzzleSprite(string spriteKey, int spriteIndex)
    {
        string path = string.Format(Constants.Assets.PUZZLE_SPRITE_PATH, spriteKey);

        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        return sprites[spriteIndex];
    }
}
