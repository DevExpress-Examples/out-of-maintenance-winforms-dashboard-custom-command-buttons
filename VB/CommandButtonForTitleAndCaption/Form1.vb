Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin

Namespace CommandButtonForTitleAndCaption
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			AddHandler dashboardViewer1.ConfigureDataConnection, AddressOf DashboardViewer1_ConfigureDataConnection
			AddHandler dashboardViewer1.CustomizeDashboardTitle, AddressOf DashboardViewer1_CustomizeDashboardTitle
			AddHandler dashboardViewer1.CustomizeDashboardItemCaption, AddressOf DashboardViewer1_CustomizeDashboardItemCaption
			dashboardViewer1.LoadDashboard("DashboardTest.xml")
		End Sub

		Private Sub DashboardViewer1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
			Dim item As DashboardToolbarItem = New DashboardToolbarItem(Sub(args)
				System.Diagnostics.Process.Start("https://docs.devexpress.com/Dashboard/")
			End Sub) With {.SvgImage = svgImageCollection1("needassistance"), .Caption = "Online Help", .Tooltip = "Navigate to the online Dashboard help"}
			e.Items.Add(item)
		End Sub
		Private Sub DashboardViewer1_CustomizeDashboardItemCaption(ByVal sender As Object, ByVal e As CustomizeDashboardItemCaptionEventArgs)
			Dim viewer As DashboardViewer = TryCast(sender, DashboardViewer)
			Dim gridItem As GridDashboardItem = TryCast(viewer.Dashboard.Items(e.DashboardItemName), GridDashboardItem)
			If gridItem IsNot Nothing Then
				Dim cmdButtonItem As DashboardToolbarItem = New DashboardToolbarItem With {.ClickAction = Sub(args) SwitchGridMeasureColumnDisplayMode(gridItem), .SvgImage = svgImageCollection1("bluedatabarsolid"), .Tooltip = "Switch between bars and numbers"}
				e.Items.Insert(0, cmdButtonItem)
			End If

		End Sub

		Private Sub SwitchGridMeasureColumnDisplayMode(ByVal gridItem As GridDashboardItem)
			For Each column In gridItem.Columns
				Dim measureColumn As GridMeasureColumn = TryCast(column, GridMeasureColumn)
				If measureColumn IsNot Nothing Then
					Select Case measureColumn.DisplayMode
						Case GridMeasureColumnDisplayMode.Bar
							measureColumn.DisplayMode = GridMeasureColumnDisplayMode.Value
						Case GridMeasureColumnDisplayMode.Value
							measureColumn.DisplayMode = GridMeasureColumnDisplayMode.Bar
					End Select
				End If
			Next column
		End Sub
        Private Sub DashboardViewer1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DashboardConfigureDataConnectionEventArgs)
            Dim connParams As ExcelDataSourceConnectionParameters = TryCast(e.ConnectionParameters, ExcelDataSourceConnectionParameters)
            If connParams IsNot Nothing Then
                connParams.FileName = "SalesPerson.xlsx"
            End If
        End Sub
    End Class
End Namespace
