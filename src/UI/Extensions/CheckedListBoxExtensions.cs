namespace UI.Extensions;

internal static class CheckedListBoxExtensions
{
    internal static IEnumerable<TValue> GetValues<TKey, TValue>(this CheckedListBox.CheckedItemCollection checkedItems)
    {
        foreach (var item in checkedItems)
        {
            if (item is KeyValuePair<TKey, TValue> kvp)
            {
                yield return kvp.Value;
            }
        }
    }
}
