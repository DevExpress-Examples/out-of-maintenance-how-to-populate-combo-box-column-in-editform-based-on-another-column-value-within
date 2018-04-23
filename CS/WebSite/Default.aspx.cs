using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class _Default : System.Web.UI.Page {
    private static List<ComboBoxDataSourceItem> _Issues;
    protected static List<ComboBoxDataSourceItem> Issues {
        get {
            if (_Issues == null)
                _Issues = CreateDemoIssues();
            return _Issues;
        }
    }

    private static List<ComboBoxDataSourceItem> CreateDemoIssues() {
        List<ComboBoxDataSourceItem> comboBoxDataSource = new List<ComboBoxDataSourceItem>();

        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Bug, IssueStatus = "ByDesign" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Bug, IssueStatus = "Fixed" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Bug, IssueStatus = "WontFix" });

        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Suggestion, IssueStatus = "Active" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Suggestion, IssueStatus = "Implemented" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Suggestion, IssueStatus = "Planned" });

        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Question, IssueStatus = "Answered" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Question, IssueStatus = "Processed" });
        comboBoxDataSource.Add(new ComboBoxDataSourceItem() { Type = IssueType.Question, IssueStatus = "Outdated" });

        return comboBoxDataSource;
    }

    protected void Page_Load(object sender, EventArgs e) {
        ASPxGridViewDemo.DataSource = GetGridViewDataSource();

        GridViewDataComboBoxColumn comboBoxColumn = (GridViewDataComboBoxColumn)ASPxGridViewDemo.Columns["Type"];
        comboBoxColumn.PropertiesComboBox.DataSource = Issues;
        comboBoxColumn.PropertiesComboBox.ValueField = "Type";
        comboBoxColumn.PropertiesComboBox.TextField = "IssueStatus";
        comboBoxColumn.PropertiesComboBox.ValueType = typeof(string);

        ASPxGridViewDemo.DataBind();
    }
    private List<DataSourceItem> GetGridViewDataSource() {
        List<DataSourceItem> dataSource = new List<DataSourceItem>();

        dataSource.Add(new DataSourceItem() {
            ID = 1,
            IssueTypeName = IssueType.Bug,
            Type = IssueType.Bug,
            Subject = "Treelist shows incorrect values."
        });

        dataSource.Add(new DataSourceItem() {
            ID = 2,
            IssueTypeName = IssueType.Suggestion,
            Type = IssueType.Suggestion,
            Subject = "Customization Window suggestions."
        });

        dataSource.Add(new DataSourceItem() {
            ID = 3,
            IssueTypeName = IssueType.Question,
            Type = IssueType.Question,
            Subject = "How to populate a Combo Box on the Fly?"
        });

        return dataSource;
    }
    protected void ASPxGridViewDemo_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        if (e.Column != null && e.Column.FieldName == "Type") {
            ASPxComboBox comboBox = (ASPxComboBox)e.Editor;
            ASPxGridView gridView = (ASPxGridView)sender;

            IssueType issueType = (IssueType)gridView.GetRowValuesByKeyValue(e.KeyValue, "IssueTypeName");

            /*var filterValues = from issue in Issues
                               where issue.Type == issueType
                               select issue;*/
            var filterValues = Issues.Where(issue => issue.Type == issueType);

            comboBox.DataSource = filterValues;
            comboBox.DataBindItems();
        }
    }
    protected void ASPxGridViewDemo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
        throw new InvalidOperationException("Updating is not supported");
    }
}

public enum IssueType {
    Bug,
    Suggestion,
    Question
}

public class DataSourceItem {
    public int ID { get; set; }
    public IssueType IssueTypeName { get; set; }
    public IssueType Type { get; set; }
    public string Subject { get; set; }
}

public class ComboBoxDataSourceItem {
    public IssueType Type { get; set; }
    public string IssueStatus { get; set; }
}