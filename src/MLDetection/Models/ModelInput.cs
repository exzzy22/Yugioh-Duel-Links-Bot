using Microsoft.ML.Data;

namespace MLDetection.Models;

internal class ModelInput
{
    [LoadColumn(0)]
    [ColumnName(@"Labels")]
    public string[]? Labels { get; set; }

    [LoadColumn(1)]
    [ColumnName(@"Image")]
    [Microsoft.ML.Transforms.Image.ImageType(1440, 2560)]
    public MLImage Image { get; set; } = null!;

    [LoadColumn(2)]
    [ColumnName(@"Box")]
    public float[]? Box { get; set; }
}
