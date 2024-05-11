using System.Collections.Immutable;

namespace MLDetection;

public static class Tags
{
    public static Tag GetTag(string tagName)
    {
        tagName = tagName.Replace(":", "").Trim();

        if (Enum.TryParse(tagName, out Tag tag))
        {
            return tag;
        }
        else
        {
            throw new ArgumentException($"Invalid tag name: {tagName}");
        }
    }

    public static string GetTagName(Tag tag)
    {
        return tag.ToString();
    }

    public static IEnumerable<string> GetTagNames(List<Tag> tag)
    {
        foreach (var item in tag)
        {
            yield return item.ToString();
        }
    }

    public static List<Tag> MissClickButtons()
    {
        return
        [
            Tag.BackButton,
            Tag.CloseButton,
        ];
    }

    public static List<Tag> ClickableButtons()
    {
        return
        [
            Tag.CloseButton,
            Tag.CancelButton,
            Tag.OkButton,
            Tag.NextButton,
            Tag.NoButton,
            Tag.DuelistDialog,
            Tag.RetryButton,
        ];
    }

    public static List<Tag> Duelists()
    {
        return
        [
            Tag.WorldDuelist,
            Tag.LegendaryDuelist,
            Tag.VagabondDuelist,
        ];
    }

    public static ImmutableList<Tag> Worlds() 
    {
        return
        [
            Tag.WorldDM,
            Tag.WorldDSOD,
            Tag.WorldGX,
            Tag.Word5DS,
            Tag.WorldZX,
            Tag.WordlAV,
            Tag.WorldVR,
            Tag.World7,
        ];
    }
}