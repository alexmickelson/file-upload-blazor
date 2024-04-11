
using System.Diagnostics;
using System.Diagnostics.Metrics;

public static class DiagnosticsConfig
{
  public const string SourceName = "file-upload-blazor";
  public static ActivitySource Source = new ActivitySource(SourceName);
  public static Meter MyMeter = new Meter("FileUploadMeter");
  public static Counter<int> MyCounter = MyMeter.CreateCounter<int>("file_upload_counter`");
}
