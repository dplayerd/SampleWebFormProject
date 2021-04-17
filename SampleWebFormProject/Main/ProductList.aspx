<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Main.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="pricing-table pricing-three-column row">

            <asp:Repeater runat="server" ID="repList" OnItemDataBound="repList_ItemDataBound">
                <ItemTemplate>

            <div class="plan col-sm-4 col-lg-4">
                <div class="plan-name-bronze">
                    <h2><%# Eval("Caption") %></h2>
                    <span><%# Eval("Price", "{0:#}") %></span>
                </div>
                <ul>
                    <li class="plan-feature">
                        <asp:Image runat="server" ID="img" Width="100" Height="100" Visible="false" />
                    </li>

                    <li class="plan-feature">
                        <%# (Eval("Body") as string) %>
                    </li>
                    
                    <li class="plan-feature"><a href="ProductDetail.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary btn-plan-select"><i class="icon-white icon-ok"></i>Select</a></li>
                </ul>
            </div>


                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
