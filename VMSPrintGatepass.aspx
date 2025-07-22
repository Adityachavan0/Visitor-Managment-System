<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VMSPrintGatepass.aspx.cs" Inherits="VMS.VMSPrintGatepass" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Visitor Pass</title>
    <style>
        .pass-container {
            width: 400px;
            border: 2px solid black;
            padding: 20px;
            text-align: center;
            font-family: Arial, sans-serif;
        }

        .company-logo {
            width: 100px;
            height: auto;
            display: block;
            margin: 0 auto 10px auto;
        }

        .btn-print {
            margin-top: 10px;
            padding: 10px;
            background-color: blue;
            color: white;
            border: none;
            cursor: pointer;
        }

        .label-title {
            font-weight: bold;
        }

        .label-value {
            display: inline-block;
            margin-left: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pass-container">
            <img src="assets/images/logo/file.jpg" alt="Company Logo" class="company-logo" />
            <h2>Visitor Pass</h2>

            <p><span class="label-title">ID:</span> <asp:Label ID="lblVisitorId" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Name:</span> <asp:Label ID="lblVisitorName" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">ID Type:</span> <asp:Label ID="lblIDType" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">ID No:</span> <asp:Label ID="lblIDNumber" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Pax:</span> <asp:Label ID="lblPax" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Vehicle:</span> <asp:Label ID="lblVehicle" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Valid From:</span> <asp:Label ID="lblValidFrom" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Valid Till:</span> <asp:Label ID="lblValidTill" runat="server" CssClass="label-value" /></p>
            <p><span class="label-title">Remarks:</span> <asp:Label ID="lblRemarks" runat="server" CssClass="label-value" /></p>

            <button class="btn-print" onclick="window.print(); return false;">Print</button>
        </div>
    </form>
</body>
</html>
