<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TableOnBoardingV4._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <label>&nbsp &nbsp Bot&nbsp &nbsp Name:</label>
    <asp:TextBox ID="TextBox1" runat="server" class="commentInput" TabIndex="1" />
    <br />
    <br />
    <label>&nbsp &nbsp Table Name:</label>
    <asp:TextBox ID="TextBox2" runat="server" class="commentInput" TabIndex="2" />
    <br />
    <br />
    <table>
        <td>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" OnClick="ButtonGenerateSchema_Click" Text="Upload" UseSubmitBehavior="false" />
        </td>
    </table>
    <br />
    <br />

    <asp:Button ID="ButtonHaveNoUse" runat="server" OnClick="ButtonHaveNoUse_Click" Text="EnterUseLessCatch" Visible="false" />

    <div style="width: 1460px">
        <asp:Panel ID="GetKeyBoardEnter0" runat="server" DefaultButton="ButtonHaveNoUse">
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                DataKeyNames="TableHeader,IsSubject,Schema,Type,NeedIndex,RegexForValue,NL" Width="80%" CellPadding="4" ForeColor="#333333" GridLines="Both">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="TableHeader" HeaderText="TableHeader" ControlStyle-CssClass="100%" />
                    <asp:TemplateField HeaderText="IsSubject">
                        <ItemTemplate>
                            <asp:Label ID="lblIsSubject" runat="server" Text='<%# Eval("IsSubject") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate> 
                            <asp:CheckBox ID="CheckBox_IsSubject" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsSubject")) %>' />
                            <%--
                            <asp:RadioButtonList ID="RadioButtonList_IsSubject" runat="server">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:RadioButtonList> --%>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Schema" HeaderText="Schema" />
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList_Type" DataTextField="TypeName" DataValueField="TypeID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Type_SelectedIndexChanged">
                                <asp:ListItem Value="1">String</asp:ListItem>
                                <asp:ListItem Value="2">Bool</asp:ListItem>
                                <asp:ListItem Value="3">Number</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NeedIndex">
                        <ItemTemplate>
                            <asp:Label ID="lblNeedIndex" runat="server" Text='<%# Eval("NeedIndex") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox_NeedIndex" runat="server" Checked='<%# Convert.ToBoolean(Eval("NeedIndex")) %>'>
                            </asp:CheckBox>
                            <%-- 
                            <asp:DropDownList ID="DropDownList_NeedIndex" DataTextField="NeedIndexName" DataValueField="NeedIndexID" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="1">True</asp:ListItem>
                                <asp:ListItem Value="2">False</asp:ListItem>
                            </asp:DropDownList> --%>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="RegexForValue" HeaderText="RegexForValue" />
                    <asp:BoundField DataField="NL" HeaderText="IndexTerm" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Link" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

        </asp:Panel>
        <br />
        <br />
        &nbsp &nbsp
        <asp:Label ID="Comment_Text" runat="server" Text="" Font-Italic="true" />
        <br />
        <br />
        <asp:Button ID="SchemaSubmit" runat="server" OnClick="SchemaSubmit_Click" Text="Submit" Visible="false" UseSubmitBehavior="false" />
        <br />
        <br />
        <p>
            &nbsp &nbsp
            <asp:Label ID="SubmitResult" runat="server" Font-Bold="true" />
        </p>
    </div>
</asp:Content>
