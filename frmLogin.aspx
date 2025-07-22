<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="VMS.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
<style>
    body {
        height: 100vh;
        margin: 0;
        background: #0a2e38;
        display: flex;
        align-items: center;
        justify-content: center;
        font-family: 'Segoe UI', Arial, sans-serif;
    }
    .login-bg {
        width: 420px;
        padding: 48px 32px 32px 32px;
        border-radius: 12px;
        background: linear-gradient(135deg, #0a2e38 60%, #174d5c 100%);
        box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.2);
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .login-title {
        color: #fff;
        font-size: 2rem;
        font-weight: 700;
        letter-spacing: 1px;
        margin-bottom: 36px;
    }
    .input-group-custom {
        width: 100%;
        margin-bottom: 24px;
        position: relative;
    }
    .input-icon {
        position: absolute;
        left: 16px;
        top: 50%;
        transform: translateY(-50%);
        color: #174d5c;
        font-size: 1.3rem;
        background: #fff;
        border-radius: 50%;
        width: 40px;
        height: 40px;
        display: flex;
        align-items: center;
        justify-content: center;
        box-shadow: 0 2px 8px rgba(31,38,135,0.08);
    }
    .input-icon-right {
        right: 16px;
        left: auto;
    }
    .asp-textbox {
        width: 100%;
        padding: 12px 56px 12px 56px;
        border: none;
        border-radius: 24px;
        background: rgba(255,255,255,0.15);
        color: #fff;
        font-size: 1rem;
        font-weight: 500;
        outline: none;
        transition: background 0.2s;
    }
    .asp-textbox::placeholder {
        color: #b0c4c9;
        opacity: 1;
    }
    .asp-textbox:focus {
        background: rgba(255,255,255,0.25);
    }
    .btn-login {
        width: 100%;
        padding: 14px 0;
        border: none;
        border-radius: 24px;
        background: #fff;
        color: #0a2e38;
        font-size: 1.1rem;
        font-weight: 700;
        letter-spacing: 1px;
        box-shadow: 0 2px 8px rgba(31,38,135,0.08);
        transition: background 0.2s, color 0.2s;
    }
    .btn-login:hover {
        background: #174d5c;
        color: #fff;
    }
    .message-label {
        margin-bottom: 10px;
        color: #e74c3c;
        font-size: 14px;
        min-height: 18px;
        text-align: center;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="login-bg">
        <div class="login-title">LOGIN PAGE</div>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" ForeColor="Red"></asp:Label>
        <div class="input-group-custom">
            <span class="input-icon"><i class="fa-solid fa-user"></i></span>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="asp-textbox" placeholder="Username"></asp:TextBox>
        </div>
        <div class="input-group-custom">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="asp-textbox" TextMode="Password" placeholder="Password"></asp:TextBox>
            <span class="input-icon input-icon-right"><i class="fa-solid fa-lock"></i></span>
        </div>
        <asp:Button ID="btnLogin" runat="server" CssClass="btn-login" Text="LOGIN" OnClick="btnLogin_Click"  />
    </div>
</form>
</body>
</html>
