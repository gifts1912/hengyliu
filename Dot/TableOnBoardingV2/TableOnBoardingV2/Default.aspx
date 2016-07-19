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
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
    </table>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
</asp:Content>
