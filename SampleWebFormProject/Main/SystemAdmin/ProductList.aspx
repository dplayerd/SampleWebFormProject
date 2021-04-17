<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Main.SystemAdmin.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-3.6.0.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <a href="ProductDetail.aspx">新增</a>

    <div>
        進階搜尋：
        <p> 
            Caption: <asp:TextBox runat="server" ID="txtCaption"></asp:TextBox> 
            Type: 
            <asp:DropDownList runat="server" ID="ddlProductType">
                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="農機" Value="1"></asp:ListItem>
                <asp:ListItem Text="門禁系統" Value="2"></asp:ListItem>
                <asp:ListItem Text="電池" Value="3"></asp:ListItem>
            </asp:DropDownList>
            Price:
            <asp:TextBox runat="server" ID="txtPrice1" placeholder="最小值"></asp:TextBox>~
            <asp:TextBox runat="server" ID="txtPrice2" placeholder="最大值"></asp:TextBox>
            
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </p>
    </div>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Caption">
                <ItemTemplate>
                    <a href="ProductDetail.aspx?ID=<%# Eval("ID") %>">
                    <%# Eval("Caption") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProductType">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltProductType" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0: #.00}" />

            <asp:BoundField DataField="CurrentQty" HeaderText="CurrentQty" />
            <asp:BoundField DataField="LockedQty" HeaderText="LockedQty" />
            <asp:BoundField DataField="ModifyDate" HeaderText="ModifyDate" />
            <asp:BoundField DataField="ModifierAccount" HeaderText="ModifierAccount" />

            <asp:TemplateField HeaderText="Act">
                <ItemTemplate>
                <asp:Button runat="server" ID="btnDelete" Text="Del" CommandName="DeleteItem" 
                CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure?');" />

                <input type="hidden" value="<%# Eval("ID") %>" />
                <button type="button" class="EditStock">Edit Stock</button>
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

    <br />
    <br />
    <br />
    <div id="divStockArea">
        <table border="1">
            <tr><td>CurrentQty</td> <td><input type="number" name="CurrentQty" min="0" /></td></tr>
            <tr><td>LockedQty</td>  <td><input type="number" name="LockedQty" min="0" /></td></tr>
        </table>
        <input type="hidden" id="StockID" />
        <button type="button" id="SaveStock">Save</button>
        <button type="button" id="CancelStockEdit">Cancel</button>
    </div>


    <script>
        $(document).ready(function () {
            $("#divStockArea").hide(300);

            $(".EditStock").click(function () {
                $(".EditStock").hide(300);

                var id = $(this).closest("td").find(":hidden").val();
                var url = "/SystemAdmin/Handlers/StockHandler.ashx?ID=" + id;

                $.ajax({
                    url: url,
                    method: "GET",
                    dataType: "JSON"
                }).done(function (responseData) {
                    $("input[name=CurrentQty]").val(responseData.CurrentQty);
                    $("input[name=LockedQty]").val(responseData.LockedQty);
                    $("#StockID").val(id);

                    $("#divStockArea").show(300);
                });
            });

            $("#SaveStock").click(function () {
                var id = $("#StockID").val();
                var url = "/SystemAdmin/Handlers/StockHandler.ashx?ID=" + id;

                $.ajax({
                    url: url,
                    method: "POST",
                    dataType: "JSON", 
                    data: {
                        CurrentQty: $("input[name=CurrentQty]").val(),
                        LockedQty: $("input[name=LockedQty]").val()
                    }
                }).done(function (responseData) {
                    alert("更新成功");

                    $("#divStockArea").hide(300);
                    $(".EditStock").show(300);
                });
            });

            $("#CancelStockEdit").click(function () {
                $("input[name=CurrentQty]").val("0");
                $("input[name=LockedQty]").val("0");

                $("#divStockArea").hide(300);
                $(".EditStock").show(300);
            });
        });
    </script>
</asp:Content>
