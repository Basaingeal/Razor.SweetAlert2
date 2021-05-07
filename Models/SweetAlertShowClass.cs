namespace CurrieTechnologies.Razor.SweetAlert2 {
  using System.Text.Json.Serialization;
  public class SweetAlertShowClass {

    [JsonPropertyName("popup")]
    public string Popup { get; set; }

    [JsonPropertyName("backdrop")]
    public string Backdrop { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }
  }
}