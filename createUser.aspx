<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.Master" CodeBehind="createUser.aspx.cs" Inherits="VMS.createUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-label {
            font-weight: 500;
        }
        .form-container {
            max-width: 600px;
            margin: 0 auto;
            padding: 2rem;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form>
    <div class="container mt-5">
        <div class="form-container">
            <h4 class="text-center mb-4">Create User</h4>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            
            <div class="mb-3">
                <label for="txtuserName" class="form-label">User Name</label>
                <asp:TextBox ID="txtuserName" OnTextChanged="txtuserName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtempName" class="form-label">Employee Name</label>
                <asp:TextBox ID="txtempName" runat="server" CssClass="form-control" placeholder="Enter Employee Name"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtempid" class="form-label">EMP ID</label>
                <asp:TextBox ID="txtempid" runat="server" OnTextChanged="txtempid_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Enter Emp Id"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtemail" class="form-label">Email</label>
                <asp:TextBox ID="txtemail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
            </div>
            
            <div class="mb-3">
                <label for="txtActive" class="form-label">Active</label>
                <asp:TextBox ID="txtActive" runat="server"  CssClass="form-control" placeholder="Active(1) Or InActive(0)"></asp:TextBox>
            </div>
            
            <div class="mb-3">
                <label for="" class="form-label">Password</label>
                <asp:TextBox ID="TextBox1" runat="server"  CssClass="form-control" Text="pass" Enabled="false"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary me-2" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger me-2" />
            </div>
        </div>
    </div>
        </form>
</asp:Content>
