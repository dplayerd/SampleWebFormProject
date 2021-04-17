<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFileUpload.aspx.cs" Inherits="Main.TestFileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <img runat="server" id="img1" src="/FileDownload/child.jpg" />
        <br />

        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
        <br />
        <asp:Label runat="server" ID="lbMsg" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
