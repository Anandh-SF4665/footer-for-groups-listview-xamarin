using Syncfusion.DataSource;
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
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;

        #endregion

        #region Overrides

        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ListView.ItemDragging += ListView_ItemDragging;
            base.OnAttachedTo(bindable);
        }

        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if(e.Action == DragAction.Drop)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ListView.RefreshListViewItem(-1, -1, true);
                });
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView = null;
            ListView.ItemDragging -= ListView_ItemDragging;
            base.OnDetachingFrom(bindable);
        }
        #endregion
    }
}