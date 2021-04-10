<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MamberList.aspx.cs" Inherits="Main.SystemAdmin.MamberList" %>
<%@ Import Namespace="CoreProject.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <a href="MemberDetail.aspx">新增</a>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Account">
                <ItemTemplate>
                    <a href="MemberDetail.aspx?ID=<%# Eval("ID") %>">
                    <%# Eval("Account") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:TemplateField HeaderText="Level">
                <ItemTemplate>
                    <%# (UserLevel)Eval("UserLevel") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                <asp:Button runat="server" ID="btnDelete" Text="Del" CommandName="DeleteItem" 
                CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
</asp:Content>
