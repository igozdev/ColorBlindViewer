using UnityEngine;
using ColorTup = System.Tuple<UnityEngine.Color, UnityEngine.Color, UnityEngine.Color>;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorBlindViewer : MonoBehaviour
{
    public enum ColorBlindness
    {
        None,
        Protanopia,
        Deuteranopia,
        Protanomaly,
        Deuteranomaly,
        Tritanopia,
        Tritanomaly,
        Achromatopsia,
        Achromatomaly
    }

    // Values obtained from: https://web.archive.org/web/20081014161121/http://www.colorjack.com/labs/colormatrix/
    public static readonly ColorTup ChannelsNone            = new ColorTup(new Color(1.00000f, 0.00000f, 0.00000f), new Color(0.00000f, 1.00000f, 0.00000f), new Color(0.00000f, 0.00000f, 1.00000f));
    public static readonly ColorTup ChannelsProtanopia      = new ColorTup(new Color(0.56667f, 0.43333f, 0.00000f), new Color(0.55833f, 0.44167f, 0.00000f), new Color(0.00000f, 0.24167f, 0.75833f));
    public static readonly ColorTup ChannelsDeuteranopia    = new ColorTup(new Color(0.62500f, 0.37500f, 0.00000f), new Color(0.70000f, 0.30000f, 0.00000f), new Color(0.00000f, 0.30000f, 0.70000f));
    public static readonly ColorTup ChannelsProtanomaly     = new ColorTup(new Color(0.81667f, 0.18333f, 0.00000f), new Color(0.33333f, 0.66667f, 0.00000f), new Color(0.00000f, 0.12500f, 0.87500f));
    public static readonly ColorTup ChannelsDeuteranomaly   = new ColorTup(new Color(0.80000f, 0.20000f, 0.00000f), new Color(0.25833f, 0.74167f, 0.00000f), new Color(0.00000f, 0.14167f, 0.85833f));
    public static readonly ColorTup ChannelsTritanopia      = new ColorTup(new Color(0.95000f, 0.05000f, 0.00000f), new Color(0.00000f, 0.43333f, 0.56667f), new Color(0.00000f, 0.47500f, 0.52500f));
    public static readonly ColorTup ChannelsTritanomaly     = new ColorTup(new Color(0.96667f, 0.03333f, 0.00000f), new Color(0.00000f, 0.73333f, 0.26667f), new Color(0.00000f, 0.18333f, 0.81667f));
    public static readonly ColorTup ChannelsAchromatopsia   = new ColorTup(new Color(0.29900f, 0.58700f, 0.11400f), new Color(0.29900f, 0.58700f, 0.11400f), new Color(0.29900f, 0.58700f, 0.11400f));
    public static readonly ColorTup ChannelsAchromatomaly   = new ColorTup(new Color(0.61800f, 0.32000f, 0.06200f), new Color(0.16300f, 0.77500f, 0.06200f), new Color(0.16300f, 0.32000f, 0.51600f));

    private Material _renderMaterial;
    public ColorBlindness ColorBlindnessMode = ColorBlindness.None;
    private ColorBlindness _lastColorBlindnessMode = ColorBlindness.None;

    private void UpdateMaterial()
    {
        switch (ColorBlindnessMode)
        {
            case ColorBlindness.None:
                _renderMaterial.SetColor("_Red", ChannelsNone.Item1);
                _renderMaterial.SetColor("_Green", ChannelsNone.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsNone.Item3);
                return;
            case ColorBlindness.Protanopia:
                _renderMaterial.SetColor("_Red", ChannelsProtanopia.Item1);
                _renderMaterial.SetColor("_Green", ChannelsProtanopia.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsProtanopia.Item3);
                break;
            case ColorBlindness.Deuteranopia:
                _renderMaterial.SetColor("_Red", ChannelsDeuteranopia.Item1);
                _renderMaterial.SetColor("_Green", ChannelsDeuteranopia.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsDeuteranopia.Item3);
                break;
            case ColorBlindness.Protanomaly:
                _renderMaterial.SetColor("_Red", ChannelsProtanomaly.Item1);
                _renderMaterial.SetColor("_Green", ChannelsProtanomaly.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsProtanomaly.Item3);
                break;
            case ColorBlindness.Deuteranomaly:
                _renderMaterial.SetColor("_Red", ChannelsDeuteranomaly.Item1);
                _renderMaterial.SetColor("_Green", ChannelsDeuteranomaly.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsDeuteranomaly.Item3);
                break;
            case ColorBlindness.Tritanopia:
                _renderMaterial.SetColor("_Red", ChannelsTritanopia.Item1);
                _renderMaterial.SetColor("_Green", ChannelsTritanopia.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsTritanopia.Item3);
                break;
            case ColorBlindness.Tritanomaly:
                _renderMaterial.SetColor("_Red", ChannelsTritanomaly.Item1);
                _renderMaterial.SetColor("_Green", ChannelsTritanomaly.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsTritanomaly.Item3);
                break;
            case ColorBlindness.Achromatopsia:
                _renderMaterial.SetColor("_Red", ChannelsAchromatopsia.Item1);
                _renderMaterial.SetColor("_Green", ChannelsAchromatopsia.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsAchromatopsia.Item3);
                break;
            case ColorBlindness.Achromatomaly:
                _renderMaterial.SetColor("_Red", ChannelsAchromatomaly.Item1);
                _renderMaterial.SetColor("_Green", ChannelsAchromatomaly.Item2);
                _renderMaterial.SetColor("_Blue", ChannelsAchromatomaly.Item3);
                break;
        }
    }

    private void Awake()
    {
        _renderMaterial = new Material(Shader.Find("Hidden/ColorBlindFiltering"));
        UpdateMaterial();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (ColorBlindnessMode == _lastColorBlindnessMode)
            Graphics.Blit(source, destination, _renderMaterial);
        else
        {
            UpdateMaterial();
            _lastColorBlindnessMode = ColorBlindnessMode;
            Graphics.Blit(source, destination, _renderMaterial);
        }
    }
}
