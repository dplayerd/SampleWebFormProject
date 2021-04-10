<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MemberDetail.aspx.cs" Inherits="Main.SystemAdmin.MemberDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="1">
        <tr>
            <td>Account</td>
            <td>
                <asp:TextBox runat="server" ID="txtAccount"></asp:TextBox></td>
        </tr>
        <tr>
            <td>PWD</td>
            <td>
                <asp:TextBox runat="server" ID="txtPWD" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>New PWD</td>
            <td>
                <asp:TextBox runat="server" ID="txtNewPWD" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Title</td>
            <td>
                <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Phone</td>
            <td>
                <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email</td>
            <td>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Level</td>
            <td>
                <asp:RadioButtonList ID="rdblUserLevel" runat="server">
                    <asp:ListItem Text="Normal" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Employee" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Supervisor" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    &nbsp;
    &nbsp;
    &nbsp;
    <a href="MamberList.aspx">回上頁</a><br />
    <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>

    <asp:HiddenField runat="server" ID="hfMsg" Value="" />
</asp:Content>
