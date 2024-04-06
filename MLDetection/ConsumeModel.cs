using Microsoft.ML.Data;
using MLDetection.Models;
using System.Drawing;

namespace MLDetection;

public class ConsumeModel : IConsumeModel
{
    public List<ObjectPoint> GetObjects(string imagePath)
    {
        var image = MLImage.CreateFromFile(imagePath);
        ModelInput sampleData = new ModelInput()
        {
            Image = image,
        };

        ModelOutput predictionResult = MLModelConsumption.Predict(sampleData);

        if (predictionResult is null
            || predictionResult.PredictedBoundingBoxes is null
            || predictionResult.Score is null
            || predictionResult.PredictedLabel is null)
        {
            return new List<ObjectPoint>();
        }

        List<ObjectPoint> middlePoints = new ();

        // Iterate through predicted labels and bounding boxes simultaneously
        for (int i = 0; i < predictionResult.PredictedBoundingBoxes.Length; i += 4)
        {
            float xTop = predictionResult.PredictedBoundingBoxes[i];
            float yTop = predictionResult.PredictedBoundingBoxes[i + 1];
            float xBottom = predictionResult.PredictedBoundingBoxes[i + 2];
            float yBottom = predictionResult.PredictedBoundingBoxes[i + 3];

            // Get the corresponding label
            var label = predictionResult.PredictedLabel[i / 4];

            int middleX = (int)((xTop + xBottom) / 2);
            int middleY = (int)((yTop + yBottom) / 2);

            middlePoints.Add(new ObjectPoint 
            { 
                Point = new Point(middleX, middleY),
                Tag = label 
            });
        }

        return middlePoints;
    }
}
