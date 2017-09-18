<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       
        body {
            background-image: url('Images/loginPage.jpg');
            background-repeat: no-repeat;
            background-size: 100%;
            height: 583px;
        }
    
        .auto-style1 {
            width: 303px;

        }      
    
        .auto-style2 {
            width: 124px;
            font-size:large;
            font-style:italic;
            font-weight:bold;
        }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="border: medium solid #33CC33; height: 237px; width: 328px; background-color: #FFFFFF; margin-top:300px; margin-left:800px; line-height: normal;">
                <tr>
                    <td class="auto-style2">User Name</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtUsername" runat="server" BackColor="#DBDBDB" BorderColor="#666666" BorderWidth="2px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="User Name is mandatory"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtPassword" runat="server" BackColor="#DBDBDB" BorderColor="#666666" BorderWidth="2px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is mandatory"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" BackColor="#99FF99" BorderColor="#666666" BorderStyle="Solid" Font-Bold="True" Font-Size="Large" Height="50px" />
                    </td>
                </tr>
            </table>
         </div>
    </form>
</body>
</html>
