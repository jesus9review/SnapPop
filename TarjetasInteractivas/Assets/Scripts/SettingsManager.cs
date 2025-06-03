using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Sonido")]
    public Toggle sonidoToggle;
    public Slider volumenSlider;

    private const string SONIDO_KEY = "SonidoActivado";
    private const string VOLUMEN_KEY = "Volumen";

    void Start()
    {
        // Cargar configuraciones guardadas
        bool sonidoActivo = PlayerPrefs.GetInt(SONIDO_KEY, 1) == 1;
        float volumen = PlayerPrefs.GetFloat(VOLUMEN_KEY, 1f);

        sonidoToggle.isOn = sonidoActivo;
        volumenSlider.value = volumen;

        AudioListener.volume = sonidoActivo ? volumen : 0f;

        // Listeners
        sonidoToggle.onValueChanged.AddListener(ActivarSonido);
        volumenSlider.onValueChanged.AddListener(CambiarVolumen);
    }

    public void ActivarSonido(bool activo)
    {
        PlayerPrefs.SetInt(SONIDO_KEY, activo ? 1 : 0);
        AudioListener.volume = activo ? volumenSlider.value : 0f;
    }

    public void CambiarVolumen(float valor)
    {
        PlayerPrefs.SetFloat(VOLUMEN_KEY, valor);
        if (sonidoToggle.isOn)
            AudioListener.volume = valor;
    }
}
