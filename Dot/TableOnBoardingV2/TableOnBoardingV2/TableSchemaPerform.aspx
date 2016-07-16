<%@ Import Namespace="System.Data" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableSchemaPerform.aspx.cs" Inherits="TableOnBoardingV2.TableSchemaPerform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="SchemaContent" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#990000" Font-Bold="true" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="TableHeader" HeaderText="TableHeader" ReadOnly="true" />
                <asp:BoundField DataField="IsSubject" HeaderText="IsSubject" />
                <asp:BoundField DataField="Schema" HeaderText ="Schema" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="NeedIndex" HeaderText="NeedIndex" />
                <asp:BoundField DataField ="RegexForValue" HeaderText="RegexForValue" />
                <asp:BoundField DataField="NL" HeaderText="NL" />
            </Columns>
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </form>
</body>
</html>