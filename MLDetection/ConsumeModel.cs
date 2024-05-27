using Microsoft.ML.Data;
using MLDetection.Models;
using System.Drawing;

namespace MLDetection;

public class ConsumeModel : IConsumeModel
{
    private readonly UserConfiguration _configuration;
    public ConsumeModel(UserConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<ObjectPoint> GetObjects(string imagePath, float threshold, List<Tag>? tags = null)
    {
        var image = MLImage.CreateFromFile(imagePath);

        string[]? tagsForSearch = null;

        if (tags is not null)
        {
            tagsForSearch = Tags.GetTagNames(tags).ToArray();
        }


        ModelInput input = new ModelInput()
        {
            Image = image,
            Labels = tagsForSearch,
        };

        return GetObjects(input, threshold, tags ?? []);
    }

    public List<ObjectPoint> GetObjects(string imagePath, Tag tag, float threshold)
    {
        var image = MLImage.CreateFromFile(imagePath);


        ModelInput input = new ModelInput()
        {
            Image = image,
            Labels = [tag.ToString()],
        };

        return GetObjects(input, threshold, [tag]);
    }

    private List<ObjectPoint> GetObjects(ModelInput input, float threshold, List<Tag> tags)
    {
        ModelOutput predictionResult;

        predictionResult = MLModelConsumption.Predict(input, _configuration);

        if (predictionResult is null
            || predictionResult.PredictedBoundingBoxes is null
            || predictionResult.Score is null
            || predictionResult.PredictedLabel is null)
        {
            return new List<ObjectPoint>();
        }

        List<ObjectPoint> middlePoints = new();

        // Iterate through predicted labels and bounding boxes simultaneously
        for (int i = 0; i < predictionResult.PredictedBoundingBoxes.Length; i += 4)
        {
            float xTop = predictionResult.PredictedBoundingBoxes[i];
            float yTop = predictionResult.PredictedBoundingBoxes[i + 1];
            float xBottom = predictionResult.PredictedBoundingBoxes[i + 2];
            float yBottom = predictionResult.PredictedBoundingBoxes[i + 3];

            // Get the corresponding label
            var label = predictionResult.PredictedLabel[i / 4];

            var score = predictionResult.Score[i / 4];

            int middleX = (int)((xTop + xBottom) / 2);
            int middleY = (int)((yTop + yBottom) / 2);

            middlePoints.Add(new ObjectPoint
            {
                Point = new Point(middleX, middleY),
                Tag = Tags.GetTag(label),
                Score = score
            });
        }

        return middlePoints
            .Where(i => tags.Contains(i.Tag))
            .Where(x => x.Score > threshold)
            .OrderByDescending(x => x.Score)
            .ToList();
    }
}
