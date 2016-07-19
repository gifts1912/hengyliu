<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TableOnBoardingV2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>Bot&nbsp;&nbsp;&nbsp;&nbsp; Name: </td>
            <td> 
                <asp:TextBox ID="TextBox1" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Table Name: </td>
            <td> 
                <asp:TextBox ID="TextBox2" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>Table&nbsp;&nbsp;&nbsp; File: </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" />
            </td>
            <td> 
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
    </table>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="ButtonGenerateSchema_Click" Text="GenerateSchema" />

    <div>
        <asp:GridView ID="GridView1" runat="server"
                      AutoGenerateColumns="False"
                      OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
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
        </Columns>
        </asp:GridView>
        <br />
        <%--
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
        --%>
        <asp:Button ID="SchemaSubmit" runat="server" OnClick="SchemaSubmit_Click" Text="Submit" />
        <p>
            <asp:Label ID="SubmitResult" runat="server" />
        </p> 
    </div>
</asp:Content>
