<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="Main.SystemAdmin.ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="1">
        <tr>
            <td>Caption</td>
            <td>
                <asp:TextBox runat="server" ID="txtCaption"></asp:TextBox></td>
        </tr>
        <tr>
            <td>ProductType</td>
            <td>
                <asp:RadioButtonList ID="rdblProductType" runat="server">
                    <asp:ListItem Text="農機" Value="1"></asp:ListItem>
                    <asp:ListItem Text="門禁系統" Value="2"></asp:ListItem>
                    <asp:ListItem Text="電池" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>Price</td>
            <td>
                <asp:TextBox runat="server" ID="txtPrice" TextMode="Number"></asp:TextBox></td>
        </tr>
         <tr>
            <td>Body</td>
            <td>
                <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine" Rows="5" Columns="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>IsEnabled</td>
            <td>
                <asp:CheckBox runat="server" ID="ckbIsEnabled" />
            </td>
        </tr>
        
    </table>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    &nbsp;
    &nbsp;
    &nbsp;
    <a href="ProductList.aspx">回上頁</a><br />
    <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>

    <asp:HiddenField runat="server" ID="hfMsg" Value="" />

</asp:Content>
