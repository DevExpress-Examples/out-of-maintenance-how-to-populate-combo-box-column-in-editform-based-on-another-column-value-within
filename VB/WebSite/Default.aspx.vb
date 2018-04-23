Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private Shared _Issues As List(Of ComboBoxDataSourceItem)
	Protected Shared ReadOnly Property Issues() As List(Of ComboBoxDataSourceItem)
		Get
			If _Issues Is Nothing Then
				_Issues = CreateDemoIssues()
			End If
			Return _Issues
		End Get
	End Property

	Private Shared Function CreateDemoIssues() As List(Of ComboBoxDataSourceItem)
		Dim comboBoxDataSource As List(Of ComboBoxDataSourceItem) = New List(Of ComboBoxDataSourceItem)()

		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Bug, .IssueStatus = "ByDesign"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Bug, .IssueStatus = "Fixed"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Bug, .IssueStatus = "WontFix"})

		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Suggestion, .IssueStatus = "Active"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Suggestion, .IssueStatus = "Implemented"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Suggestion, .IssueStatus = "Planned"})

		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Question, .IssueStatus = "Answered"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Question, .IssueStatus = "Processed"})
		comboBoxDataSource.Add(New ComboBoxDataSourceItem() With {.Type = IssueType.Question, .IssueStatus = "Outdated"})

		Return comboBoxDataSource
	End Function

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ASPxGridViewDemo.DataSource = GetGridViewDataSource()

		Dim comboBoxColumn As GridViewDataComboBoxColumn = CType(ASPxGridViewDemo.Columns("Type"), GridViewDataComboBoxColumn)
		comboBoxColumn.PropertiesComboBox.DataSource = Issues
		comboBoxColumn.PropertiesComboBox.ValueField = "Type"
		comboBoxColumn.PropertiesComboBox.TextField = "IssueStatus"
		comboBoxColumn.PropertiesComboBox.ValueType = GetType(String)

		ASPxGridViewDemo.DataBind()
	End Sub
	Private Function GetGridViewDataSource() As List(Of DataSourceItem)
		Dim dataSource As List(Of DataSourceItem) = New List(Of DataSourceItem)()

		dataSource.Add(New DataSourceItem() With {.ID = 1, .IssueTypeName = IssueType.Bug, .Type = IssueType.Bug, .Subject = "Treelist shows incorrect values."})

		dataSource.Add(New DataSourceItem() With {.ID = 2, .IssueTypeName = IssueType.Suggestion, .Type = IssueType.Suggestion, .Subject = "Customization Window suggestions."})

		dataSource.Add(New DataSourceItem() With {.ID = 3, .IssueTypeName = IssueType.Question, .Type = IssueType.Question, .Subject = "How to populate a Combo Box on the Fly?"})

		Return dataSource
	End Function
	Protected Sub ASPxGridViewDemo_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		If e.Column IsNot Nothing AndAlso e.Column.FieldName = "Type" Then
			Dim comboBox As ASPxComboBox = CType(e.Editor, ASPxComboBox)
			Dim gridView As ASPxGridView = CType(sender, ASPxGridView)

			Dim issueType As IssueType = CType(gridView.GetRowValuesByKeyValue(e.KeyValue, "IssueTypeName"), IssueType)

'            var filterValues = from issue in Issues
'                               where issue.Type == issueType
'                               select issue;
			Dim filterValues = Issues.Where(Function(issue) issue.Type = issueType)

			comboBox.DataSource = filterValues
			comboBox.DataBindItems()
		End If
	End Sub
	Protected Sub ASPxGridViewDemo_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Throw New InvalidOperationException("Updating is not supported")
	End Sub
End Class

Public Enum IssueType
	Bug
	Suggestion
	Question
End Enum

Public Class DataSourceItem
	Private privateID As Integer
	Public Property ID() As Integer
		Get
			Return privateID
		End Get
		Set(ByVal value As Integer)
			privateID = value
		End Set
	End Property
	Private privateIssueTypeName As IssueType
	Public Property IssueTypeName() As IssueType
		Get
			Return privateIssueTypeName
		End Get
		Set(ByVal value As IssueType)
			privateIssueTypeName = value
		End Set
	End Property
	Private privateType As IssueType
	Public Property Type() As IssueType
		Get
			Return privateType
		End Get
		Set(ByVal value As IssueType)
			privateType = value
		End Set
	End Property
	Private privateSubject As String
	Public Property Subject() As String
		Get
			Return privateSubject
		End Get
		Set(ByVal value As String)
			privateSubject = value
		End Set
	End Property
End Class

Public Class ComboBoxDataSourceItem
	Private privateType As IssueType
	Public Property Type() As IssueType
		Get
			Return privateType
		End Get
		Set(ByVal value As IssueType)
			privateType = value
		End Set
	End Property
	Private privateIssueStatus As String
	Public Property IssueStatus() As String
		Get
			Return privateIssueStatus
		End Get
		Set(ByVal value As String)
			privateIssueStatus = value
		End Set
	End Property
End Class