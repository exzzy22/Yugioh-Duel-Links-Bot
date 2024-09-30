using Microsoft.ML;
using MLDetection.Models;

namespace MLDetection;

internal class MLModelConsumption
{
    public const int TrainingImageWidth = 2560;
    public const int TrainingImageHeight = 1440;

    private static string MLNetModelPath = Path.GetFullPath("PredictionModel.mlnet");

    private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine(UserConfiguration configuration)
    {
        var mlContext = new MLContext();
        if (configuration.UseGpu)
        {
            mlContext.GpuDeviceId = 0;
            mlContext.FallbackToCpu = false;
        }
        else 
        {
            mlContext.GpuDeviceId = null;
            mlContext.FallbackToCpu = true;
        }

        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
    }

    public static ModelOutput Predict(ModelInput input, UserConfiguration configuration)
    {
        var predEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(configuration), true).Value;
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
