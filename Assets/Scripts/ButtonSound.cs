using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        // Obtén la referencia al componente Button del GameObject
        button = GetComponent<Button>();

        // Agrega un nuevo oyente de evento a la función OnClick
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SoundEfects.Instance.TouchGUISound();
    }
}
