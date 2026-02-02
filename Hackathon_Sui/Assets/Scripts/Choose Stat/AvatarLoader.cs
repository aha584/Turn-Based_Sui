using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class AvatarLoader : MonoBehaviour
{
    public static AvatarLoader Instance;

    public SpriteRenderer mySpriteRenderer;
    private Texture2D oldTexture;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void LoadAvatar(string avatarPath)
    {
        if (File.Exists(avatarPath))
        {
            byte[] fileData = File.ReadAllBytes(avatarPath);
            Texture2D tex = new Texture2D(1, 1);
            if(oldTexture != null)
            {
                Destroy(oldTexture);
            }
            if (tex.LoadImage(fileData) && mySpriteRenderer != null)
            {
                ApplyTexture(mySpriteRenderer, tex);
                ChooseStatManager.Instance.playerPet.avatar = mySpriteRenderer.sprite;
                oldTexture = tex;
            }
            else
            {
                Debug.LogError("Không thấy file bg.png!!!!");
            }
        }
    }
    public void ApplyTexture(SpriteRenderer renderer, Texture2D texture)
    {
        // 1. Xác định vùng cắt (Rect): Lấy toàn bộ kích thước của Texture
        Rect rect = new Rect(0, 0, texture.width, texture.height);

        // 2. Xác định tâm (Pivot): (0.5, 0.5) là chính giữa
        Vector2 pivot = new Vector2(0.5f, 0.5f);

        // 3. (Tùy chọn) Pixels Per Unit: Mặc định là 100
        float pixelsPerUnit = 100f;

        // 4. TẠO SPRITE MỚI
        Sprite newSprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit);

        // 5. Gán vào Renderer
        renderer.sprite = newSprite;
    }
}