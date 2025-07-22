<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VMSMaster.Master" CodeBehind="changePass.aspx.cs" Inherits="VMS.changePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 400px;
            margin: 0 auto;
            margin-top: 50px;
            padding: 30px;
            border: 1px solid #ddd;
            border-radius: 15px;
            background-color: #f9f9f9;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .form-label {
            font-weight: 600;
            margin-top: 10px;
        }

        .asp-textbox {
            width: 100%;
            padding: 10px;
            margin-top: 5px;
            margin-bottom: 15px;
            border-radius: 8px;
            border: 1px solid #ccc;
        }

        .btn-primary {
            width: 100%;
            padding: 10px;
            border-radius: 8px;
        }

        #lblMessage {
            color: red;
            font-weight: bold;
        }

        @media (max-width: 576px) {
            .form-container {
                padding: 20px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-container">
        
        <label ID="Label1" runat="server" CssClass="form-label" style ="text-align:center;background-color:black;color:white;width:100%;font-size:25px;border-radius:10px;font-weight:bold" Text="">CHANGE PASSWORD</label><br /><br />
        <label ID="lblMessage" runat="server" CssClass="form-label" style ="text-align:center;background-color:white;"></label><br /><br />

        <label for="txtPassword" class="form-label">Current Password</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="asp-textbox" TextMode="Password" placeholder="Enter current password" ></asp:TextBox>
        

        <label for="txtNewPass" class="form-label">New Password</label>
        <asp:TextBox ID="txtNewPass" runat="server" CssClass="asp-textbox" TextMode="Password" placeholder="Enter new password"></asp:TextBox>

        <label for="txtConfirm" class="form-label">Confirm Password</label>
        <asp:TextBox ID="txtConfirm" runat="server" CssClass="asp-textbox" TextMode="Password" placeholder="Confirm new password"></asp:TextBox>

        <asp:Button ID="btnchange" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnchange_Click" />
        <input type="reset" class="btn btn-secondary mt-2 w-100" value="Reset" />
    </div>
</asp:Content>
