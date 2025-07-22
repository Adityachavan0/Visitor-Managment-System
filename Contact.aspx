<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VMSMaster.Master" CodeBehind="Contact.aspx.cs" Inherits="VMS.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .contact-container {
            display: block;
            flex-wrap: wrap;
            gap: 32px;
            margin-top: 40px;
        }
        .contact-info {
            flex: 1 1 260px;
            min-width: 220px;
            max-width: 320px;
            color: #444;
            font-size: 16px;
        }
        .contact-info .info-row {
            display: flex;
            align-items: flex-start;
            margin-bottom: 24px;
        }
        .contact-info .info-row i {
            font-size: 24px;
            color: #f5c518;
            margin-right: 16px;
            margin-top: 2px;
        }
        .contact-info .info-row .info-details {
            line-height: 1.3;
        }
        .contact-form {
            flex: 2 1 400px;
            min-width: 300px;
            display: flex;
            flex-direction: column;
            gap: 16px;
        }
        .contact-form-row {
            display: flex;
            gap: 16px;
        }
        .contact-form input, .contact-form textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #eee;
            border-radius: 4px;
            font-size: 16px;
            margin-bottom: 0;
            background: #fff;
        }
        .contact-form textarea {
            min-height: 120px;
            resize: vertical;
        }
        .contact-form label {
            font-size: 14px;
            color: #888;
            margin-bottom: 4px;
        }
        .contact-form .send-btn {
            background: #f5c518;
            color: #fff;
            font-weight: bold;
            border: none;
            border-radius: 4px;
            padding: 14px 32px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 16px;
            transition: background 0.2s;
        }
        .contact-form .send-btn:hover {
            background: #e6b800;
        }
        .send-btn:hover{
            background:white;
            color:black;
        }
        @media (max-width: 700px) {
            .contact-container {
                flex-direction: column;
                gap: 0;
            }
            .contact-info, .contact-form {
                max-width: 100%;
            }
            .contact-form-row {
                flex-direction: column;
                gap: 0;
            }
        }
    </style>
    <!-- Font Awesome for icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contact-container">
        <div class="contact-info">
            <div class="info-row">
                <i class="fa fa-home" style="color:black"></i>
                <div class="info-details">
                    <b>MP TECH SOLUTIONS,<br />Lonavala</b><br />
                    Pune, Maharashtra
                </div>
            </div>
            <div class="info-row">
                <i class="fa fa-phone" style="color:black"></i>
                <div class="info-details">
                    <b><a href="tel:7887718325" style="color:black">+91 7887718325</a> </b><br />
                    24/7 All Services
                </div>
            </div>
            <div class="info-row">
                <i class="fa fa-envelope" style="color:black"></i>
                <div class="info-details">
                    <b> <a href="mailto:mptechsolutions01@gmail.com" style="color:black">mptechsolutions01@gmail.com</a><br /></b>
                    Send us your query anytime!
                </div>
            </div>
        </div>
        <div class="contact-form">
            <div class="contact-form-row">
                <input type="text" placeholder="Name" />
            </div>
            <div class="contact-form-row">
                <input type="email" placeholder="Email" />
            </div>
            <div class="contact-form-row">
                <input type="text" placeholder="Phone" />
            </div>
            <div class="contact-form-row">
                <textarea placeholder="Message"></textarea>
            </div>
            <label style="color:#888; font-size:14px;">thats all? really?</label>
                    <b> <a href="mailto:mptechsolutions01@gmail.com">
             <button type="submit" class="send-btn" style="margin-bottom:20px ;width:100%; background-color:blue">SEND MESSAGE</button></a>
        </div>
    </div>
</asp:Content>
