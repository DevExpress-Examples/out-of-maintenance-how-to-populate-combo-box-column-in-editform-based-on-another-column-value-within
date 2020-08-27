<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="mainForm" runat="server">
	<div>
		<dx:ASPxGridView ID="ASPxGridViewDemo" runat="server" AutoGenerateColumns="False"
			KeyFieldName="ID" 
			OnCellEditorInitialize="ASPxGridViewDemo_CellEditorInitialize" 
			onrowupdating="ASPxGridViewDemo_RowUpdating">
			<Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowClearFilterButton="True"/>
				<dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="2">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="IssueTypeName" VisibleIndex="3" Caption="Issue Type">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataComboBoxColumn FieldName="Type" VisibleIndex="4" Caption="Issue Status">
				</dx:GridViewDataComboBoxColumn>
			</Columns>
		</dx:ASPxGridView>
	</div>
	</form>
</body>
</html>