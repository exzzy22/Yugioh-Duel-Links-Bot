using Microsoft.ML.Data;

namespace MLDetection.Models;

internal class ModelOutput
{
    [ColumnName(@"Labels")]
    public uint[]? Labels { get; set; }

    [ColumnName(@"Image")]
    [Microsoft.ML.Transforms.Image.ImageType(1328, 2306)]
    public MLImage? Image { get; set; }

    [ColumnName(@"Box")]
    public float[]? Box { get; set; }

    [ColumnName(@"PredictedLabel")]
    public string[]? PredictedLabel { get; set; }

    [ColumnName(@"score")]
    public float[]? Score { get; set; }

    [ColumnName(@"PredictedBoundingBoxes")]
    public float[]? PredictedBoundingBoxes { get; set; }
}