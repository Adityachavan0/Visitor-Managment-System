<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VMSMaster.Master" CodeBehind="Home.aspx.cs" Inherits="VMS.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        .visitor-form {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(31,38,135,0.08);
            padding: 2rem;
            margin-bottom: 2rem;
        }
        .visitor-form .form-group {
            margin-bottom: 1rem;
        }
        .visitor-form label {
            font-weight: 500;
            color: #2575fc;
        }
        .visitor-form input, .visitor-form select, .visitor-form textarea {
            
            padding: 10px;
            border-radius: 6px;
            border: 1px solid #ccc;
            margin-top: 5px;
        }
        .visitor-form .asp-textbox {
            width: 100%;
        }
        .visitor-form .form-actions {
            text-align: center;
            margin-top: 1.5rem;
        }
        /* Responsive: stack all fields vertically on mobile */
        @media (max-width: 768px) {
            .visitor-form {
                padding: 1rem;
            }
            .visitor-form .row {
                display: block;
            }
            .visitor-form .row > div {
                width: 100% !important;
                max-width: 100% !important;
                display: block;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function checkoutconfirm() {
            if (confirm("Confirm TO Check Out")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-md-10">
                <div class="visitor-form">
                    <div class="form-group d-flex justify-content-center mb-3">
                        <asp:RadioButton ID="rbCheckIn" CssClass="me-3" OnCheckedChanged="rbCheckIn_CheckedChanged" runat="server" AutoPostBack="true" GroupName="CheckStatus" Text="CheckIn" style ="font-size:25px;" />
                        <asp:RadioButton ID="rbCheckOut" runat="server" OnCheckedChanged="rbCheckOut_CheckedChanged" AutoPostBack="true" GroupName="CheckStatus" Text="CheckOut" style ="font-size:25px"/>
                    </div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3"></asp:Label>
                    <asp:Panel ID="divCheckIn" runat="server" Visible="false">
                        <div class="form-group">
                            <label>ID Type:</label>
                            <asp:DropDownList ID="slcIdType" runat="server" CssClass="form-select">
                                <asp:ListItem Value="" Text="Select ID Type"></asp:ListItem>
                                <asp:ListItem Value="AADHAR" Text="AADHAR"></asp:ListItem>
                                <asp:ListItem Value="PAN" Text="PAN"></asp:ListItem>
                                <asp:ListItem Value="DRIVING LICENSE" Text="DRIVING LICENSE"></asp:ListItem>
                                <asp:ListItem Value="VOTER ID" Text="VOTER ID"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>ID Number:</label>
                            <asp:TextBox ID="txtidno" runat="server" CssClass="form-control asp-textbox" placeholder="Id Number"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Visitor Name:</label>
                            <asp:TextBox ID="txtvisitorName" runat="server" CssClass="form-control asp-textbox" placeholder="Visitor Name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Company Name:</label>
                            <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control asp-textbox" placeholder="Company Name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Mobile:</label>
                            <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control asp-textbox" placeholder="Mobile"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Email:</label>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control asp-textbox" placeholder="Enter email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From:</label>
                            <asp:TextBox ID="txtFromLocation" runat="server" CssClass="form-control asp-textbox" placeholder="From"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>To:</label>
                            <asp:TextBox ID="txtTolocation" runat="server" CssClass="form-control asp-textbox" placeholder="To"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From Date:</label>
                            <asp:TextBox ID="dtfromDate" runat="server" CssClass="form-control asp-textbox" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>To Date:</label>
                            <asp:TextBox ID="dttoDate" runat="server" CssClass="form-control asp-textbox" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Gender:</label><br />
                            <asp:RadioButton ID="rdogendermale" runat="server" GroupName="Gender" Text="Male" CssClass="me-2" />
                            <asp:RadioButton ID="rdogenderfemale" runat="server" GroupName="Gender" Text="Female" />
                        </div>
                        <div class="form-group">
                            <label>Age:</label>
                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control asp-textbox" placeholder="Age"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Pax:</label>
                            <asp:TextBox ID="txtpax" runat="server" CssClass="form-control asp-textbox" placeholder="Pax"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Vehicle:</label>
                            <asp:TextBox ID="txtvehical" runat="server" CssClass="form-control asp-textbox" placeholder="Vehicle"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>To Meet:</label>
                            <asp:TextBox ID="txttomeet" runat="server" CssClass="form-control asp-textbox" placeholder="To Meet"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Purpose of Meet:</label>
                            <asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control asp-textbox" placeholder="Purpose of Meet"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Remark:</label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control asp-textbox" placeholder="Remark"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Approved By:</label>
                            <asp:TextBox ID="txtapproved" runat="server" CssClass="form-control asp-textbox" placeholder="Approved By"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Capture Image:</label>
                            <asp:FileUpload ID="fileCapture" runat="server" CssClass="form-control" />
                            <button type="button" onclick="openCamera()" class="btn btn-secondary mt-2">Open Camera</button>
                            <button type="button" onclick="captureImage()" style="display: none;" id="captureButton" class="btn btn-info mt-2">Capture</button>
                            <video id="video" autoplay style="display: none; width: 150px; height: 150px; margin-top: 10px;"></video>
                            <br />
                            <img id="imagePreview" src="#" alt="Visitor Image" style="display: none; width: 150px; height: 150px; margin-top: 10px;" />
                            <canvas id="canvas" style="display: none;"></canvas>
                            <asp:HiddenField ID="capturedImageData" runat="server" />
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Data" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="divCheckOut" runat="server" Visible="false" style="width:100%;overflow:auto;">
    <asp:GridView ID="gvVisitors" runat="server" AutoGenerateColumns="False" CssClass="responsive-table table table-bordered" DataKeyNames="VisitorId" OnRowDataBound="gvVisitors_RowDataBound" OnRowCommand="gvVisitors_RowCommand">
        <Columns>
            <asp:BoundField DataField="VisitorId" HeaderText="ID" />
           

        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnCheckout" runat="server" Text="Checkout"
                    CommandName="checkout"
                    CommandArgument='<%# Eval("VisitorId") %>'
                    OnClientClick="return checkoutconfirm(this);" />
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Name" />
            <asp:BoundField DataField="IDType" HeaderText="ID Type" />
            <asp:BoundField DataField="IDNumber" HeaderText="ID No" />
            <asp:BoundField DataField="Pax" HeaderText="Pax" />
            <asp:BoundField DataField="Vehicle" HeaderText="Vehicle" />
            <asp:BoundField DataField="ValidFrom" HeaderText="Valid From" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
            <asp:BoundField DataField="ValidTill" HeaderText="Valid Till" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
            <asp:BoundField DataField="Status" HeaderText="Checkout Status" />
        </Columns>
    </asp:GridView>
</asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script>
        let videoStream = null;
        function openCamera() {
            navigator.mediaDevices.getUserMedia({ video: true })
                .then(function (stream) {
                    videoStream = stream;
                    let video = document.getElementById('video');
                    video.srcObject = stream;
                    video.style.display = "block";
                    document.getElementById('captureButton').style.display = "inline";
                })
                .catch(function (error) {
                    console.log("Error accessing camera: ", error);
                });
        }
        function captureImage() {
            let video = document.getElementById('video');
            let canvas = document.getElementById('canvas');
            let context = canvas.getContext('2d');
            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;
            context.drawImage(video, 0, 0, canvas.width, canvas.height);
            let imageData = canvas.toDataURL('image/png');
            document.getElementById('imagePreview').src = imageData;
            document.getElementById('imagePreview').style.display = "block";
            document.getElementById('<%= capturedImageData.ClientID %>').value = imageData;
            stopCamera();
        }
        function stopCamera() {
            if (videoStream) {
                videoStream.getTracks().forEach(track => track.stop());
            }
            document.getElementById('video').style.display = "none";
            document.getElementById('captureButton').style.display = "none";
        }
    </script>
</asp:Content>