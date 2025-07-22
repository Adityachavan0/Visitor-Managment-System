<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.Master" CodeBehind="userList.aspx.cs" Inherits="VMS.userList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media (max-width: 768px) {
            .table-responsive {
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
            }
        }

        .table th {
            white-space: nowrap;
            background-color: red;
            text-align: center;
        }

        .table td {
            white-space: nowrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script>
        function Edit() {
            if (confirm("Confirm TO Check Out")) {
                alert("Checkout Successfiull1")
            }
            else {
                alert("Checkout Not Successfiull1")
            }
        }
    </script>
    <div class="table-responsive" id="GridDate" runat="server">
        <asp:GridView ID="gvVisitors" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="id">
            <Columns>

                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="Edit" runat="server" Text="Edit"
                            CommandArgument='<%# Eval("id") %>'
                            OnClick="Edit_Click" style ="background:#007bff;"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="Name" />
                <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                <asp:BoundField DataField="EmpId" HeaderText="Emp Id" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="isActive" HeaderText="Active" />
                <asp:BoundField DataField="createdate" HeaderText="Create Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField DataField="Password" HeaderText="Password" />


            </Columns>
        </asp:GridView>

    </div>

    <div class="container mt-5" id="EditUser" runat="server" visible="false" style="width:50%">
        <div class="form-container">
            <h4 class="text-center mb-4">Edit User</h4>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

            <div class="mb-3">
                <label for="txtuserName" class="form-label">User Name</label>
                <asp:TextBox ID="txtuserName" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server"
        ControlToValidate="txtCustomerName" ErrorMessage="Enter Customer Name"
        ForeColor="Red" ValidationGroup="MilkSellGroup" Display="Dynamic" />
</div>
            </div>

            <div class="mb-3">
                <label for="txtempName" class="form-label">Employee Name</label>
                <asp:TextBox ID="txtempName" runat="server" CssClass="form-control" placeholder="Enter Employee Name"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtempid" class="form-label">EMP ID</label>
                <asp:TextBox ID="txtempid" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="Enter Emp Id"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtemail" class="form-label">Email</label>
                <asp:TextBox ID="txtemail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtActive" class="form-label">Active</label>
                <asp:TextBox ID="txtActive" runat="server" CssClass="form-control" placeholder="Active(1) Or InActive(0)"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtActive" class="form-label">Password</label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-primary me-2" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger me-2" />
            </div>
        </div>
    </div>
</asp:Content>
