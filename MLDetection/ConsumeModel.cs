using Microsoft.ML.Data;
using MLDetection.Models;
using System.Drawing;

namespace MLDetection;

public class ConsumeModel : IConsumeModel
{
    private const float THRESHOLD = 0.8f;

    public List<Point> GetObjects(string imagePath, string tagName)
    {
        var image = MLImage.CreateFromFile(imagePath);
        ModelInput sampleData = new ModelInput()
        {
            Image = image,
            Labels = [tagName],
        };

        ModelOutput predictionResult = MLModelConsumption.Predict(sampleData);

        if (predictionResult is null
            || predictionResult.PredictedBoundingBoxes is null
            || predictionResult.Score is null)
        {
            return new List<Point>();
        }

        List<Point> middlePoints = new ();

        var boxes = predictionResult.PredictedBoundingBoxes.Chunk(4)
                        .Select(x => new { XTop = x[0], YTop = x[1], XBottom = x[2], YBottom = x[3] })
                        .Zip(predictionResult.Score, (a, b) => new { Box = a, Score = b });

        foreach (var item in boxes.Where(x => x.Score > THRESHOLD))
        {
            int middleX = (int)((item.Box.XTop + item.Box.XBottom) / 2);
            int middleY = (int)((item.Box.YTop + item.Box.YBottom) / 2);
            middlePoints.Add(new Point(middleX, middleY));
        }

        return middlePoints;
    }
}
