<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="Main.ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="1">
        <tr><td>Caption</td><td><asp:Literal runat="server" ID="ltCaption"></asp:Literal></td></tr>
        <tr><td>Type</td><td><asp:Literal runat="server" ID="ltType"></asp:Literal></td></tr>
        <tr><td>Price</td><td><asp:Literal runat="server" ID="ltPrice"></asp:Literal></td></tr>
        <tr><td>Body</td><td><asp:Literal runat="server" ID="ltBody"></asp:Literal></td></tr>

        <tr>
            <td>Pic1</td>
            <td>
                <asp:Image runat="server" ID="img1" Width="150" Height="150" />
            </td>
        </tr>

        <tr>
            <td>Pic2</td>
            <td>
                <asp:Image runat="server" ID="img2" Width="150" Height="150" />
            </td>
        </tr>

    </table>

</asp:Content>
