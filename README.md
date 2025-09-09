**[View document in Syncfusion Xamarin Knowledge base](https://www.syncfusion.com/kb/12286/how-to-add-footer-for-groups-in-xamarin-forms-listview-sflistview)**

## Sample

```xaml
<syncfusion:SfListView x:Name="listView" AutoFitMode="DynamicHeight" ItemsSource="{Binding ContactsInfo}" DragStartMode="OnHold">
    <syncfusion:SfListView.DataSource>
        <data:DataSource>
            <data:DataSource.GroupDescriptors>
                <data:GroupDescriptor PropertyName="ContactType"/>
            </data:DataSource.GroupDescriptors>
        </data:DataSource>
    </syncfusion:SfListView.DataSource>

    <syncfusion:SfListView.ItemTemplate >
        <DataTemplate>
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </syncfusion:SfListView.ItemTemplate>

    <syncfusion:SfListView.GroupHeaderTemplate>
        <DataTemplate>
            <StackLayout x:Name="groupHeader" BackgroundColor="#E4E4E4">
                <Label Text="{Binding Key}" FontSize="22" FontAttributes="Bold" VerticalOptions="CenterAndExpand" HorizontalOptions="Start" Margin="20,0,0,0" />
            </StackLayout>
        </DataTemplate>
    </syncfusion:SfListView.GroupHeaderTemplate>
</syncfusion:SfListView>

Converter:

public class FooterVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listView = parameter as SfListView;

        if (value == null || listView.DataSource.Groups.Count == 0)
            return false;

        var groupHelper = new GroupHelper(listView);
        var groupresult = groupHelper.GetGroup(value);
        var dropGroupList = groupresult.GetType().GetRuntimeProperties().FirstOrDefault(method => method.Name == "ItemList").GetValue(groupresult) as List<object>;
        var lastItem = dropGroupList[dropGroupList.Count - 1] as Contacts;
        return dropGroupList[dropGroupList.Count - 1] == value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class FooterTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listView = parameter as SfListView;

        if (value == null || listView.DataSource.Groups.Count == 0)
            return false;

        var groupHelper = new GroupHelper(listView);
        var groupresult = groupHelper.GetGroup(value);
        return "Items count: " + groupresult.Count.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public GroupResult GetGroup(object itemData)
{
    GroupResult itemGroup = null;

    foreach (var item in this.ListView.DataSource.DisplayItems)
    {
        if (item == itemData)
            break;

        if (item is GroupResult)
            itemGroup = item as GroupResult;
    }
    return itemGroup;
}
```
