using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region FooterVisibilityConverter

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
    #endregion

    #region FooterTextConverter
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
    #endregion

    #region GroupHelper
    internal class GroupHelper
    {
        #region Fields

        private SfListView ListView;
        #endregion

        #region Constructor
        
        public GroupHelper(SfListView sfListView)
        {
            ListView = sfListView;
        }

        #endregion

        #region Methods
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
        #endregion
    }
    #endregion
}