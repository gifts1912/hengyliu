<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableSchemaV2.aspx.cs" Inherits="TableOnBoardingV2.TableSchemaV2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GridView Data Manipulation</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server"
                      AutoGenerateColumns="False"
                      ShowFooter="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                      DataKeyNames="TableHeader, IsSubject, Schema, Type, NeedIndex, RegexForValue, NL"
            >
        <Columns>
            <asp:BoundField DataField="TableHeader"
                            HeaderText="TableHeader"/>
            <asp:TemplateField HeaderText="IsSubject">
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList_IsSubject" DataTextField ="IsSubjectName" DataValueField="IsSubjectID" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="1">True</asp:ListItem>
                        <asp:ListItem Value="2">False</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Schema" HeaderText="Schema"/>
            <%-- 
            <asp:BoundField DataField="Type"
                            HeaderText="Type" />
            --%>
            <asp:TemplateField HeaderText ="Type">
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList_Type" DataTextField="TypeName" DataValueField ="TypeID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Type_SelectedIndexChanged">
                        <asp:ListItem Value="1">String</asp:ListItem>
                        <asp:ListItem Value="2">Bool</asp:ListItem>
                        <asp:ListItem Value="3">Numeric</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NeedIndex">
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList_NeedIndex" DataTextField ="NeedIndexName" DataValueField="NeedIndexID" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="1">True</asp:ListItem>
                        <asp:ListItem Value="2">False</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="RegexForValue" HeaderText="RegexForValue"/>
            <asp:BoundField DataField="NL" HeaderText="NL"/>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
            <%--
            <asp:TemplateField>
                <FooterTemplate>
                    <asp:LinkButton ID="SchemaAdd" runat="server" CommandName="Select">Insert</asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            --%>
        </Columns>
        </asp:GridView>

        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100px; height: 19px;">
                    TableHeader</td>
                <td style="width: 100px; height: 19px;">
                    IsSubject</td>
                <td style="width: 100px; height: 19px;">
                    Schema</td>
                <td style="width: 100px; height: 19px;">
                    Type</td>
                <td style="width: 100px; height: 19px;">
                    NeedIndex</td>
                <td style="width: 100px; height: 19px;">
                    RegexForValue</td>
                <td style="width: 100px; height: 19px;">
                    NL</td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox1" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox2" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox3" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox4" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox5" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox6" runat="server"/></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox7" runat="server"/></td>
                <td style="width: 100px">
                    <asp:Button ID="Button1" runat="server"
                                Text="Add New"
                                OnClick="ButtonAdd_Click" />
            </tr>
        </table>
    </div>
    </form>
</body>
</html>