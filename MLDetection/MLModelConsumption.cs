using Microsoft.ML;
using MLDetection.Models;

namespace MLDetection;

internal class MLModelConsumption
{
    public const int TrainingImageWidth = 800;
    public const int TrainingImageHeight = 600;


    private static string MLNetModelPath = Path.GetFullPath("PredictionModel.mlnet");

    public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


    private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
    {
        var mlContext = new MLContext();
        mlContext.GpuDeviceId = 0;
        mlContext.FallbackToCpu = false;
        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
    }

    public static ModelOutput Predict(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        var output = predEngine.Predict(input);

        CalculateAspectAndOffset(input.Image.Width, input.Image.Height, TrainingImageWidth, TrainingImageHeight, out float xOffset, out float yOffset, out float aspect);

        if (output.PredictedBoundingBoxes != null && output.PredictedBoundingBoxes.Length > 0)
        {
            for (int x = 0; x < output.PredictedBoundingBoxes.Length; x += 2)
            {
                output.PredictedBoundingBoxes[x] = (output.PredictedBoundingBoxes[x] - xOffset) / aspect;
                output.PredictedBoundingBoxes[x + 1] = (output.PredictedBoundingBoxes[x + 1] - yOffset) / aspect;
            }
        }
        return output;
    }

    private static void CalculateAspectAndOffset(float sourceWidth, float sourceHeight, float destinationWidth, float destinationHeight, out float xOffset, out float yOffset, out float aspect)
    {
        float widthAspect = destinationWidth / sourceWidth;
        float heightAspect = destinationHeight / sourceHeight;
        xOffset = 0;
        yOffset = 0;
        if (heightAspect < widthAspect)
        {
            aspect = heightAspect;
            xOffset = (destinationWidth - (sourceWidth * aspect)) / 2;
        }
        else
        {
            aspect = widthAspect;
            yOffset = (destinationHeight - (sourceHeight * aspect)) / 2;
        }
    }
}
