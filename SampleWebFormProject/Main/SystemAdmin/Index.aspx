<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Main.SystemAdmin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr><td>帳號</td><td><asp:TextBox runat="server" ID="txtAccount" ></asp:TextBox></td></tr>
            <tr><td>密碼</td><td><asp:TextBox runat="server" ID="txtPWD" ></asp:TextBox></td></tr>
        </table>

        <asp:Button runat="server" ID="btn1" Text="Login" />
    </form>
</body>
</html>
