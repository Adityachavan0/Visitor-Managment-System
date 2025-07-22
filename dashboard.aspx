<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="VMS.dashboard" %>

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

        .pass-container {
            display: none; /* hidden by default, used for print preview if needed */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive">
        <asp:GridView ID="gvVisitors" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
            DataKeyNames="VisitorId" OnRowCommand="gvVisitors_RowCommand" OnRowDataBound="gvVisitors_RowDataBound">
            <Columns>
                <asp:BoundField DataField="VisitorId" HeaderText="ID" />
                <asp:BoundField DataField="FullName" HeaderText="Name" />
                <asp:BoundField DataField="IDType" HeaderText="ID Type" />
                <asp:BoundField DataField="IDNumber" HeaderText="ID No" />
                <asp:BoundField DataField="Pax" HeaderText="Pax" />
                <asp:BoundField DataField="Vehicle" HeaderText="Vehicle" />
                <asp:BoundField DataField="ValidFrom" HeaderText="Valid From" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="ValidTill" HeaderText="Valid Till" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnPDF" runat="server" Text="Print PDF" CommandName="PrintPDF" 
                                    CommandArgument='<%# Eval("VisitorId") %>' CssClass="btn-print" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
