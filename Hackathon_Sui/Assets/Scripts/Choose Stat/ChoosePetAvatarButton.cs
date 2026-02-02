using UnityEngine;
using SFB;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class AddPetAvatarButton : MonoBehaviour
{
    public Button chooseAvatarButton;

    public string selectedAvatarPath;

    public void OnClick()
    {
        if (chooseAvatarButton != null)
        {
            chooseAvatarButton.interactable = false;
        }
        var imageFilter = new[] { new ExtensionFilter("Image Files", "png") };
        string[] imagePath = StandaloneFileBrowser.OpenFilePanel("Chọn Avatar: ", string.Empty, imageFilter, false);
        if (chooseAvatarButton != null)
        {
            chooseAvatarButton.interactable = true;
        }
        if (imagePath.Length > 0 && !(imagePath.Contains(string.Empty) || imagePath.Contains(null)))
        {
            selectedAvatarPath = imagePath[0];
            AvatarLoader.Instance.LoadAvatar(selectedAvatarPath);
        }
    }
}
