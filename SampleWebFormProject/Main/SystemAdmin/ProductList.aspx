<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Main.SystemAdmin.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <a href="ProductDetail.aspx">新增</a>

   <%-- <div>
        進階搜尋：
        <p> 
            Name: <asp:TextBox runat="server" ID="txtName"></asp:TextBox> 
            Level: 
            <asp:RadioButtonList runat="server" ID="rdblLevel" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="Normal" Value="0"></asp:ListItem>
                <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                <asp:ListItem Text="Employee" Value="2"></asp:ListItem>
                <asp:ListItem Text="Supervisor" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </p>
    </div>--%>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Caption">
                <ItemTemplate>
                    <a href="MemberDetail.aspx?ID=<%# Eval("ID") %>">
                    <%# Eval("Caption") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltProductType" />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0: #.00}" />

            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                <asp:Button runat="server" ID="btnDelete" Text="Del" CommandName="DeleteItem" 
                CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <%--<asp:Repeater runat="server" ID="repPaging">
        <ItemTemplate>
            <a href="<%# Eval("Link") %>" title="<%# Eval("Title") %>">Page-<%# Eval("Name") %></a>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>--%>

</asp:Content>
