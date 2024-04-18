namespace MLDetection;

public static class Tags
{
    public static Tag GetTag(string tagName)
    {
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
}