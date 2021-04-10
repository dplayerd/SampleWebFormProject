<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MamberList.aspx.cs" Inherits="Main.SystemAdmin.MamberList" %>
<%@ Import Namespace="CoreProject.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Account" HeaderText="Account" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%# (UserLevel)Eval("UserLevel") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
