<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.Master" CodeBehind="resetpass.aspx.cs" Inherits="VMS.resetpass" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .main{
            display:flex;
            align-items:center;
            justify-content:center;
            flex-direction:column;
               padding:20px;
        }
        .upper {
            display: flex;
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="main">
        <asp:Label ID="lblMessage" style="margin:20px;margin-bottom:30px" runat="server"></asp:Label>
        <div class="upper">
            <h4>UserName</h4>
            <div>
                <asp:DropDownList ID="ddlUser" AutoPostBack="true" runat="server" Width="200px">
                    <asp:ListItem Value="0" Text="Select User" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
                
         <asp:Button ID="btnChangePassword" runat="server" style="margin:20px;" class="btn btn-success" Text="Reset Password" OnClick="btnChangePassword_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" style="margin:20px;" class="btn btn-danger" OnClick="btnCancel_Click"/>
    </div>
</asp:Content>