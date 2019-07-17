using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;

namespace CommandButtonForTitleAndCaption
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            dashboardViewer1.ConfigureDataConnection += DashboardViewer1_ConfigureDataConnection;
            dashboardViewer1.CustomizeDashboardTitle += DashboardViewer1_CustomizeDashboardTitle;
            dashboardViewer1.CustomizeDashboardItemCaption += DashboardViewer1_CustomizeDashboardItemCaption;
            dashboardViewer1.LoadDashboard("DashboardTest.xml");
        }

        private void DashboardViewer1_CustomizeDashboardTitle(object sender, CustomizeDashboardTitleEventArgs e)
        {
            DashboardToolbarItem item = new DashboardToolbarItem(
                (args) =>
                {
                    System.Diagnostics.Process.Start("https://docs.devexpress.com/Dashboard/");
                })
            {
                SvgImage = svgImageCollection1["needassistance"],
                Caption = "Online Help",
                Tooltip = "Navigate to the online Dashboard help"
            };
            e.Items.Add(item);
        }
        private void DashboardViewer1_CustomizeDashboardItemCaption(object sender, CustomizeDashboardItemCaptionEventArgs e)
        {
            DashboardViewer viewer = sender as DashboardViewer;
            GridDashboardItem gridItem = viewer.Dashboard.Items[e.DashboardItemName] as GridDashboardItem;
            if (gridItem != null)
            {
                DashboardToolbarItem cmdButtonItem = new DashboardToolbarItem
                {
                    ClickAction = args => SwitchGridMeasureColumnDisplayMode(gridItem),
                    SvgImage = svgImageCollection1["bluedatabarsolid"],
                    Tooltip = "Switch between bars and numbers"
                };
                e.Items.Insert(0, cmdButtonItem);
            };

        }

        private void SwitchGridMeasureColumnDisplayMode(GridDashboardItem gridItem)
        {
            foreach (var column in gridItem.Columns)
            {
                GridMeasureColumn measureColumn = column as GridMeasureColumn;
                if (measureColumn != null)
                {
                    switch (measureColumn.DisplayMode)
                    {
                        case GridMeasureColumnDisplayMode.Bar:
                            measureColumn.DisplayMode = GridMeasureColumnDisplayMode.Value;
                            break;
                        case GridMeasureColumnDisplayMode.Value:
                            measureColumn.DisplayMode = GridMeasureColumnDisplayMode.Bar;
                            break;
                    }
                }
            }
        }
        private void DashboardViewer1_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e)
        {
            if (e.ConnectionParameters is ExcelDataSourceConnectionParameters connParams)
                connParams.FileName = "SalesPerson.xlsx";
        }
    }
}
