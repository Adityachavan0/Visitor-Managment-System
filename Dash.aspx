<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.Master" AutoEventWireup="true" CodeBehind="Dash.aspx.cs" Inherits="VMS.Dash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-print {
            padding: 6px 12px;
            background-color: #007bff;
            border: none;
            color: white;
            cursor: pointer;
        }

        .btn-print:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <h3 class="text-center">Visitor Dashboard</h3>
<div class="table-responsive">
    <asp:GridView ID="gvVisitors" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
        DataKeyNames="VisitorId" OnRowCommand="gvVisitors_RowCommand">
        <Columns>
            <asp:BoundField DataField="VisitorId" HeaderText="ID" />
            <asp:BoundField DataField="FullName" HeaderText="Name" />
            <asp:BoundField DataField="Company" HeaderText="Company" />
            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ValidFrom" HeaderText="From" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
            <asp:BoundField DataField="ValidTill" HeaderText="Till" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnPrint" runat="server" Text="Print Pass" CommandName="PrintPDF"
                        CommandArgument='<%# Eval("VisitorId") %>' CssClass="btn-print" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
    </div>
</asp:Content>
